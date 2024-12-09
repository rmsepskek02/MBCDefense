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
        #endregion

        #region Test�� Range
        private float range = 5f;
        #endregion

        public override void ActivateSkill(Transform activator, float range)
        {
            Debug.Log("Warrior uses Roar skill!");

            // ���� �� �� Ž��
            Collider[] hitEnemies = Physics.OverlapSphere(activator.position, range);

            foreach (var enemyCollider in hitEnemies)
            {
                var enemyStats = enemyCollider.GetComponentInParent<EnemyAttackController>();
                if (enemyStats != null)
                {
                    enemyStats.ChangedAttackDamage(increaseAttackPowerAmount); // ���ݷ� ����
                    Debug.Log($"{enemyStats.gameObject.name}�� ���ݷ��� �����߽��ϴ�!");
                }
            }
            //ShowEffect(activator, range);
        }

        //���� ����Ʈ�� �߰��ϰԵǸ� ����
        private void ShowEffect(Transform activator, float range)
        {
            Debug.Log($"Warrior ��ȿ ȿ�� �߻�! ����: {range}");
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