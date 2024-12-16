using UnityEngine;

public class TreeCreationPointGizmo : MonoBehaviour
{
    // Scene �信�� �׵θ��� �׸��� �޼���
    private void OnDrawGizmos()
    {
        // ���� ��ġ�� �������� �׵θ��� ����մϴ�.
        Vector3 center = transform.position;
        Vector3 size = new Vector3(1, 0, 1);

        // ����� ���� ����
        Gizmos.color = Color.green;

        // �׵θ� �ڽ��� �׸��ϴ�.
        Gizmos.DrawWireCube(center, size);
    }
}
