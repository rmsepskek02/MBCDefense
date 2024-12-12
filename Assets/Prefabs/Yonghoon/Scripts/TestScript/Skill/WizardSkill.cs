using Defend.TestScript;
using UnityEngine;

namespace Defend.Enemy.Skill
{
    public class WizardSkill : SkillBase
    {
        #region Variables
        private float lastSkillTime = -Mathf.Infinity; // ������ ��ų �ߵ� �ð�
        #endregion

        public override void ActivateSkill()
        {
            //Debug.Log("Wizard uses Heal skill!");

            float healAmount = gameObject.GetComponent<EnemyAttackController>().CurrentAttackDamage;

            //Debug.Log(healAmount);

            // ���� �� Ư�� ���̾��� Collider �˻�
            int layerMask = LayerMask.GetMask("Enemy", "Boss");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, layerMask);

            foreach (var collider in hitColliders)
            {
                // GetComponentInParent�� EnemyStats ��������
                var enemyStats = collider.GetComponentInParent<Health>();
                if (enemyStats != null)
                {

                    enemyStats.Heal(healAmount); // ��
                    //Debug.Log($"{enemyStats.gameObject.name}�� ü���� {healAmount}��ŭ �����߽��ϴ�!");
                }
            }
            lastSkillTime = Time.time;
        }

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