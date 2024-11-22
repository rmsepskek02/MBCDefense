using UnityEngine;

/// <summary>
/// ��ƼŸ�� ��� ����
/// ������ ���� Ÿ�ٵ��� Ȯ�������� �ѹ� ����
/// </summary>
namespace Defend.Tower
{
    public class MultipleTower : TowerBase
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
