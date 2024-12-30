using Defend.TestScript;
using Defend.Utillity;

using UnityEngine;
using UnityEngine.Events;

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

        private EnemyMoveController moveController; // ���� �̵� �Ӽ�
        private Animator animator; // �ִϸ����� ������Ʈ
        private Health damageableTarget;


        //���� ����
        public float baseAttackDamage = 10f;
        public float baseAttackDelay = 2f;

        public float CurrentAttackDamage { get; private set; }
        public float CurrentAttackDelay { get; private set; }

        private bool isAttacking = false;
        private bool hasArrived = false;

        private EnemyController enemyController;
        private bool isChanneling = false;

        public UnityAction<float> AttackDamageChanged;
        public UnityAction OnAttacking;

        //���ݽ� SFX
        [SerializeField] private AudioClip attackClip;
        #endregion

        private void Awake()
        {
            attackTarget = FindAnyObjectByType<HealthBasedCastle>().transform;
            damageableTarget = attackTarget.GetComponent<Health>();
            // ����
            moveController = GetComponent<EnemyMoveController>();
            enemyController = GetComponent<EnemyController>();
            animator = GetComponent<Animator>();

            //�ʱ�ȭ
            CurrentAttackDamage = baseAttackDamage;
            //�������ڸ��� ����
            CurrentAttackDelay = 0;
        }

        private void Start()
        {
            moveController.EnemyArrive += OnEnemyArrive;
            enemyController.OnChanneling += OnChanneling;
        }


        private void Update()
        {
            //�������ʹ� ���ݱ�� ����
            if (enemyController.type == EnemyType.Buffer) return;

            //Enemy�� ������ WayPoint�� �������� �ʾҰų�, �������̰ų�, ���� Ÿ���� ���ų�, ��ų�� ������̶�� ���� ������ �ð��� �������� �ʰ� ���ݵ� ���� ����
            if (!hasArrived || isAttacking || !damageableTarget || isChanneling) return;
            // ���� ��Ÿ�Ӹ��� ����
            if (CurrentAttackDelay > 0f)
            {
                CurrentAttackDelay -= Time.deltaTime;
            }
            else
            {
                TriggerAttackAnimation();
            }
        }

        private void TriggerAttackAnimation()
        {
            if (damageableTarget.CurrentHealth > 0f)
            {
                transform.LookAt(attackTarget);
                // ���� �ִϸ��̼� ����
                animator.SetTrigger(Constants.ENEMY_ANIM_ATTACKTRIGGER);
            }
            else
            {
                animator.ResetTrigger(Constants.ENEMY_ANIM_ATTACKTRIGGER);
            }
        }

        public void ChangeAttackingStatus()
        {
            isAttacking = !isAttacking;
            OnAttacking?.Invoke();
        }

        // �ִϸ��̼� �̺�Ʈ���� ȣ���� �޼���
        public void PerformAttack()
        {
            AudioUtility.CreateSFX(attackClip, transform.position, AudioUtility.AudioGroups.EFFECT, 1);
            damageableTarget.TakeDamage(CurrentAttackDamage);
        }

        public void StartAttackCooldown()
        {
            CurrentAttackDelay = baseAttackDelay; // ���� ���ð� �ʱ�ȭ
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

        private void OnChanneling()
        {
            isChanneling = !isChanneling;
        }
    }
}
