using UnityEngine;

/// <summary>
/// ���÷���Ÿ�� ��� ����
/// ������ Ÿ���� ���� ���� ����
/// </summary>
namespace Defend.Tower
{
    public class SplashTower : TowerBase
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