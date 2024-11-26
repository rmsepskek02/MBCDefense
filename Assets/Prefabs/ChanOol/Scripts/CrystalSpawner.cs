using UnityEngine;
using System.Collections;

public class CrystalSpawner : MonoBehaviour
{
    #region Variables

    public GameObject crystalPrefab;    // Cave �տ��� ������ ������

    public float spawnDistance = 2f;    // ���� ������Ʈ������ �Ÿ�

    public float spawnInterval = 1f;   // ���� �ֱ�

    private bool isSpawn;

    #endregion

    private void Start()
    {
  
    }

    private void Update()
    {
        //Ÿ�̸� ���
        StartCoroutine(SpawnObjectRoutine());
    }

    

    private IEnumerator SpawnObjectRoutine()
    {
        isSpawn = true;

        if (isSpawn)
        {
            // ���� ������Ʈ �� ��ġ ���
            Vector3 spawnPosition = transform.position + transform.forward * spawnDistance;

            // ������Ʈ ����
            Instantiate(crystalPrefab, spawnPosition, Quaternion.identity);

            // ���� ������� ���
            yield return new WaitForSeconds(spawnInterval);

            isSpawn = false;
        }
  
    }
}

