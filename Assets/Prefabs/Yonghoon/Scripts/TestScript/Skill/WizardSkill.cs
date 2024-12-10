using Defend.TestScript;
using UnityEngine;

namespace Defend.Enemy.Skill
{
    public class WizardSkill : SkillBase
    {

        [SerializeField] private float skillCooldown = 5f; // ��ų �ߵ� �ֱ�
        private float lastSkillTime = -Mathf.Infinity; // ������ ��ų �ߵ� �ð�

        #region Test�� Range
        [SerializeField] private float range = 5f;
        #endregion

        public override void ActivateSkill()
        {
            Debug.Log("Wizard uses Heal skill!");

            float healAmount = gameObject.GetComponent<EnemyAttackController>().CurrentAttackDamage;

            Debug.Log(healAmount);

            // ���� �� ��� Collider �˻�
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);

            foreach (var collider in hitColliders)
            {
                // GetComponentInParent�� EnemyStats ��������
                var enemyStats = collider.GetComponentInParent<Health>();
                if (enemyStats != null)
                {

                    enemyStats.Heal(healAmount); // ���� ����
                    Debug.Log($"{enemyStats.gameObject.name}�� ü���� {healAmount}��ŭ �����߽��ϴ�!");
                }
            }

            // ������ ����Ʈ
            //ShowEffect(activator, range);
        }

        //private void ShowEffect(Transform activator, float range)
        //{
        //    Debug.Log($"Heal ȿ�� �߻�! ����: {range}");
        //}

        public override bool CanActivateSkill(float healthRatio)
        {
            return Time.time - lastSkillTime >= skillCooldown;
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