using Defend.TestScript;
using UnityEngine;

namespace Defend.Enemy.Skill
{
    public class WizardSkill : SkillBase
    {
        #region Variables
        [SerializeField] private float healAmount = 5f;
        #endregion

        #region Test�� Range
        private float range = 5f;
        #endregion

        public override void ActivateSkill(Transform activator, float range)
        {
            Debug.Log("Wizard uses Heal skill!");

            // ���� �� ��� Collider �˻�
            Collider[] hitColliders = Physics.OverlapSphere(activator.position, range);

            foreach (var collider in hitColliders)
            {
                // GetComponentInParent�� EnemyStats ��������
                var enemyStats = collider.GetComponentInParent<Health>();
                if (enemyStats != null)
                {
                    enemyStats.Heal(healAmount); // ���� ����
                    Debug.Log($"{enemyStats.gameObject.name}�� ü���� �����߽��ϴ�!");
                }
            }

            // ������ ����Ʈ
            //ShowEffect(activator, range);
        }

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