using Defend.Projectile;
using Defend.TestScript;
using Defend.Utillity;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ���ο�Ÿ�� ��� ����
/// ���� �� �� ���ֿ��� ������� �ο��Ͽ� �̵��ӵ� ����
/// </summary>
namespace Defend.Tower
{
    public class SlowTower : MultipleTower
    {
        private HashSet<Transform> attackedTargets = new HashSet<Transform>(); // �̹� ������ Ÿ�� ����

        // TODO :: MultipleTower �� ȣ���ϳ� ? �ֻ������ϳ� ?
        protected override void Start()
        {
            base.Start();
            
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override void Shoot()
        {
            // �� �����̿� Ÿ�� ��ȿ�� �˻�
            if (towerInfo.shootDelay >= shootTime) return;

            // ��ȿ�� Ÿ�ٵ� ��������
            var allTargets = UpdateTargets()
                .Where(target => target != null && target.GetComponent<Health>().CurrentHealth > 0) // ��ȿ�� Ÿ�� ���͸�
                .ToList();

            // ��ȿ�� Ÿ���� ������ ����
            if (allTargets.Count == 0) return;

            // �������� ���� Ÿ�ٸ� ������� �߻�ü ����
            foreach (var target in allTargets)
            {
                if (!attackedTargets.Contains(target))
                {
                    // �߻�ü �ν��Ͻ� ����
                    GameObject projectilePrefab = Instantiate(
                        towerInfo.projectile.prefab,
                        firePoint.transform.position,
                        Quaternion.identity
                    );

                    // �߻�ü �ʱ�ȭ
                    projectilePrefab.GetComponent<ProjectileBase>().Init(towerInfo.projectile, target);

                    // Ÿ���� ���ݵ� ��Ͽ� �߰�
                    attackedTargets.Add(target);

                    // �ִϸ��̼� ���
                    if (animator != null)
                        animator.SetTrigger(Constants.ANIM_SHOOTTRIGGER);
                }
            }

            // �� Ÿ�� �ʱ�ȭ
            shootTime = 0;
        }
    }
}