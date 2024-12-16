using UnityEditor;
using UnityEngine;

public class DetectionCheckGPT : MonoBehaviour
{
    public GameObject resourcePrefab; // ������ �ڿ��� ������
    public Transform spawnPoint; // �ڿ��� ������ ��ġ

    void Start()
    {
        spawnPoint = transform;

        // �ڽ� �ݶ��̴� �ȿ� "Tree"��� �̸��� ������Ʈ�� �ִ��� Ȯ��
        if (!HasTreeInside())
        {
            SpawnResource();
        }
        else
        {
            Debug.Log("Tree ������Ʈ�� �̹� �ֽ��ϴ�. �ڿ��� �������� �ʽ��ϴ�.");
        }
    }

    private bool HasTreeInside()
    {
        // �ڽ��� BoxCollider�� ������
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider == null)
        {
            Debug.LogError("BoxCollider�� �ʿ��մϴ�.");
            return false;
        }

        // �ڽ� �ݶ��̴� ���� ���� ��� �ݶ��̴��� �˻�
        Collider[] colliders = Physics.OverlapBox(boxCollider.bounds.center, boxCollider.bounds.extents, boxCollider.transform.rotation);

        // "Tree"��� �̸��� ������Ʈ�� �ִ��� Ȯ��
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.name == "Tree")
            {
                return true;
            }
        }

        return false;
    }

    private void SpawnResource()
    {
        if (resourcePrefab == null || spawnPoint == null)
        {
            Debug.LogError("resourcePrefab �Ǵ� spawnPoint�� �������� �ʾҽ��ϴ�.");
            return;
        }

        // �ڿ� ����
        Instantiate(resourcePrefab, spawnPoint.position, spawnPoint.rotation);
        Debug.Log("�ڿ��� �����Ǿ����ϴ�.");
    }

    void OnDrawGizmos()
    {
        // BoxCollider�� ������ Gizmo�� ǥ��
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            Gizmos.color = Color.green;
            Gizmos.matrix = Matrix4x4.TRS(boxCollider.transform.position, boxCollider.transform.rotation, boxCollider.transform.lossyScale);
            Gizmos.DrawWireCube(boxCollider.center, boxCollider.size);
        }
    }
}

