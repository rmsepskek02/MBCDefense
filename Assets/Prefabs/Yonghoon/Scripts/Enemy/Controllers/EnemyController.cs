using UnityEngine;
using Defend.Enemy;
using System.Collections.Generic;
using Defend.item;
using Defend.Utillity;
using Defend.Enemy.Skill;
using UnityEngine.Events;
using System;

namespace Defend.TestScript
{
    public enum EnemyType
    {
        Buffer,
        Warrior,
        Tanker,
        Boss
    }

    // Renderer�� ��Ƽ���� �ε����� �����ϱ� ���� ����ü
    [System.Serializable]
    public struct RendererIndexData
    {
        public Renderer renderer;
        public int metarialIndx;

        public RendererIndexData(Renderer _renderer, int index)
        {
            renderer = _renderer;
            metarialIndx = index;
        }
    }

    public class EnemyController : MonoBehaviour
    {
        #region Variables
        //Enemy IMG Sprite
        public Sprite sprite;

        //�ִϸ�����
        private Animator animator;
        private float animatorSpeed;

        //��ų�� �޾ƿ� ������Ʈ
        private SkillBase skill; // ���� ��ų ����
        private bool channeling = false; //��ų�� ��� ������ üũ
        public UnityAction OnChanneling;


        //������ ����� VFX
        public ParticleSystem buffParticleSystem;
        public ParticleSystem debuffParticleSystem;
        public ParticleSystem healParticleSystem;

        //SFX
        public AudioClip buffAudioClip;
        public AudioClip debuffAudioClip;
        public AudioClip healAudioClip;
        public AudioClip dieAudioClip;

        private Health health;//ü�´�� ������Ʈ
        private EnemyMoveController moveController;//�̵���� ������Ʈ

        private EnemyAttackController attackController;//���ݴ�� ������Ʈ
        private bool isAttacking = false;

        public Vector3 positionOffset;//�̻����� ���ƿͼ� �ε��� ���� offset���� �Ҵ�
        public float scaleOffset;//���͸��� Ÿ���� ����Ʈ�� �������� �����ϱ����� scaleOffset

        //����߸� ��� ����
        public int rewardGoldCount;
        public int RewardGoldCount { get { return rewardGoldCount; } private set { rewardGoldCount = value; } } //������ �ʿ�� ����� ���۷���
        public GameObject goldPrefab;           //���� ������
        public Transform offsetTransform;       //������ ��ġ (���� ����)

        // VFX ���� ����
        public Material bodyMaterial; // �������� �� ��Ƽ����
        [GradientUsage(true)] public Gradient hitEffectGradient; // ������ �÷� �׶���Ʈ ȿ��
        [GradientUsage(true)] public Gradient healEffectGradient; // ������ �÷� �׶���Ʈ ȿ��
        private List<RendererIndexData> bodyRenderers = new List<RendererIndexData>();
        private MaterialPropertyBlock bodyFlashMaterialPropertyBlock;
        private float lastTimeEffect; // ���������� ȿ���� �߻��� �ð�

        [SerializeField] private float flashDuration = 0.5f;
        private bool isFlashing; // ��¦�Ÿ� ����
        private Gradient currentEffectGradient; // ���� ���� ���� �׶���Ʈ

        public EnemyType type;


        #endregion
        void Start()
        {
            //����
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();
            moveController = GetComponent<EnemyMoveController>();
            attackController = GetComponent<EnemyAttackController>();


            //UnityAction
            health.OnDie += OnDie;
            health.OnHeal += OnHeal;
            health.OnDamaged += OnDamaged;

            //������ ��������� UnityAction
            health.Armorchange += UpdateArmor;
            moveController.MoveSpeedChanged += UpdateSpeed;
            attackController.AttackDamageChanged += UpdateAttactDamage;
            attackController.OnAttacking += OnAttacking;

            // ��¦�� ȿ�� �ʱ�ȭ
            bodyFlashMaterialPropertyBlock = new MaterialPropertyBlock();
            Renderer[] renderers = GetComponentsInChildren<Renderer>(true);
            foreach (var renderer in renderers)
            {
                for (int i = 0; i < renderer.sharedMaterials.Length; i++)
                {
                    if (renderer.sharedMaterials[i] == bodyMaterial)
                    {
                        bodyRenderers.Add(new RendererIndexData(renderer, i));
                    }
                }
            }

            //�ʱ�ȭ
            animatorSpeed = animator.speed;

            buffParticleSystem.Stop();
            debuffParticleSystem.Stop();
            healParticleSystem.Stop();

            switch (type)
            {
                case EnemyType.Tanker:
                    skill = gameObject.GetComponent<TankerSkill>();
                    break;
                case EnemyType.Warrior:
                    skill = gameObject.GetComponent<WarriorSkill>();
                    break;
                case EnemyType.Buffer:
                    skill = gameObject.GetComponent<WizardSkill>();
                    break;
                case EnemyType.Boss:
                    skill = gameObject.GetComponent<BossSkill>();
                    break;
                default:
                    Debug.LogWarning("Unknown EnemyType. No skill assigned.");
                    break;
            }
        }



        void Update()
        {
            // ������ ȿ�� ������Ʈ
            if (isFlashing)
            {
                UpdateEffect();
            }

            if (skill.CanActivateSkill(health.GetRatio()) && !channeling && !isAttacking)
            {
                if (type == EnemyType.Boss)
                {
                    skill.SoundPlay(2, 30);
                }
                else
                {
                    skill.SoundPlay();
                }
                animator.SetTrigger(Constants.ENEMY_ANIM_SKILLTRIGGER);
                ChangeChannelingStatus();
            }
        }

        public void ChangeChannelingStatus()
        {
            channeling = !channeling;
            OnChanneling?.Invoke();
        }

        private void OnDamaged(float amount)
        {
            //Debug.Log("���� ����");
            TriggerEffect(hitEffectGradient); // ������ ȿ�� ����
        }

        private void OnHeal(float amount)
        {
            //Debug.Log("�� ����");
            healParticleSystem.Play();
            if (healAudioClip != null)
            {
                AudioUtility.CreateSFX(healAudioClip, transform.position, AudioUtility.AudioGroups.BuffAndDebuff, 1);
            }
        }

        private void OnDie()
        {
            AudioUtility.CreateSFX(dieAudioClip, transform.position, AudioUtility.AudioGroups.EFFECT);

            //����ִ� ���ʹ� �� ����
            ListSpawnManager.enemyAlive--;

            animator.SetBool(Constants.ENEMY_ANIM_ISDEATH, true);

            //������ ���� ��Ѹ���
            ScatterCoins();

            // �ִϸ��̼� ���� �� ���ŵǴ� ������ StateMachineBehaviour���� ó��
        }

        private void TriggerEffect(Gradient effectGradient)
        {
            lastTimeEffect = Time.time; // ȿ�� ���� �ð� ���
            currentEffectGradient = effectGradient; // ���� ���� ���� �׶���Ʈ ����
            isFlashing = true; // ��¦�Ÿ� ����
        }

        private void UpdateEffect()
        {
            // ��¦�Ÿ� ���� �ð� ���
            float elapsed = Time.time - lastTimeEffect;

            if (elapsed > flashDuration)
            {
                // ��¦�Ÿ� ȿ�� ����
                ResetMaterialProperties();
                return;
            }

            // ���� �ð��� ���� ���� ���
            Color currentColor = currentEffectGradient.Evaluate(elapsed / flashDuration);
            bodyFlashMaterialPropertyBlock.SetColor("_EmissionColor", currentColor);

            // �� �������� ȿ�� ����
            foreach (var data in bodyRenderers)
            {
                data.renderer.SetPropertyBlock(bodyFlashMaterialPropertyBlock, data.metarialIndx);
            }
        }

        private void ResetMaterialProperties()
        {
            // ��¦�Ÿ� ȿ���� �ʱ�ȭ�Ͽ� ���� ���·� �ǵ���
            bodyFlashMaterialPropertyBlock.SetColor("_EmissionColor", Color.black);
            foreach (var data in bodyRenderers)
            {
                data.renderer.SetPropertyBlock(bodyFlashMaterialPropertyBlock, data.metarialIndx);
            }
            isFlashing = false;
        }

        private void ScatterCoins()
        {
            for (int i = 0; i < rewardGoldCount; i++)
            {
                // ���� ����
                Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                GameObject coin = Instantiate(goldPrefab, spawnPosition, Quaternion.identity);
                DropItem item = coin.GetComponent<DropItem>();
                item.amount = 1f;
                item.resourceName = "Money";

            }
        }

        private void UpdateSpeed(float value, float rate)
        {
            // rate�� ���� ���� �Ǵ� ����� ȿ�� ����
            PlayEffect(rate);

            animator.speed = animatorSpeed * (1.0f + rate);
        }

        private void UpdateArmor(float amount)
        {
            //Debug.Log($"{amount}��ŭ ���� ��/���ҵ�!");
            PlayEffect(amount);
        }

        private void UpdateAttactDamage(float amount)
        {
            //Debug.Log($"{amount}��ŭ ���ݷ� ��/���ҵ�!");
            PlayEffect(amount);
        }

        private void PlayEffect(float amount)
        {
            if (amount > 0)
            {
                AudioUtility.CreateSFX(buffAudioClip, transform.position, AudioUtility.AudioGroups.BuffAndDebuff, 1);
                buffParticleSystem.Play();
            }
            else if (amount < 0)
            {
                AudioUtility.CreateSFX(debuffAudioClip, transform.position, AudioUtility.AudioGroups.BuffAndDebuff, 1);
                debuffParticleSystem.Play();
            }
        }

        //���������� Ȯ���ϴ� UnityAction
        private void OnAttacking()
        {
            isAttacking = !isAttacking;
        }
    }
}