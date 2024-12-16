using UnityEngine;

public class RockCreationPointGizmo : MonoBehaviour
{
    // Scene �信�� �׵θ��� �׸��� �޼���
    private void OnDrawGizmos()
    {
        // ���� ��ġ�� �������� �׵θ��� ����մϴ�.
        Vector3 center = transform.position;
        Vector3 size = new Vector3(2, 0, 2);

        // ����� ���� ����
        Gizmos.color = new Color(0.9f, 0.9f, 0.9f); // ���� ȸ��

        // �׵θ� �ڽ��� �׸��ϴ�.
        Gizmos.DrawWireCube(center, size);
    }
}
