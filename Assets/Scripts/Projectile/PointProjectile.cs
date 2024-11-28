using Defend.Enemy;
using Defend.TestScript;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ������ �����ϴ� �߻�ü�� ����� ����
/// </summary>
namespace Defend.Projectile
{
    public class PointProjectile : ProjectileBase
    {
        [SerializeField] protected Vector3 pointTarget;             // ��ǥ ����

        #region collider Ȱ�� => �̻��
        //[SerializeField] protected Collider[] colliders;          // collider ������Ʈ��
        #endregion

        protected override void Start()
        {

        }

        protected override void Update()
        {
            MoveToPoint();
            if (ArrivalPoint() == true) { Hit(); }
        }

        // Init �޼��带 �������̵��Ͽ� target�� ������ �� �ش� ������ ���
        public override void Init(ProjectileInfo _projectileInfo, Transform closestTarget)
        {
            base.Init(_projectileInfo, closestTarget);
            // target�� �����Ǹ� �� ��ġ�� ���
            pointTarget = closestTarget.position;
        }

        // ������ ���� �̵�
        protected virtual void MoveToPoint()
        {
            // ������ �ٶ󺸵��� 
            transform.LookAt(pointTarget);
            // ������ ���� �̵�
            transform.position = Vector3.MoveTowards(transform.position, pointTarget, Time.deltaTime * projectileInfo.moveSpeed);
        }

        // ������ ���� ����
        protected virtual bool ArrivalPoint()
        {
            // Ÿ�ٱ����� ����
            Vector3 dir = pointTarget - transform.position;
            // �߻�ü�� �������Ӵ� �̵��ϴ� �Ÿ�
            float distanceThisFrame = Time.deltaTime * projectileInfo.moveSpeed;
            if (dir.magnitude < distanceThisFrame)
            {
                // ����
                return true;
            }
            // �̵���
            return false;
        }

        // Ÿ��
        protected override void Hit()
        {
            base.Hit();

            #region collider Ȱ�� => �̻��
            // ���� ���� �� collider Ž��
            //colliders = Physics.OverlapSphere(transform.position, projectileInfo.attackRange);
            #endregion

            // EnemyController ������Ʈ�� ���� Object ã��
            var enemies = FindObjectsByType<EnemyController>(FindObjectsSortMode.None);
            foreach (var obj in enemies)
            {
                // �Ÿ� üũ
                float distance = Vector3.Distance(transform.position, obj.transform.position);
                if (distance <= projectileInfo.attackRange)
                {
                    Health ehc = obj.GetComponent<Health>();
                    if (ehc != null)
                    {
                        // ������ �ֱ�
                        ehc.TakeDamage(projectileInfo.attack);
                    }
                }
            }
        }
    }
}