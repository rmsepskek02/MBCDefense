using System;
using UnityEngine;
/// <summary>
/// Debuff�� �Ŵ� �߻�ü ��� ����
/// </summary>
namespace Defend.Projectile
{
    public class Debuffer<T> : TargetProjectile where T : Component
    {
        [SerializeField] protected T targetComponent;             // ���׸� ������Ʈ
        [SerializeField] protected bool isDebuff = false;         // ����� ����

        // ����� ���� ����
        protected virtual void DoDebuffAction(T component) { }
        // ���󺹱� ����
        protected virtual void UndoDebuffAction(T component) { }

        protected override void Start()
        {
            // ����� ������Ʈ�� ������
            targetComponent = target.GetComponent<T>();
            if (targetComponent == null)
            {
                Debug.Log($"NULL Component!");
            }
        }

        protected override void Update()
        {
            base.Update();
            CheckDistanceFromTower();
        }

        protected override void Hit()
        {

        }

        // ����� ����
        public void ApplyDebuff(Action<T> applyAction)
        {
            applyAction(targetComponent); // ����� ���� ����
            isDebuff = true;
        }

        // ����� ����
        public void RemoveDebuff(Action<T> removeAction)
        {
            removeAction(targetComponent); // ����� ���� ���� ����
            isDebuff = false;
        }

        // Ÿ������ �Ÿ� Ȯ��
        protected virtual void CheckDistanceFromTower()
        {
            if (targetComponent == null) return;

            float distance = Vector3.Distance(transform.position, projectileInfo.tower.transform.position);
            bool inAttackRange = distance <= projectileInfo.attackRange;

            // Ÿ�� ���� ���� �ְ�, ������� ������� �ʾҴٸ�
            if (inAttackRange == true && isDebuff == false)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                ApplyDebuff(DoDebuffAction);
            }
            // Ÿ�� ���� �ۿ� �ְ�, ������� ����Ǿ��ٸ�
            else if (inAttackRange == false && isDebuff == true)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                RemoveDebuff(UndoDebuffAction);
            }
            // Ÿ�� ���� ���� �ְ�, ������� ����Ǿ� �ִ� ���
            // Ÿ�� ���� �ۿ� �ְ�, ������� ������� ���� ���
            else
            {
                //Debug.Log($"����� Ÿ�� ���� �߻� \n inAttackRange = {inAttackRange} \n isDebuff = {isDebuff}");
            }
        }
    }
}
