using UnityEngine;

/// <summary>
/// ������Ÿ�� ��� ����
/// ������ Ÿ���� Ȯ�������� ������ ����
/// </summary>
namespace Defend.Tower
{
    public class LaserTower : TowerBase
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