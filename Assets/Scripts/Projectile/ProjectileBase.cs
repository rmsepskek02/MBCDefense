using Defend.TestScript;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �߻�ü�� ����� ������ ���� Ŭ����
/// </summary>
namespace Defend.Projectile
{
    public abstract class ProjectileBase : MonoBehaviour
    {
        [SerializeField] protected Transform target;                    // ��ǥ��
        [SerializeField] protected Vector3 targetPosition;              // ��ǥ��
        [SerializeField] protected ProjectileInfo projectileInfo;       // �߻�ü ����
        [SerializeField] protected Vector3 offset;

        protected virtual void Start()
        {
            
        }

        protected virtual void Update()
        {

        }

        // Ÿ�ٰ� �߻�ü ���� �ʱ�ȭ
        public virtual void Init(ProjectileInfo _projectileInfo, Transform closestTarget)
        {
            target = closestTarget;
            offset = target.gameObject.GetComponent<EnemyController>().offset;
            targetPosition = target.position + offset;
            projectileInfo = _projectileInfo;
        }

        // Ÿ�ٿ� Projectile Effect ����
        protected virtual void Hit()
        {
            // Projectile Effect ����
            GameObject effect = Instantiate(projectileInfo.effectPrefab, transform.position, Quaternion.identity);
            // Projectile Effect ���� ����
            Destroy(effect, projectileInfo.effectTime);
            // Projectile ���� 
            Destroy(this.gameObject);
        }

        // Ÿ���� ���� ���� ��ŭ ��������
        protected void HitOnRange()
        {
            // EnemyController ������Ʈ�� ���� Object ã��
            var enemies = FindObjectsByType<EnemyController>(FindObjectsSortMode.None);
            foreach (var obj in enemies)
            {
                // �Ÿ� üũ
                float distance = Vector3.Distance(transform.position, obj.transform.position);
                if (distance <= projectileInfo.attackRange)
                {
                    Health health = obj.GetComponent<Health>();
                    if (health != null)
                    {
                        // ������ �ֱ�
                        health.TakeDamage(projectileInfo.attack);
                    }
                }
            }
        }

        // Ÿ�ٸ� ����
        protected void HitOnTarget()
        {
            // Health ������Ʈ ����
            Health health = target.GetComponent<Health>();
            // ������ �ֱ�
            health.TakeDamage(projectileInfo.attack);
        }


        // ���ݹ��� �����
        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, projectileInfo.attackRange);
        }
    }
}