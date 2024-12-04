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
    // TODO :: �÷��̾ Ȱ��ȭ ���θ� �����ϴ� ���
    // => off�� ��� ��� ����, on�� ��� ��� Ȱ��ȭ
    public class DebuffTower : MultipleTower
    {
        private HashSet<Transform> attackedTargets = new HashSet<Transform>(); // �̹� ������ Ÿ�� ����
        [SerializeField] private float manaAmount;
        [SerializeField] private bool isOn => status.CurrentMana >= manaAmount;
        [SerializeField] private GameObject effectObj;
        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
            ActivatedEffect();
            InvokeRepeating(nameof(CleanUpAttackedTargets), 0f, 3f);
        }
        protected override void Shoot()
        {
            // �� �����̿� Ÿ�� ��ȿ�� �˻�
            if (towerInfo.shootDelay >= shootTime) return;

            // ��ȿ�� Ÿ�ٵ� ��������
            var allTargets = UpdateTargets();

            // ��ȿ�� Ÿ���� ������ ����
            if (allTargets.Count == 0) return;

            // �������� ���� Ÿ�ٸ� ������� �߻�ü ����
            foreach (var target in allTargets)
            {
                // �̹� ������ ������, �Ҹ� �������� ���� ������ ���
                if (!attackedTargets.Contains(target) && status.CurrentMana >= manaAmount)
                {
                    // ���� �Ҹ�
                    status.UseMana(manaAmount);

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

        // Ȱ��ȭ ������ Effect On/Off
        private void ActivatedEffect()
        {
            effectObj.SetActive(isOn);
        }

        // null �Ǵ� Missing ������ Ÿ�� ����
        private void CleanUpAttackedTargets()
        {
            attackedTargets.RemoveWhere(target => target == null);
        }
    }
}