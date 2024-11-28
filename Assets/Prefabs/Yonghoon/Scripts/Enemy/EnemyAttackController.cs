using UnityEngine;

namespace Defend.Enemy
{
    /// <summary>
    /// ���� ������ ����ϴ� ��Ʈ�ѷ� Ŭ����
    /// </summary>
    public class EnemyAttackController : MonoBehaviour
    {
        // ���� ���
        [SerializeField] private Transform attackTarget;

        private EnemyStats enemyStats; // ���� �⺻ �Ӽ�
        private Animator animator; // �ִϸ����� ������Ʈ

        //���� ����
        public float baseAttackDamage = 10f;
        public float baseAttackDelay = 2f;

        public float CurrentAttackDamage { get; private set; }
        public float CurrentAttackDelay { get; private set; }

        private float attackCooldown;
        private bool isAttacking;

        private void Start()
        {
            // ����
            enemyStats = GetComponent<EnemyStats>();
            animator = GetComponent<Animator>();

            //�ʱ�ȭ
            CurrentAttackDamage = baseAttackDamage;
            CurrentAttackDelay = baseAttackDelay;
            attackCooldown = 0f;
            isAttacking = false;
        }

        private void Update()
        {
            //if (isAttacking || !attackTarget) return;
            if (isAttacking) return;
            // ���� ��Ÿ�Ӹ��� ����
            if (attackCooldown > 0f)
            {
                attackCooldown -= Time.deltaTime;
            }
            else
            {
                TriggerAttackAnimation();
            }

        }

        private void TriggerAttackAnimation()
        {
            // ���� �ִϸ��̼� ����
            Debug.Log("Attack!");
            animator.SetTrigger("Attack");
            isAttacking = true;
        }

        // �ִϸ��̼� �̺�Ʈ���� ȣ���� �޼���
        public void PerformAttack()
        {
            if (attackTarget == null) return;

            //// ������ ��� �� ����
            //IDamageable damageableTarget = attackTarget.GetComponent<IDamageable>();
            //if (damageableTarget != null)
            //{
            //    damageableTarget.TakeDamage(CurrentAttackDamage);
            //    Debug.Log($"{enemyStats.type} dealt {CurrentAttackDamage} damage to the target.");
            //}
        }

        // ���� ����� �����ϴ� �޼���
        //public void SetAttackTarget(Transform target)
        //{
        //    attackTarget = target;
        //}

        public void StartAttackCooldown()
        {
            isAttacking = false;
            attackCooldown = CurrentAttackDelay; // ���� ���ð� �ʱ�ȭ
        }
    }
}
