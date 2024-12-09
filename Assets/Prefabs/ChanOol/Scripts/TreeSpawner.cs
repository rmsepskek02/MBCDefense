using UnityEngine;
using System.Collections;

public class TreeSpawner : MonoBehaviour
{
    public GameObject treePrefab; // ������ ������Ʈ�� ������
    public float width = 10f; // x�� ���� ũ��
    public float depth = 66f; // z�� ���� ũ��
    private Vector3 currentPosition; // ���� ������Ʈ ��ġ
    [SerializeField] private int maxTrees = 10; // ������ 10 ���� ���̻� ����X
    [SerializeField] private float spawnDelay = 5f; // ���� ���� ������

    void Start()
    {
        // ���� ������Ʈ�� ��ġ�� �����ɴϴ�.
        currentPosition = transform.position;

        StartCoroutine(SpawnTrees());
    }

    public IEnumerator SpawnTrees()
    {
        for (int i = 0; i < maxTrees; i++)
        {
            yield return new WaitForSeconds(spawnDelay);

            SpawnTree();
        }
    }

    public void SpawnTree()
    {
        // ������ x�� ��ġ�� �����մϴ�.
        float randomX = Random.Range(currentPosition.x - width / 2, currentPosition.x + width / 2);
        //Debug.Log($"Random.Range : {randomX}");

        // ������ z�� ��ġ�� �����մϴ�.
        float randomZ = Random.Range(currentPosition.z - depth / 2, currentPosition.z + depth / 2);
        //Debug.Log($"Random.Range : {randomZ}");

        // y�� ��ġ�� ���� ��ġ�� �����ϰ� �����մϴ�.
        float randomY = currentPosition.y;
        //Debug.Log($"Random.Range : {randomY}");

        // ���ο� ���� ��ġ�� �����մϴ�.
        Vector3 spawnPosition = new Vector3(randomX, randomY, randomZ);

        // ���ο� ������Ʈ�� �����մϴ�.
        Instantiate(treePrefab, spawnPosition, Quaternion.identity);
    }

    // Scene �信�� �׵θ��� �׸��� �޼���
    private void OnDrawGizmos()
    {
        // ���� ��ġ�� �������� �׵θ��� ����մϴ�.
        Vector3 center = transform.position;
        Vector3 size = new Vector3(width, 0, depth);

        // ����� ���� ����
        Gizmos.color = Color.green;

        // �׵θ� �ڽ��� �׸��ϴ�.
        Gizmos.DrawWireCube(center, size);
    }
}
