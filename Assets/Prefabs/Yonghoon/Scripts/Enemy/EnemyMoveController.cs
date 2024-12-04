using Defend.TestScript;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Defend.Enemy
{
    /// <summary>
    /// Enemy�� �������� �����ϴ� Ŭ����, WayPoint �� ���� ��������, Ratation ����
    /// </summary>
    public class EnemyMoveController : MonoBehaviour
    {
        //�ʵ�
        #region Variable
        [SerializeField] private float baseSpeed = 5f;
        [SerializeField] public float CurrentSpeed { get; private set; }   //�̵� �ӵ�

        // ���ο츦 ������ ��ü�� ������ �����ϴ� ��ųʸ�
        private Dictionary<GameObject, float> moveSources = new Dictionary<GameObject, float>();

        ////�׽�Ʈ��
        //GameObject enemy1 = new GameObject("Enemy1");
        //GameObject enemy2 = new GameObject("Enemy2");
        //GameObject enemy3 = new GameObject("Enemy3");

        private Health health;
        //Arrive���·� ������ animator
        //private Animator animator;

        private Transform target;   //�̵��� ��ǥ����        

        private int wayPointIndex = 0;  //wayPoints �迭�� �����ϴ� �ε���

        private bool isDeath;

        //�̵��ӵ� ��ȭ�� ������ UnityAction
        public UnityAction<float> MoveSpeedChanged;
        public UnityAction EnemyArrive;
        #endregion

        // Start is called before the first frame update
        void Awake()
        {
            //����
            //animator = GetComponent<Animator>();
            health = GetComponent<Health>();

            //�ʱ�ȭ
            CurrentSpeed = baseSpeed;
            isDeath = false;

            //ù��° ��ǥ���� ����
            wayPointIndex = 0;
            target = WayPoints.points[wayPointIndex];

            //Unity Action
            health.OnDie += OnDie;
        }

        private void Update()
        {
            if (isDeath) return;
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
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, (Time.deltaTime * CurrentSpeed) * 2);
            }
            //transform.LookAt(target);         (LookAt�� �̿��ؼ� �ٷ� ȸ��)

            //�̵�
            transform.Translate(dir.normalized * Time.deltaTime * CurrentSpeed, Space.World);

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

        public void ChangedMoveSpeed(GameObject source, float rate)
        {
            if (moveSources.ContainsKey(source))
            {
                // �̹� ������ ��ü�� ������ ���, ���ο� ������ ����
                moveSources[source] = rate;
            }
            else
            {
                // ���ο� ��ü��� �߰�
                moveSources.Add(source, rate);
            }

            // ���� �ӵ� ���
            UpdateCurrentSpeed();
        }

        public void RemoveMoveSource(GameObject source)
        {
            if (moveSources.ContainsKey(source))
            {
                // ���ο� ��ü ����
                moveSources.Remove(source);

                // ���� �ӵ� ���
                UpdateCurrentSpeed();
            }
        }

        private void UpdateCurrentSpeed()
        {
            float currentSpeed = baseSpeed;

            // ���� ���: ���� �ӵ��� �������� ���ο� ȿ�� ����
            foreach (var rate in moveSources.Values)
            {
                currentSpeed -= currentSpeed * -rate; // ���� ����
            }

            CurrentSpeed = currentSpeed; // ���� �ӵ� ������Ʈ

            Debug.Log("���� �ӵ� = " + CurrentSpeed);

            // �ӵ� ���� �̺�Ʈ ȣ��
            MoveSpeedChanged?.Invoke(CurrentSpeed);
        }

        //�̵��ӵ� �����
        //public void ChangedMoveSpeed(float rate)
        //{


        //    float checkChangeSpeed = baseSpeed - (baseSpeed * (1.0f + rate));
        //    Debug.Log("�ٲ�� �� = " + checkChangeSpeed);
        //    CurrentSpeed -= checkChangeSpeed;
        //    Debug.Log("���� �̼� = " + CurrentSpeed);

        //    MoveSpeedChanged?.Invoke(CurrentSpeed);
        //}

        //��ǥ���� ���� ó��
        void Arrive()
        {
            GetComponent<Animator>().SetBool("IsArrive", true);
            EnemyArrive?.Invoke();
            // Update�� ���߱� ���� ������Ʈ ��Ȱ��ȭ
            enabled = false;
        }

        private void OnDie()
        {
            isDeath = true;
        }
    }
}