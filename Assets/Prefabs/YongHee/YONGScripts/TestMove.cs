using UnityEngine;

public class TestMove : MonoBehaviour
{
    #region Variables
    public float speed = 2f; // �̵� �ӵ�
    public Transform[] wayPoints;
    private int currentWaypointIndex = 0; // ���� �̵� ���� ��������Ʈ �ε���
    private Vector3 targetPosition; // ���� ��ǥ ��ġ
    #endregion
    void Start()
    {
        if (wayPoints.Length > 0)
        {
            // ù ��° ��������Ʈ�� ��ǥ ��ġ�� ����
            targetPosition = wayPoints[currentWaypointIndex].position;
        }
    }

    void Update()
    {
        if (wayPoints.Length == 0)
            return; // ��������Ʈ�� ������ ������Ʈ �ߴ�

        // ������Ʈ�� ��ǥ ��ġ�� �̵�
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // ��ǥ ��ġ�� �����ߴ��� Ȯ��
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // ���� ��������Ʈ�� �̵� (������ ��������Ʈ�� �Ѿ�� ù ��° ��������Ʈ�� ��ȯ)
            currentWaypointIndex = (currentWaypointIndex + 1) % wayPoints.Length;
            targetPosition = wayPoints[currentWaypointIndex].position;
        }
    }
}
