using UnityEngine;
/// <summary>
/// �߻�ü�� ������ ����
/// </summary>
namespace Defend.Projectile
{
    [System.Serializable]
    public class ProjectileInfo
    {
        public GameObject prefab;       // �߻�ü ������
        public GameObject effectPrefab; // ����Ʈ ������
        public float effectTime;        // ����Ʈ ���ӽð�
        public float attack;            // ���ݷ�
        public float moveSpeed;         // �̵��ӵ�
    }
}
