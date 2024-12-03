using Defend.Enemy;
using Defend.TestScript;
using UnityEngine;
/// <summary>
/// Ÿ���� �����ϴ� �߻�ü�� ����� ����
/// </summary>
namespace Defend.Projectile
{
    public class TargetProjectile : ProjectileBase
    {
        protected override void Start()
        {
        }

        protected override void Update()
        {
            if (target == null)
            {
                Destroy(this.gameObject);
                return;
            }

            MoveToTarget();
            if (ArrivalTarget() == true) { Hit(); }
        }

        // Ÿ���� ���� �̵�
        protected virtual void MoveToTarget()
        {
            // Ÿ���� ���� ��ġ�� �������� �ݿ��� targetPosition ����
            targetPosition = target.position + offset;
            // Ÿ���� �ٶ󺸵��� 
            transform.LookAt(targetPosition);
            // Ÿ���� ���� �̵�
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * projectileInfo.moveSpeed);
        }

        // Ÿ�ٿ� ���� ����
        protected virtual bool ArrivalTarget()
        {
            // Ÿ�ٱ����� ����
            Vector3 dir = targetPosition - transform.position;
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
    }
}