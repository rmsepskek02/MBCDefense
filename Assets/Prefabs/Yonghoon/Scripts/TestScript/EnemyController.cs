using UnityEngine;
using Defend.Enemy;
using System.Collections.Generic;
using Defend.item;
using Defend.Utillity;
using Defend.Enemy.Skill;

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
        //�ִϸ�����
        private Animator animator;
        private float animatorSpeed;

        //��ų�� �޾ƿ� ������Ʈ
        private SkillBase skill; // ���� ��ų ����

        //������ �����
        public ParticleSystem buffParticleSystem;
        public ParticleSystem debuffParticleSystem;
        public ParticleSystem healParticleSystem;

        private Health health;//ü�´�� ������Ʈ
        private EnemyMoveController moveController;//�̵���� ������Ʈ
        private EnemyAttackController attackController;//���ݴ�� ������Ʈ

        public Vector3 positionOffset;//�̻����� ���ƿͼ� �ε��� ���� offset���� �Ҵ�
        public float scaleOffset;//���͸��� Ÿ���� ����Ʈ�� �������� �����ϱ����� scaleOffset

        //����߸� ��� ����
        [SerializeField] private int rewardGoldCount;
        public int RewardGoldCount { get { return rewardGoldCount; } private set { rewardGoldCount = value; } } //������ �ʿ�� ����� ���۷���
        public GameObject goldPrefab;           //���� ������
        public float scatterForce = 5f;         //��Ѹ� ��
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
        }

        void Update()
        {
            // ������ ȿ�� ������Ʈ
            if (isFlashing)
            {
                UpdateEffect();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnHeal(1);
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                PlayEffect(0.1f);
            }
        }

        private void OnDamaged(float amount)
        {
            Debug.Log("���� ����");
            TriggerEffect(hitEffectGradient); // ������ ȿ�� ����
        }

        private void OnHeal(float amount)
        {
            //TriggerEffect(healEffectGradient); // �� ȿ�� ����
            Debug.Log("�� ����");
            healParticleSystem.Play();
        }

        private void OnDie()
        {
            //����ִ� ���ʹ� �� ����
            ListSpawnManager.enemyAlive--;

            animator.SetBool(Constants.ENEMY_ANIM_ISDEATH, true);

            //������ ���� ��Ѹ���
            ScatterCoins();

            //Enemy ų
            Destroy(gameObject, 2f);
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
                GameObject coin = Instantiate(goldPrefab, transform.position, Quaternion.identity);
                DropItem item = coin.GetComponent<DropItem>();
                item.amount = 1f;
                item.resourceName = "Money";

            }
        }

        private void UpdateSpeed(float value, float rate)
        {
            // rate�� ���� ���� �Ǵ� ����� ȿ�� ����
            PlayEffect(rate);

            Debug.Log($"before anim speed = {animator.speed}, rate = {rate}");
            animator.speed = animatorSpeed * (1.0f + rate);
            Debug.Log($"after anim speed = {animator.speed}");
        }

        private void UpdateArmor(float amount)
        {
            Debug.Log($"{amount}��ŭ ���� ��/���ҵ�!");
            PlayEffect(amount);
        }

        private void PlayEffect(float amount)
        {
            if (amount > 0)
            {
                buffParticleSystem.Play();
            }
            else if (amount < 0)
            {
                debuffParticleSystem.Play();
            }
        }

        private void ActiveSkill()
        {

        }
    }
}