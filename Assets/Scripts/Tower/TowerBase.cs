using Defend.Enemy;
using Defend.Projectile;
using Defend.Utillity;
using System.Collections.Generic;
using UnityEngine;
using Defend.TestScript;
/*
�⺻Ÿ�� => Ÿ�ٰ���
���÷���Ÿ�� => ��������
��Ƽ����Ÿ�� => Ÿ�ٰ���
������Ÿ�� => Ÿ�ٰ���
���ο�Ÿ�� => ���� ������ ���� �ʰ� ���� ������ �ش� ȿ�� ����
��ȭŸ��   => ���� ������ ���� �ʰ� ���� ������ �ش� ȿ�� ����
���Ÿ��   => ���� ������ ���� �ʰ� ���� ������ �ش� ȿ�� ����
*/
/// <summary>
/// Tower�� �������� ����� ������ ���� Ŭ����
/// </summary>
namespace Defend.Tower
{
    public abstract class TowerBase : MonoBehaviour
    {
        #region Variables
        // Ÿ��
        [SerializeField] List<Transform> targets;           // Ÿ�ٵ�
        public Transform currentTarget;                     // ���� ���� ����� Ÿ��
        #region Layer �� collider ��� => �̻��
        //public List<LayerMask> targetLayerList;             // Ÿ�� ������Ʈ�� ���̾�
        #endregion

        // �߻�
        public Transform firePoint;                         // �߻�ü ������
        [SerializeField] protected float shootTime;         // �� Ÿ�� ī��Ʈ

        // Ÿ�� ����
        [SerializeField] protected TowerInfo towerInfo;

        // ������Ʈ
        protected Animator animator;
        protected Status status;
        #endregion

        #region Variables For Test
        public Color gizmoColor = Color.green;              // ����� ����
        public float sphereRadius;                          // ���� ������
        public float lineLength = 10f;                      // ������ ����

        LineRenderer lineRenderer;                          // ���� ������
        #endregion

        protected virtual void Start()
        {
            // ����
            animator = GetComponent<Animator>();
            status = GetComponent<Status>();

            status.Init(towerInfo);

            #region Layer �� collider ��� => �̻��
            // Layer ����
            //targetLayerList.Add(LayerMask.GetMask(Constants.LAYER_ENEMY));
            //targetLayerList.Add(LayerMask.GetMask(Constants.LAYER_BOSS));
            #endregion

            // ���� �ֱ�� Ÿ�� Ž��
            InvokeRepeating(nameof(SetClosestTarget), 0f, towerInfo.detectDelay);

            #region Test�� ���� �ð�ȭ => LineRenderer �ʱ�ȭ, Gizmo
            {
                // ����� ���� �ʱ�ȭ
                sphereRadius = towerInfo.attackRange;

                // LineRenderer ������Ʈ�� �������ų� �߰�
                lineRenderer = GetComponent<LineRenderer>();
                if (lineRenderer == null)
                    lineRenderer = gameObject.AddComponent<LineRenderer>();

                // LineRenderer �ʱ� ����
                lineRenderer.startWidth = 0.1f;
                lineRenderer.endWidth = 0.1f;
                lineRenderer.positionCount = 2; // �������� ����
                lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // �⺻ ���̴�
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.red;
            }
            #endregion
        }

        protected virtual void Update()
        {
            SetRotationToTarget(); // �� �����Ӹ��� Ÿ���� �ٶ󺸵��� ȸ��
            shootTime += Time.deltaTime;
            //Shoot();

            // TEST
            //DrawLine();            // Ÿ�� �������� ���� �׸���
        }

        // Ÿ���� �������� ȸ��
        protected virtual void SetRotationToTarget()
        {
            if (currentTarget != null)
            {
                // Ÿ�� ����
                Vector3 targetPosition = currentTarget.position;

                // Ÿ���� y ��ǥ�� ���� ������Ʈ�� y ��ǥ�� ����
                targetPosition.y = transform.position.y;

                // ���� ������Ʈ���� Ÿ���� ���ϴ� ���� ���
                Vector3 direction = targetPosition - transform.position;

                // Ÿ���� �ٶ󺸴� ȸ�� ���
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // ������ ȸ�� (Slerp ���)
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, towerInfo.rotationSpeed * Time.deltaTime);

                // ȸ���� ���� �Ϸ�Ǿ����� üũ
                if (Quaternion.Angle(transform.rotation, targetRotation) <= 5.0f)
                {
                    Shoot(); // ȸ�� �Ϸ� �� �� ����
                }
            }
        }

        // ���� ���� Ÿ���� ����
        protected List<Transform> UpdateTargets()
        {
            // ���� Ÿ�� �ʱ�ȭ
            targets.Clear();

            // ��ȯ�� Ÿ�ٵ� = ���� �� Ÿ��
            List<Transform> tempTarget = new List<Transform>();

            // EnemyController�� ���� Object�� ã��
            var enemies = FindObjectsByType<EnemyController>(FindObjectsSortMode.None);

            // Objects �� ���� ���� �ִ� Object�� ��ȯ�� List�� �߰�
            foreach (EnemyController target in enemies)
            {
                // �Ÿ� üũ
                float distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance <= towerInfo.attackRange)
                {
                    tempTarget.Add(target.transform);
                }
            }

            #region Layer �� collider ��� => �̻��
            // targetLayerList���� ���� ���̾ ��Ʈ �������� ����
            //int combinedLayerMask = 0;
            //foreach (var layerMask in targetLayerList)
            //{
            //    combinedLayerMask |= layerMask.value;  // ��Ʈ �������� ���̾� ����
            //}

            // ���� ���� �� Enemy Layer Ž��
            //Collider[] colliders = Physics.OverlapSphere(transform.position, towerInfo.attackRange, combinedLayerMask);

            //foreach (var collider in colliders)
            //{
            //    // Ÿ���� Transform �߰�
            //    tempTarget.Add(collider.transform);
            //}
            #endregion

            return tempTarget;
        }

        // ���� ����� Ÿ�� ����
        void SetClosestTarget()
        {
            // ���� ���� �� Enemy �޾ƿ���
            targets = UpdateTargets();

            // ���� ����� Ÿ�� ã��
            {
                float closestDistance = Mathf.Infinity;
                Transform closestTarget = null;

                foreach (var target in targets)
                {
                    // Ÿ���� ���ų� ü���� 0���Ϸ� �������� �ٸ� Ÿ���� ã����
                    if (target == null || target.GetComponent<Health>().CurrentHealth <= 0) continue;

                    float distance = Vector3.Distance(transform.position, target.position);

                    // Ÿ���� ���� ���� ���� �ְ�, ���� ����� Ÿ������ Ȯ��
                    if (distance <= towerInfo.attackRange && distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestTarget = target;
                    }
                }
                currentTarget = closestTarget;
            }
        }

        // �߻�
        protected virtual void Shoot()
        {
            // �� �����̰� ������ ���, Ÿ���� ü���� 0���� ū ���
            if (towerInfo.shootDelay < shootTime && currentTarget.GetComponent<Health>().CurrentHealth > 0)
            {
                // �߻�ü ����
                GameObject projectilePrefab = Instantiate(towerInfo.projectile.prefab, firePoint.transform.position, Quaternion.identity);
                // Shoot Animation ���
                if (animator != null)
                    animator.SetTrigger(Constants.ANIM_SHOOTTRIGGER);

                // �߻�ü ���� �ʱ�ȭ, �߻�ü�� ���� ����� Ÿ�ټ���, ���� ���� �� Ÿ�ٵ� ����
                projectilePrefab.GetComponent<ProjectileBase>().Init(towerInfo.projectile, currentTarget);

                // �� Ÿ�� �ʱ�ȭ
                shootTime = 0;
            }
        }

        // ���ݹ��� ���� ������
        private void DrawLine()
        {
            // ������ (������Ʈ�� ���� ��ġ)
            lineRenderer.SetPosition(0, transform.position);

            // ���� (������Ʈ�� ���� �������� lineLength��ŭ ������ ��ġ)
            Vector3 endPosition = transform.position + transform.forward * lineLength;
            lineRenderer.SetPosition(1, endPosition);
        }

        // ���ݹ��� �����
        private void OnDrawGizmos()
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere(transform.position, sphereRadius);
        }
    }
}
