using Defend.UI;
using System.Collections;
using UnityEngine;

namespace Defend.Enemy
{
    public class ListSpawnManager : MonoBehaviour
    {
        #region Variable

        // ���� ��ġ�� �����ϴ� ���� ���� (Transform)
        public Transform startPoint;

        // ������ ���͸� ������ �θ� ������Ʈ
        public Transform spawnManager;

        // ���̺� �� ��� �ð�
        public float spawnTimer = 180f;
        [HideInInspector] public float countdown = 0f; // ���� ���̺� ���۱��� ���� �ð�

        // ���� ���� ������ ���θ� ��Ÿ���� ���� �÷���
        [SerializeField] public static bool isSpawn;
        [SerializeField] private bool onPressSkipBtn;

        // ���̺� �����͸� �迭�� ����
        public ListWaveData[] waves;
        public int waveCount; // ���� ���̺� ��ȣ

        // ���� ����ִ� ������ �� ���� (��� ���̺� ����)
        public static int enemyAlive;

        #endregion

        void Start()
        {
            // �ʱ� ����
            // ù ���̺� ���� ��� �ð� ����
            //countdown = 2f;             //2�ʵڿ� ù ���̺� ����
            countdown = spawnTimer;     //spawnTimer��ŭ ������ ���̺� ����

            isSpawn = false;      // ó���� ���� ���°� �ƴ�
            enemyAlive = 0;       // ���� �� ����ִ� ���ʹ� ����
            waveCount = 0;        // ù ��° ���̺���� ����
        }

        void Update()
        {
            // ��� ���̺긦 �� ���������� ����
            if (waves.Length <= waveCount) return;

            #region Basic Wave Logic (Timer)

            // ī��Ʈ�ٿ��� 0 ���ϰ� �Ǹ� ���ο� ���̺� ����
            if (countdown <= 0f)
            {
                BuildManager.instance.enemy.ShowProUI();
                StartCoroutine(SpawnWave()); // ���̺� ���� �ڷ�ƾ ȣ��
                countdown = spawnTimer;     // ���� ���̺긦 ���� Ÿ�̸� ����
            }

            // ���� ���� �ƴϰ�, ���̺� ���͸� ���� �������, �Ǵ� ��ŵ��ư�� �������� ī��Ʈ�ٿ� ����
            if ((!isSpawn && enemyAlive == 0 )|| onPressSkipBtn)
            {
                countdown -= Time.deltaTime; // Ÿ�̸Ӹ� ����
                countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity); // Ÿ�̸� �� ����
            }

            #endregion

            #region new Wave Logic (Monster Alive Check)

            /*
            // ����ִ� ���Ͱ� ���� ������ �������� ���� ���̺� ����
            if (enemyAlive == 0 && !isSpawn)
            {
                StartCoroutine(SpawnWave());
            }
            */

            #endregion


        }

        // ���� �������� �����ϴ� �Լ�
        private void SpawnEnemy(GameObject enemyPrefab)
        {
            // ������ ��ġ���� ���͸� �����ϰ�, spawnManager �Ʒ��� ��ġ
            Instantiate(enemyPrefab, startPoint.position, Quaternion.identity, spawnManager);

            // ����ִ� ���� �� ����
            enemyAlive++;
        }

        // ���̺긦 �����ϴ� �ڷ�ƾ �Լ�
        IEnumerator SpawnWave()
        {
            isSpawn = true; // ���� ���� ���·� ��ȯ
            onPressSkipBtn = false; // ��ŵ ��ư�� false�� �׻� �ʱ�ȭ
            // ���� ���̺� �����͸� ������
            ListWaveData wave = waves[waveCount];
            // ���̺� ���� ��� ���� �����͸� ��ȸ
            foreach (var enemyData in wave.enemies)
            {
                // �� ������ ������ŭ ����
                for (int i = 0; i < enemyData.count; i++)
                {
                    SpawnEnemy(enemyData.enemyPrefab); // ���� ����
                    yield return new WaitForSeconds(enemyData.delayTime); // ���� ���� �������� ���
                }
            }

            waveCount++; // ���̺� ��ȣ ����
            isSpawn = false; // ���� ���� ���·� ��ȯ
        }

        public void SkipTimer()
        {
            //Enemy�� �ʿ� ��������� ��ŵ���� ����
            if (enemyAlive > 0) return;
            //��ŵ Ȱ��ȭ
            onPressSkipBtn = true;
            //Ÿ�̸� 5�ʵڿ� �����ǵ��� ����
            countdown = 5f;
        }
    }
}
