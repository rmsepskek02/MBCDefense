using Defend.TestScript;
using Defend.Utillity;
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

        private Health health;
        //Arrive���·� ������ animator
        //private Animator animator;

        private Transform target;   //�̵��� ��ǥ����        

        private int wayPointIndex = 0;  //wayPoints �迭�� �����ϴ� �ε���

        private bool isDeath;

        //�̵��ӵ� ��ȭ�� ������ UnityAction
        public UnityAction<float, float> MoveSpeedChanged;
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
                // �̹� ������ ��ü�� ������ ���, ���� ������ ����
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
                // ���� ��ü ����
                moveSources.Remove(source);

                // ���� �ӵ� ���
                UpdateCurrentSpeed();
            }
        }

        private void UpdateCurrentSpeed()
        {
            float totalRate = 0;

            // ��� ���ο� ������ �ջ�
            foreach (var rate in moveSources.Values)
            {
                totalRate += rate;
            }

            //����ȿ���� ���ο� = 66%, ���� = 50%�� ����
            totalRate = Mathf.Clamp(totalRate, -0.66f, 0.5f);

            // �̵� �ӵ� ����
            CurrentSpeed = baseSpeed * (1.0f + totalRate);
            //�̵��ӵ��� baseSpeed�� ������� +-50%�� �ʰ��� �� ����
            //CurrentSpeed = Mathf.Clamp(CurrentSpeed, (baseSpeed / 2), (baseSpeed * 2));

            Debug.Log("���� �ӵ� = " + CurrentSpeed);

            // �ӵ� ���� �̺�Ʈ ȣ��
            MoveSpeedChanged?.Invoke(CurrentSpeed, totalRate);
        }

        //private void UpdateCurrentSpeed()
        //{
        //    float currentSpeed = baseSpeed;

        //    //���� ���: ���� �ӵ��� �������� ���ο� ȿ�� ����
        //    foreach (var rate in moveSources.Values)
        //    {
        //        currentSpeed -= currentSpeed * -rate; // ���� ����
        //    }

        //    CurrentSpeed = currentSpeed; // ���� �ӵ� ������Ʈ

        //    //    //�̵��ӵ��� baseSpeed�� ������� +-50%�� �ʰ��� �� ����
        //    CurrentSpeed = Mathf.Clamp(currentSpeed, (baseSpeed / 2), (baseSpeed * 2));

        //    Debug.Log("���� �ӵ� = " + CurrentSpeed);

        //    // �ӵ� ���� �̺�Ʈ ȣ��
        //    MoveSpeedChanged?.Invoke(CurrentSpeed);
        //}

        //��ǥ���� ���� ó��
        void Arrive()
        {
            GetComponent<Animator>().SetBool(Constants.ENEMY_ANIM_ISARRIVE, true);
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