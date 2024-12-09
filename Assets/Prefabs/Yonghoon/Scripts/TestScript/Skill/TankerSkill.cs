using Defend.TestScript;
using UnityEngine;

namespace Defend.Enemy.Skill
{
    public class TankerSkill : SkillBase
    {
        #region Variables
        [SerializeField] private float increaseArmorAmount = 5f;
        #endregion

        #region Test�� Range
        private float range = 5f;
        #endregion

        public override void ActivateSkill(Transform activator, float range)
        {
            Debug.Log("Tanker uses Fortify skill!");

            // ���� �� ��� Collider �˻�
            Collider[] hitColliders = Physics.OverlapSphere(activator.position, range);

            foreach (var collider in hitColliders)
            {
                // GetComponentInParent�� EnemyStats ��������
                var enemyStats = collider.GetComponentInParent<Health>();
                if (enemyStats != null)
                {
                    enemyStats.ChangedArmor(increaseArmorAmount); // ���� ����
                    Debug.Log($"{enemyStats.gameObject.name}�� ������ �����߽��ϴ�!");
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