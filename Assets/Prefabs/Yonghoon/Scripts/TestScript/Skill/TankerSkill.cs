using Defend.TestScript;
using System.Collections;
using UnityEngine;

namespace Defend.Enemy.Skill
{
    public class TankerSkill : SkillBase
    {
        #region Variables
        [SerializeField] private float increaseArmorAmount = 5f;
        [SerializeField] private float skillDuration = 5f;  // ���� ���� ���� �ð�
        [SerializeField] private bool hasSkill = false;
        #endregion

        #region Test�� Range
        [SerializeField] private float range = 5f;
        #endregion

        public override void ActivateSkill()
        {
            hasSkill = true;
            
            Debug.Log("��Ŀ�� ���� ���� ���� ���");

            // ���� �� ��� Collider �˻�
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);

            foreach (var collider in hitColliders)
            {
                // GetComponentInParent�� EnemyStats ��������
                var healthComponent = collider.GetComponentInParent<Health>();
                if (healthComponent != null)
                {
                    healthComponent.ChangedArmor(increaseArmorAmount); // ���� ����
                    Debug.Log($"{healthComponent.gameObject.name}�� ������ {increaseArmorAmount}��ŭ �����߽��ϴ�!");

                    // ���� �ð� ���� ���� ���� �� ���� ������ ����
                    healthComponent.StartCoroutine(RestoreArmorAfterDuration(healthComponent, skillDuration));
                }
            }

            // ������ ����Ʈ
            //ShowEffect(activator, range);
        }

        private void ShowEffect(Transform activator, float range)
        {
            Debug.Log($"Warrior ��ȿ ȿ�� �߻�! ����: {range}");
        }

        // ���� ���� �� ���� �ð� ���ȸ� �����ǰ� �ϴ� �ڷ�ƾ
        private IEnumerator RestoreArmorAfterDuration(Health healthComponent, float duration)
        {
            yield return new WaitForSeconds(duration);

            // ������ ���� ������ ����
            healthComponent.ChangedArmor(-increaseArmorAmount);
            Debug.Log($"{healthComponent.gameObject.name}�� ������ {increaseArmorAmount}��ŭ �ٽ� �����߽��ϴ�!");
        }

        public override bool CanActivateSkill(float healthRatio)
        {
            // ü���� 50% ������ �� �ߵ�
            return healthRatio <= 0.5f && !hasSkill;
        }

        #region Test�� GIZMO
        private void OnDrawGizmosSelected()
        {
            // Gizmo�� ��ȿ ������ �ð������� ǥ��
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }
        #endregion

    }
}