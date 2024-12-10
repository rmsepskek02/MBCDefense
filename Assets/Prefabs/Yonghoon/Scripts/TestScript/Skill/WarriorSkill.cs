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
        #region Variables
        [SerializeField] private float increaseAttackPowerAmount = 5f;
        [SerializeField] private float skillDuration = 5f;  // ���ݷ� ���� ���� �ð�
        [SerializeField] private bool hasSkill = false;
        #endregion

        #region Test�� Range
        [SerializeField] private float range = 5f;
        #endregion
         
        public override void ActivateSkill()
        {
            hasSkill = true;

            Debug.Log("���簡 ���ݷ� ���� ���� ���!");

            // ���� �� �� Ž��
            Collider[] hitEnemies = Physics.OverlapSphere(transform.position, range);

            foreach (var enemyCollider in hitEnemies)
            {
                var enemyStats = enemyCollider.GetComponentInParent<EnemyAttackController>();
                if (enemyStats != null)
                {
                    enemyStats.ChangedAttackDamage(increaseAttackPowerAmount); // ���ݷ� ����
                    Debug.Log($"{enemyStats.gameObject.name}�� ���ݷ��� {increaseAttackPowerAmount}��ŭ �����߽��ϴ�!");

                    // ���� �ð� ���� ���ݷ� ���� �� ���� ������ ����
                    enemyStats.StartCoroutine(RestoreAttackAfterDuration(enemyStats, skillDuration));
                }
            }
            //ShowEffect(activator, range);
        }

        //���� ����Ʈ�� �߰��ϰԵǸ� ����
        //private void ShowEffect(Transform activator, float range)
        //{
        //    Debug.Log($"Warrior ��ȿ ȿ�� �߻�! ����: {range}");
        //}

        // ���ݷ� ���� �� ���� �ð� ���ȸ� �����ǰ� �ϴ� �ڷ�ƾ
        private IEnumerator RestoreAttackAfterDuration(EnemyAttackController enemyStats, float duration)
        {
            yield return new WaitForSeconds(duration);

            // ���ݷ��� ���� ������ ����
            enemyStats.ChangedAttackDamage(-increaseAttackPowerAmount);
            Debug.Log($"{enemyStats.gameObject.name}�� ������ {increaseAttackPowerAmount}��ŭ �ٽ� �����߽��ϴ�!");
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