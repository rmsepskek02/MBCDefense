using UnityEngine;
using System.Collections;

public class DetectionCheck : MonoBehaviour
{
    private BoxCollider boxCollider;

    //private bool treeFound = false;

    //private bool canBuild = false;

    private bool treeSpawned = false; // ������ �����Ǿ����� ���θ� ����

    //private TreeSpawner treeSpawner;

    public GameObject treePrefab;



    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        //treeSpawner = GetComponent<TreeSpawner>();
    }

    void Update()
    {
        // �θ� ������Ʈ�� TreeSpawner ��ũ��Ʈ�� ������������ ����
        //if (treeSpawner.detectionCheckActive == true)
        {
            // �ݶ��̴� �ȿ� �ִ� ��� ������Ʈ ��������
            Collider[] colliders = Physics.OverlapBox(boxCollider.bounds.center, boxCollider.bounds.extents, boxCollider.transform.rotation);

            // "Tree"��� �̸��� ������Ʈ�� �ִ��� Ȯ��
            bool treeDetected = false;
            foreach (var collider in colliders)
            {
                if (collider.gameObject.name=="Tree(Clone)")
                {
                    treeDetected = true;
                    break;
                }
            }

            // ������ ����, ������ ���� �������� �ʾҴٸ� SpawnTree ȣ��
            if (!treeDetected && !treeSpawned)
            {
                treeSpawned = true; // ���� ���� ���� ����
                StartCoroutine(SpawnTree());
            }
            else if(treeDetected) 
            {
                treeSpawned = false; // ������ �߰ߵǸ� ���� ���� �ʱ�ȭ
            }
        }
    }

    public IEnumerator SpawnTree()
    {
        yield return new WaitForSeconds(5f); // 5�� �Ŀ� ���� ����

        if (treePrefab == null)
        {
            Debug.Log("treePrefab == null");
        }
        Instantiate(treePrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(5f);
    }
}

    /*private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name.Contains("Tree"))
        {
            isTreeInside = true;
            Debug.Log(isTreeInside);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Tree"))
        {
            isTreeInside = false;
            Debug.Log(isTreeInside);
        }
    }

    private void OnDestroy()
    {
        isTreeInside = false;
    }*/

