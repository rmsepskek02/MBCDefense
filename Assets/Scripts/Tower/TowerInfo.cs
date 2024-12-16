using Defend.Projectile;
using UnityEngine;

/// <summary>
/// Ÿ���� ������ ����
/// </summary>
namespace Defend.Tower
{
    [System.Serializable]
    public class TowerInfo
    {
        public float rotationSpeed = 5f;    // ȸ�� �ӵ�
        public float attackRange = 5f;      // ���� ��Ÿ�
        public float detectDelay = 0.5f;    // Ÿ�� ���� ������
        public float shootDelay = 1.0f;     // �� ������
        public float maxHealth = 200f;      // �ִ� ü��
        public float maxMana = 50f;         // �ִ� ����
        public float armor = 5f;            // ����
        public float healthRegen = 1f;      // ü�� �����
        public float manaRegen = 1f;        // ���� �����
        public float cost1 = 1f;            // �Ǽ���� 1
        public float cost2 = 2f;            // �Ǽ���� 2
        public float cost3 = 3f;            // �Ǽ���� 3
        public float cost4 = 4f;            // �Ǽ���� 4
        public GameObject upgradeTower;     // ���׷��̵� Ÿ�� ������
        public ProjectileInfo projectile;   // �߻�ü ����
        public bool isLock = false;         // �ر� ����

        //�Ǹ� ����
        public float GetSellCost()
        {
            return cost1 / 2;
        }
    }
}
