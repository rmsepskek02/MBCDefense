using UnityEngine;

/// <summary>
/// ����Ÿ�� ��� ����
/// �ֺ� �Ʊ� Ÿ���� �ɷ�ġ�� �׼� ��Ű�� Ÿ��
/// </summary>
namespace Defend.Tower
{
    public class BuffTower : TowerBase
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