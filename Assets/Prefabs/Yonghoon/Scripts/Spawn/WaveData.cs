using UnityEngine;

namespace Defend.Enemy
{
    //Wave Data�� �����ϴ� ����ȭŬ����
    [System.Serializable]
    public class WaveData
    {
        public GameObject enemyPrefab;      //�����Ǵ� Enemy������
        public int count;                   //�����Ǵ� Enemy����
        public float delayTime;             //���������ð�
    }
}