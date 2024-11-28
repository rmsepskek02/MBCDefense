using System;
using System.Collections;
using UnityEngine;

namespace Defend.Enemy
{
    public class SpawnManager : MonoBehaviour
    {
        //�ʵ�
        #region Variable
        //���� ��ġ(���� ��ġ)
        public Transform startPoint;

        //���� �Ŵ���
        public Transform spawnManager;

        //���� Ÿ�̸�
        public float spawnTimer = 5f;
        private float countdown = 0f;

        //�������̸� true, �ƴϸ� false
        [SerializeField] private bool isSpawn;

        //Wave ������ ����
        public WaveData[] waves;
        private int waveCount;  //���̺� ī��Ʈ

        //����ִ� enemy�� ����
        public static int enemyAlive;
        //public int enemyAlive;

        #endregion

        void Start()
        {
            //�ʱ�ȭ - ���۽� ��� �ð�
            countdown = 2f;
            isSpawn = false;
            enemyAlive = 0;
            waveCount = 0;
        }

        void Update()
        {
            //������ (���̺� ���� ���� ���̺� ī��Ʈ�� ���ų� ī��Ʈ�� Ŀ���� ������������
            if (waves.Length <= waveCount) return;

            if (countdown <= 0f) 
            {
                //Ÿ�̸� ���
                StartCoroutine(SpawnWave());

                //�ʱ�ȭ
                countdown = spawnTimer;
            }

            if (isSpawn == false)
            {
                countdown -= Time.deltaTime;
                countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
            }
            //UI            
            //countdownText.text = Mathf.Round(countdown).ToString();

        }

        //�������� ��ġ�� Enemy�� ����
        private void SpawnEnemy(GameObject enemyPrefab)
        {
            Instantiate(enemyPrefab, startPoint.position, Quaternion.identity, spawnManager);
            enemyAlive++;
        }

        //Wave Data�� �°� �� ����
        IEnumerator SpawnWave()
        {
            isSpawn = true;

            //0 -> WaveData�� ������ �߰��� ī��Ʈ���� ����
            WaveData wave = waves[waveCount];

            for (int i = 0; i < wave.count; i++)
            {
                SpawnEnemy(wave.enemyPrefab);
                yield return new WaitForSeconds(wave.delayTime);
            }

            waveCount++;

            isSpawn = false;
        }
    }
}