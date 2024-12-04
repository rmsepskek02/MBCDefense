using UnityEngine;
using Defend.Enemy;
using System.Collections.Generic;
using Defend.item;

namespace Defend.TestScript
{
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

        //ü�´�� ������Ʈ
        private Health health;

        public Vector3 offset;

        //����߸� ��� ����
        [SerializeField] private int rewardGoldCount;
        //������ �ʿ�� ����� ���۷���
        public int RewardGoldCount { get { return rewardGoldCount; } private set { rewardGoldCount = value; } }
        //���� ������
        public GameObject goldPrefab;
        //��Ѹ� ��
        public float scatterForce = 5f;
        //������ ��ġ (���� ����)
        public Transform offsetTransform;


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
        #endregion
        void Start()
        {
            //����
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();

            //UnityAction
            health.OnDie += OnDie;
            health.OnHeal += OnHeal;
            health.OnDamaged += OnDamaged;

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

        }

        void Update()
        {
            // ������ ȿ�� ������Ʈ
            if (isFlashing)
            {
                UpdateEffect();
            }
        }

        private void OnDamaged(float arg0)
        {
            Debug.Log("���� ����");
            TriggerEffect(hitEffectGradient); // ������ ȿ�� ����

        }

        private void OnHeal(float arg0)
        {
            TriggerEffect(healEffectGradient); // �� ȿ�� ����
            Debug.Log("�� ����");
        }

        private void OnDie()
        {
            //����ִ� ���ʹ� �� ����
            ListSpawnManager.enemyAlive--;

            animator.SetBool("IsDeath", true);

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
    }
}