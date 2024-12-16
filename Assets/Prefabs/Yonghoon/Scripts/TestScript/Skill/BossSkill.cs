using System.Collections.Generic;
using UnityEngine;
using Defend.TestScript;

namespace Defend.Enemy.Skill
{
    public class BossSkill : SkillBase
    {
        #region Variables
        private float speedAmount = 1f;
        private float healthRatio;
        private List<float> thresholds = new List<float> { 1f, 0.75f, 0.50f }; // ü�� ���� ����
        private HashSet<float> usedThresholds = new HashSet<float>(); // �̹� ���� ü�� ���� ����
        private Dictionary<float, SkillType> skillMapping; // ü�� ������ ��ų Ÿ�� ����
        #endregion

        private enum SkillType
        {
            IncreaseSpeed,
            IncreaseArmor,
            IncreaseDamage
        }

        private void Start()
        {
            // ü�� ������ ���� ��ų ����
            skillMapping = new Dictionary<float, SkillType>
            {
                { 1f, SkillType.IncreaseSpeed },
                { 0.75f, SkillType.IncreaseArmor },
                { 0.5f, SkillType.IncreaseDamage }
            };
        }

        public override void ActivateSkill()
        {
            Debug.Log("Boss uses a skill!");

            foreach (var threshold in thresholds)
            {
                if (healthRatio <= threshold && !usedThresholds.Contains(threshold))
                {
                    usedThresholds.Add(threshold); // ���� ���� �߰�
                    ExecuteSkill(skillMapping[threshold]); // �ش� ��ų ����
                    break;
                }
            }
        }

        private void ExecuteSkill(SkillType skillType)
        {
            Debug.Log("��ų�� �б���");
            switch (skillType)
            {
                case SkillType.IncreaseArmor:
                    IncreaseArmor();
                    break;

                case SkillType.IncreaseDamage:
                    IncreaseDamage();
                    break;

                case SkillType.IncreaseSpeed:
                    IncreaseSpeed();
                    break;
            }
        }

        private void IncreaseArmor()
        {
            Debug.Log("���� ü�� 75%��ų �Ƹ� ����!");

            int layerMask = LayerMask.GetMask("Enemy", "Boss");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, layerMask);
            foreach (var collider in hitColliders)
            {
                var enemyController = collider.GetComponentInParent<Health>();
                if (enemyController != null)
                {
                    enemyController.ChangedArmor(amount);
                }
            }
        }

        private void IncreaseDamage()
        {
            Debug.Log("���� ü�� 50%��ų ���ݷ� ����!");

            int layerMask = LayerMask.GetMask("Enemy", "Boss");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, layerMask);
            foreach (var collider in hitColliders)
            {
                var enemyController = collider.GetComponentInParent<EnemyAttackController>();
                if (enemyController != null)
                {
                    enemyController.ChangedAttackDamage(amount);
                }
            }
        }

        private void IncreaseSpeed()
        {
            Debug.Log("���� ���� ��ų �̵��ӵ� ����!");

            int layerMask = LayerMask.GetMask("Enemy", "Boss");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, layerMask);
            foreach (var collider in hitColliders)
            {
                var enemyController = collider.GetComponentInParent<EnemyMoveController>();
                if (enemyController != null)
                {
                    enemyController.ChangedMoveSpeed(this.gameObject, speedAmount);
                }
            }
        }

        public override bool CanActivateSkill(float healthRatio)
        {
            this.healthRatio= healthRatio;
            foreach (var threshold in thresholds)
            {
                // Ư�� ü�� ������ ����������, ���� ������ ���� ��� �ߵ� ����
                if (healthRatio <= threshold && !usedThresholds.Contains(threshold))
                {
                    return true;
                }
            }

            return false;
        }

        #region Test�� GIZMO
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }
        #endregion
    }
}
