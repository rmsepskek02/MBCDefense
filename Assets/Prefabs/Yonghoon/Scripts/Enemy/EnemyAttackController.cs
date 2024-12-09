using Defend.TestScript;
using Defend.Utillity;
using UnityEngine;
using UnityEngine.Events;
//using static UnityEngine.XR.OpenXR.Features.Interactions.HTCViveControllerProfile;

namespace Defend.Enemy
{
    /// <summary>
    /// ���� ������ ����ϴ� ��Ʈ�ѷ� Ŭ����
    /// </summary>
    public class EnemyAttackController : MonoBehaviour
    {
        #region Variables
        // ���� ���
        [SerializeField] private Transform attackTarget;

        private EnemyMoveController moveController; // ���� �⺻ �Ӽ�
        private Animator animator; // �ִϸ����� ������Ʈ

        //���� ����
        public float baseAttackDamage = 10f;
        public float baseAttackDelay = 2f;

        public float CurrentAttackDamage { get; private set; }
        public float CurrentAttackDelay { get; private set; }

        private float attackCooldown;
        private bool isAttacking;
        private bool hasArrived;

        public UnityAction<float> AttackDamageChanged;
        #endregion

        private void Awake()
        {
            attackTarget = FindAnyObjectByType<HealthBasedCastle>().transform;

            // ����
            moveController = GetComponent<EnemyMoveController>();
            animator = GetComponent<Animator>();

            //�ʱ�ȭ
            CurrentAttackDamage = baseAttackDamage;
            CurrentAttackDelay = baseAttackDelay;
            attackCooldown = 0f;
            isAttacking = false;
            hasArrived = false;

            moveController.EnemyArrive += OnEnemyArrive;
        }

        private void Update()
        {
            if (!hasArrived) return;
            if (isAttacking || !attackTarget) return;
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
            animator.SetTrigger(Constants.ENEMY_ANIM_ATTACKTRIGGER);
            isAttacking = true;
        }

        // �ִϸ��̼� �̺�Ʈ���� ȣ���� �޼���
        public void PerformAttack()
        {
            // ������ ��� �� ����
            Health damageableTarget = attackTarget.GetComponent<Health>();
            if (damageableTarget != null)
            {
                damageableTarget.TakeDamage(CurrentAttackDamage);
            }
        }

        public void StartAttackCooldown()
        {
            isAttacking = false;
            attackCooldown = CurrentAttackDelay; // ���� ���ð� �ʱ�ȭ
        }

        public void ChangedAttackDamage(float amount)
        {
            CurrentAttackDamage = Mathf.Max(CurrentAttackDamage + amount, 1f);
            AttackDamageChanged?.Invoke(amount);
        }

        private void OnEnemyArrive()
        {
            hasArrived = true;
        }
    }
}
