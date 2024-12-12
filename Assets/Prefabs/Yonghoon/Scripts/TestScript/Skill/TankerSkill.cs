using Defend.TestScript;
using System.Collections;
using UnityEngine;

namespace Defend.Enemy.Skill
{
    public class TankerSkill : SkillBase
    {
        public override void ActivateSkill()
        {
            hasSkill = true;

            //Debug.Log("��Ŀ�� ���� ���� ���� ���");

            // ���� �� Ư�� ���̾��� Collider �˻�
            int layerMask = LayerMask.GetMask("Enemy", "Boss");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, layerMask);


            foreach (var collider in hitColliders)
            {
                // GetComponentInParent�� EnemyStats ��������
                var healthComponent = collider.GetComponentInParent<Health>();
                if (healthComponent != null)
                {
                    healthComponent.ChangedArmor(amount); // ���� ����
                    //Debug.Log($"{healthComponent.gameObject.name}�� ������ {amount}��ŭ �����߽��ϴ�!");

                    // ���� �ð� ���� ���� ���� �� ���� ������ ����
                    healthComponent.StartCoroutine(RestoreArmorAfterDuration(healthComponent, skillDuration));
                }
            }
        }

        // ���� ���� �� ���� �ð� ���ȸ� �����ǰ� �ϴ� �ڷ�ƾ
        private IEnumerator RestoreArmorAfterDuration(Health healthComponent, float duration)
        {
            yield return new WaitForSeconds(duration);

            // ������ ���� ������ ����
            healthComponent.ChangedArmor(-amount);
            //Debug.Log($"{healthComponent.gameObject.name}�� ������ {amount}��ŭ �ٽ� �����߽��ϴ�!");
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