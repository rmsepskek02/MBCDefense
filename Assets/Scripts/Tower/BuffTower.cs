using Defend.Utillity;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����Ÿ�� ��� ����
/// �ֺ� �Ʊ� Ÿ���� �ɷ�ġ�� �׼� ��Ű�� Ÿ��
/// </summary>
namespace Defend.Tower
{
    // Buff �� ��ҵ�
    [System.Serializable]
    public class BuffContents
    {
        public float duration;              // ���ӽð�
        public float atk;                   // ���ݷ� ����
        public float armor;                 // ���� ����
        public float shootDelay;            // ����� ����
        public float atkRange;              // ���ݹ��� ����
        public float healthRegen;           // ü�� ����� ����
        public float manaRegen;             // ���� ����� ����
    }

    public class BuffTower : TowerBase
    {
        [SerializeField] private TowerBase[] towers;
        [SerializeField] private float manaAmount;
        [SerializeField] private bool isOn => status.CurrentMana >= manaAmount;
        [SerializeField] private GameObject effectObj;
        [SerializeField] protected BuffContents buffContents;
        protected override void Start()
        {
            status = GetComponent<Status>();

            status.Init(towerInfo);

            shootTime = towerInfo.shootDelay;
        }

        protected override void Update()
        {
            shootTime += Time.deltaTime;
            ActivatedEffect();
            DoBuffForTower();
        }

        // Ÿ���鿡�� ���� ����
        protected void DoBuffForTower()
        {
            // �� ������ & ���� �˻�
            if (towerInfo.shootDelay >= shootTime || isOn == false) return;

            // ���� �� towers ����
            List<TowerBase> towersInRage = new List<TowerBase>();

            // ��� TowerBase�� ã��
            towers = FindObjectsByType<TowerBase>(FindObjectsSortMode.None);
            foreach (TowerBase tower in towers)
            {
                // �Ÿ� üũ
                float distance = Vector3.Distance(transform.position, tower.transform.position);

                // towers �� ���� ���� �ִ� TowerBase��List�� �߰�
                if (distance <= towerInfo.attackRange)
                {
                    towersInRage.Add(tower);
                }
            }

            // �� Ÿ�� ȿ�� ����
            foreach (TowerBase tower in towersInRage)
            {
                tower.BuffTower(buffContents);
                //TODO ȿ�� ����Ʈ ����
            }

            // �� Ÿ�� �ʱ�ȭ
            shootTime = 0;
        }

        // Ȱ��ȭ ������ Effect On/Off
        private void ActivatedEffect()
        {
            effectObj.SetActive(isOn);
        }
    }
}