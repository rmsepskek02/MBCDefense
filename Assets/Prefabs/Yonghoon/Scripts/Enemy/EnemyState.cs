using Defend.TestScript;
using UnityEngine;

/// <summary>
/// Enemy�� ���������� �����ϴ� Ŭ����
/// </summary>
namespace Defend.Enemy
{
    public class EnemyState : MonoBehaviour
    {
        //�̵�����
        public float enemySpeed;

        //���� ����
        public float enemyAttackDamage;
        public float enemyAttackDelay;

        //ü�°���
        public float enemyHealth;

        //������
        public float enemyArmor;

        //�� Ÿ��
        public EnemyType type;

        //������ �޾ƿ� ����
        private Health health;
        private EnemyMoveController moveController;
        private EnemyAttackController attackController;

        private void Start()
        {
            //����
            health = GetComponent<Health>();
            moveController = GetComponent<EnemyMoveController>();
            attackController = GetComponent<EnemyAttackController>();

            //��ȭ�� �� UnityActions
            //Health ����
            health.OnDamaged += ChangeHealth;
            health.OnHeal += ChangeHealth;

            //Armor ����
            health.Armorchange += ChangeArmor;

            //���� �ɷ�ġ ����
            attackController.AttackDamageChanged += ChangeDamage;
            attackController.AttackDelayChanged += ChangeDelay;

            //�̵��ӵ� ����
            moveController.MoveSpeedChanged += ChangeSpeed;

            //�ʱ�ȭ
            enemyHealth = health.CurrentHealth;

            enemyArmor = health.CurrentArmor;

            enemyAttackDamage = attackController.CurrentAttackDamage;
            enemyAttackDelay = attackController.CurrentAttackDelay;
            type = attackController.type;

            enemySpeed = moveController.CurrentSpeed;
            
        }

        private void ChangeDelay(float amount)
        {
            enemyAttackDelay -= amount;
        }

        private void ChangeDamage(float amount)
        {
            enemyAttackDamage += amount;
        }

        private void ChangeArmor(float amount)
        {
            enemyArmor += amount;
        }

        private void ChangeHealth(float amount)
        {
            enemyHealth += amount;
        }

        private void ChangeSpeed(float value)
        {
            enemySpeed = value;
        }
    }
}