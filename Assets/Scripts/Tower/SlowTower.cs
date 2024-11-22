using UnityEngine;

/// <summary>
/// ���ο�Ÿ�� ��� ����
/// ���� �� �� ������ �̵��ӵ� ����
/// </summary>
namespace Defend.Tower
{
    public class SlowTower : TowerBase
    {
        protected override void Start()
        {
            base.Start();
            towerInfo.attackRange = 7f;
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}