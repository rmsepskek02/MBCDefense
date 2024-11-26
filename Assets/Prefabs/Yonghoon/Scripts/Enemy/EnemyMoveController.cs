using UnityEngine;

namespace Defend.Enemy
{
    /// <summary>
    /// Enemy�� �������� �����ϴ� Ŭ����, WayPoint �� ���� ��������, Ratation ����
    /// </summary>
    public class EnemyMoveController : MonoBehaviour
    {
        //�ʵ�
        #region Variable
        //Enemy�� �⺻������ �������ִ� Ŭ����
        private EnemyStats enemyBase;

        [SerializeField] private float speed;   //�̵� �ӵ�

        private Transform target;   //�̵��� ��ǥ����        

        private int wayPointIndex = 0;  //wayPoints �迭�� �����ϴ� �ε���
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            //����
            enemyBase = GetComponent<EnemyStats>();

            //�ʱ�ȭ
            speed = enemyBase.baseSpeed;

            //ù��° ��ǥ���� ����
            wayPointIndex = 0;
            target = WayPoints.points[wayPointIndex];
        }

        private void Update()
        {
            Move();
        }

        //�̵�
        public void Move()
        {
            //�̵� :����(dir), Time.deltatiem, speed
            Vector3 dir = target.position - this.transform.position;
            // ��ü�� ��ǥ �������� ȸ��        (Slerp�� �̿��ؼ� õõ�� ȸ��)
            if (dir != Vector3.zero) // Zero ���� Ȯ�� (���� ó��)
            {
                Quaternion lookRotation = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, (Time.deltaTime * speed) * 2);
            }
            //transform.LookAt(target);         (LookAt�� �̿��ؼ� �ٷ� ȸ��)

            //�̵�
            transform.Translate(dir.normalized * Time.deltaTime * speed, Space.World);

            //��������
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance < 0.2f)
            {
                SetNextTarget();
            }
        }

        //���� ��ǥ ���� ����
        void SetNextTarget()
        {
            if (wayPointIndex == WayPoints.points.Length - 1)
            {
                Arrive();
                return;
            }

            wayPointIndex++;
            target = WayPoints.points[wayPointIndex];
        }

        //��ǥ���� ���� ó��
        void Arrive()
        {
            //����ó�� (ex ������ ���� ��)

            //���� ������Ʈ kill
            Debug.Log("Enemy Arrive!");
        }

        //���ο� ȿ�� �����
        public void Slow(float rate)
        {
            speed = enemyBase.baseSpeed * (1.0f - rate);
        }
    }
}