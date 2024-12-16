using UnityEngine;
using System.Collections;

public class TreeSpawner3 : MonoBehaviour
{
    // ������ų ������
    public GameObject treePrefab;

    // ������ų ��ġ �ټ� ����
    public Transform[] spawnPosition = new Transform[5];

    // �ӽ� ������ȣ
    private int tempIndex;

    // �ߺ�üũ �Ϸ��� ������ȣ
    private int[] randomIndex = new int[5];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ù��° �ε����� �ٷ� �Ҵ�
        randomIndex[0] = Random.Range(0, 5);

        // �ι�° �ε������ʹ� �ߺ��˻�
        for (int i = 1; i < 5; i++)
        {
            do
            {
                tempIndex = Random.Range(0, 5);

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
        

    }

    public IEnumerator SpawnTrees()
    {
        for (int i = 0; i < randomIndex.Length; i++)
        {
            yield return new WaitForSeconds(5f);

            Instantiate(treePrefab, spawnPosition[randomIndex[i]].position, Quaternion.identity);
        }
    }
}
