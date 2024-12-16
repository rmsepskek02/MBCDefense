using UnityEngine;
using System.Collections;

public class RockSpawner : MonoBehaviour
{
    // ������ų ������
    public GameObject rockPrefab;

    // ������ų ��ġ �ټ� ����
    private Transform[] spawnPosition = new Transform[4];

    // �ӽ� ������ȣ
    private int tempIndex;

    // �ߺ�üũ �Ϸ��� ������ȣ
    private int[] randomIndex = new int[4];

    /*// �ڽ� ��ũ��Ʈ ����
    private DetectionCheck detectionCheck;

    // 
    private float randomCooldown;*/


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*// �ڽ� ��ũ��Ʈ ����
        detectionCheck = GetComponentInChildren<DetectionCheck>();*/

        // �ڵ����� �ڽĵ� ������ ����
        for (int i = 0; i < spawnPosition.Length; i++)
        {
            spawnPosition[i] = transform.GetChild(i).transform;
        }


        // ù��° �ε����� �ٷ� �Ҵ�
        randomIndex[0] = Random.Range(0, 4);

        // �ι�° �ε������ʹ� �ߺ��˻�
        for (int i = 1; i < 4; i++)
        {
            do
            {
                tempIndex = Random.Range(0, 4);

                // �ߺ����� Ȯ��
                bool isDuplicate = false;
                for (int j = 0; j < i; j++) // �̹� �Ҵ�� ���� ��
                {
                    if (tempIndex == randomIndex[j])
                    {
                        isDuplicate = true;
                        break;
                    }
                }

                // �ߺ��� �ƴ϶�� �ݺ� ����
                if (!isDuplicate)
                    break;

            } while (true); // �ߺ��̸� ��� ���� �� ����

            // �ߺ��˻� ����ϸ� �Ҵ�
            randomIndex[i] = tempIndex;
        }

        StartCoroutine(SpawnTrees());

    }

    // Update is called once per frame
    void Update()
    {
        /*// �ݶ��̴� �ȿ�
        if (detectionCheck.isRockInside == false)
        { 
            
        }*/

    }

    public IEnumerator SpawnTrees()
    {
        for (int i = 0; i < randomIndex.Length; i++)
        {
            yield return new WaitForSeconds(8f);

            Instantiate(rockPrefab, spawnPosition[randomIndex[i]].position, Quaternion.identity);
        }
    }
}