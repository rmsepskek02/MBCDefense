using UnityEngine;
using System.Collections;

public class CrystalSpawner : MonoBehaviour
{
    #region Variables

    public GameObject crystalPrefab;    // ������ ������
    public float spawnInterval = 3f;    // ���� �ֱ�
    public int spawnCount = 3;          // ���� ����
    [SerializeField] private bool isSpawn;   // isSpawn�� true �϶��� ����

    #endregion

    private void Start()
    {
        //Ÿ�̸� ���
        StartCoroutine(SpawnObjectRoutine(spawnInterval));
    }

    private void Update()
    {
        
    }

    private IEnumerator SpawnObjectRoutine(float interval)
    {
        while (true)
        {
            if (isSpawn == false)
            {
                for (int i = 0; i < spawnCount; i++)
                {
                    // ������Ʈ ����
                    Instantiate(crystalPrefab, transform.position, Quaternion.identity);

                    // ���� ������� ���
                    yield return new WaitForSeconds(interval);
                }
                isSpawn = true;
            }
            else
            {
                // �÷��װ� true��� �ٷ� ���� ���������� ���
                yield return null;
            }
        }
    }
}

