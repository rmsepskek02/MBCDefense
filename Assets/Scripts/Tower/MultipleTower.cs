using Defend.Projectile;
using Defend.TestScript;
using Defend.Utillity;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ��ƼŸ�� ��� ����
/// ������ ���� Ÿ�ٵ��� Ȯ�������� �ѹ� ����
/// </summary>
namespace Defend.Tower
{
    public class MultipleTower : TowerBase
    {
        [SerializeField] protected int maxCount;            // �ִ� �߻�ü ���� ����
        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
        }

        List<Transform> GetClosestTargets()
        {
            // Ÿ�� ����� �Ÿ� �������� ����
            var sortedTargets = UpdateTargets()
                .OrderBy(target => Vector3.Distance(transform.position, target.position))          // �Ÿ� ���� ����
                .Take(maxCount)                                                                    // �ִ� maxCount��ŭ ����
                .ToList();

            return sortedTargets;
        }

        // Shoot ������
        protected override void Shoot()
        {
            // �� �����̿� Ÿ�� ��ȿ�� �˻�
            if (towerInfo.shootDelay >= shootTime) return;

            // ���� ����� Ÿ�� 3�� ��������
            var closestTargets = GetClosestTargets();

            // ��ȿ�� Ÿ���� ������ ����
            if (closestTargets.Count == 0) return;

            // �߻�ü ����
            foreach (var target in closestTargets)
            {
                // �߻�ü �ν��Ͻ� ����
                GameObject projectilePrefab = Instantiate(
                    towerInfo.projectile.prefab,
                    firePoint.transform.position,
                    Quaternion.identity
                );

                // �߻�ü �ʱ�ȭ
                projectilePrefab.GetComponent<ProjectileBase>().Init(towerInfo.projectile, target);

                // �ִϸ��̼� ���
                if (animator != null)
                    animator.SetTrigger(Constants.ANIM_SHOOTTRIGGER);
            }

            // �� Ÿ�� �ʱ�ȭ
            shootTime = 0;
        }
    }
}
