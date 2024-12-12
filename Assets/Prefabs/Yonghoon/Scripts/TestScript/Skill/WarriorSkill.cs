using Defend.TestScript;
using System.Collections;
using UnityEngine;

namespace Defend.Enemy.Skill
{
    /// <summary>
    /// Enemy(Warrior) Skill�� �����ϴ� Ŭ����
    /// </summary>
    public class WarriorSkill : SkillBase
    {
        public override void ActivateSkill()
        {
            hasSkill = true;

            //Debug.Log("���簡 ���ݷ� ���� ���� ���!");

            // ���� �� Ư�� ���̾��� Collider �˻�
            int layerMask = LayerMask.GetMask("Enemy", "Boss");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, layerMask);

            foreach (var enemyCollider in hitColliders)
            {
                var enemyStats = enemyCollider.GetComponentInParent<EnemyAttackController>();
                if (enemyStats != null)
                {
                    enemyStats.ChangedAttackDamage(amount); // ���ݷ� ����
                    //Debug.Log($"{enemyStats.gameObject.name}�� ���ݷ��� {amount}��ŭ �����߽��ϴ�!");

                    // ���� �ð� ���� ���ݷ� ���� �� ���� ������ ����
                    enemyStats.StartCoroutine(RestoreAttackAfterDuration(enemyStats, skillDuration));
                }
            }
        }

        // ���ݷ� ���� �� ���� �ð� ���ȸ� �����ǰ� �ϴ� �ڷ�ƾ
        private IEnumerator RestoreAttackAfterDuration(EnemyAttackController enemyStats, float duration)
        {
            yield return new WaitForSeconds(duration);

            // ���ݷ��� ���� ������ ����
            enemyStats.ChangedAttackDamage(-amount);
            //Debug.Log($"{enemyStats.gameObject.name}�� ������ {amount}��ŭ �ٽ� �����߽��ϴ�!");
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