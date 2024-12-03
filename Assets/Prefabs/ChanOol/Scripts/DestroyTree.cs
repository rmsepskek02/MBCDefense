using UnityEngine;

public class DestroyTree : MonoBehaviour
{
    // "EnemyWay" Tag�� ���� ������Ʈ�� �浹 �� ����
    void OnCollisionEnter(Collision collision)
    {
        // �浹�� ������Ʈ�� �̸��� "Tree"���� Ȯ��
        if (collision.gameObject.name == "Tree")
        {
            // "Tree" ������Ʈ ����
            Destroy(collision.gameObject);
        }
    }
}
