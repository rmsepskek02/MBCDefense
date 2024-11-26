using UnityEngine;
/// <summary>
/// ������ �����ϴ� �߻�ü�� ����� ����
/// </summary>
namespace Defend.Projectile
{
    public class PointProjectile : ProjectileBase
    {
        [SerializeField ]protected Transform pointTarget;             // ��ǥ ����
        protected override void Start()
        {

        }

        protected override void Update()
        {
            Debug.Log("POSITION = " + pointTarget.position);
            Debug.Log("TRANSFORM = " + pointTarget);
            MoveToPoint();
            if (ArrivalPoint() == true) { Hit(); }
        }

        // Init �޼��带 �������̵��Ͽ� target�� ������ �� �ش� ������ ���
        public override void Init(ProjectileInfo _projectileInfo, Transform _target)
        {
            base.Init(_projectileInfo, _target);
            // target�� �����Ǹ� �� ��ġ�� ���
            pointTarget = _target;
        }

        // ������ ���� �̵�
        protected virtual void MoveToPoint()
        {
            // ������ �ٶ󺸵��� 
            transform.LookAt(pointTarget);
            // ������ ���� �̵�
            transform.position = Vector3.MoveTowards(transform.position, pointTarget.position, Time.deltaTime * projectileInfo.moveSpeed);
        }

        // ������ ���� ����
        protected virtual bool ArrivalPoint()
        {
            // Ÿ�ٱ����� ����
            Vector3 dir = pointTarget.position - transform.position;
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

        protected override void Hit()
        {
            Debug.Log("HITPOINT");
        }
    }
}