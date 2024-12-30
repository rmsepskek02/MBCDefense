using UnityEngine;
using System.Collections.Generic; // ����Ʈ �ڷᱸ���� ����ϱ� ���� �߰�

namespace Defend.Enemy
{
    // ���� ���� �����͸� �����ϴ� Ŭ����
    [System.Serializable]
    public class EnemyData
    {
        public GameObject enemyPrefab;      // ������ ������ ������
        public int count;                   // �ش� ������ ���� ����
        public float delayTime;             // ���� ���� ���� ���� �ð�
    }

    // �� ���̺��� �����͸� �����ϴ� Ŭ����
    [System.Serializable]
    public class ListWaveData
    {
        public List<EnemyData> enemies;     // ���̺꿡 ���Ե� ���� ���� �����͸� ����Ʈ�� ����
    }
}
