using System.Collections;
using UnityEngine;

public class RandomTreeSpawn : MonoBehaviour
{
    // �������� ������ ���� ������
    public GameObject treePrefab;
    // ������ �ӵ�
    [SerializeField] private float spawnSpeed = 5f;
    // ������ 10 ���� ���̻� ����X
    [SerializeField] private int maxTrees = 10;
    // ������ �����Ҽ��ִ���
    //[SerializeField] private bool canSpawnTrees = false;
    
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnTrees());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public IEnumerator SpawnTrees()
    {
        for (int i = 0; i < maxTrees; i++)
        {
            yield return new WaitForSeconds(spawnSpeed);

            SpawnTree();
        }
    }

    public void SpawnTree()
    {
        float randomX = Random.Range(-25, 25);
        float randomZ = Random.Range(-25, 25);
        Instantiate(treePrefab, new Vector3(randomX, 0f, randomZ), Quaternion.identity);
    }
}
