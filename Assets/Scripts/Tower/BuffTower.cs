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
        public float duration;                   // ���ӽð�
        public float atk = 0f;                   // ���ݷ� ����
        public float armor = 0f;                 // ���� ����
        public float shootDelay = 1f;            // ����� ����
        public float atkRange = 1f;              // ���ݹ��� ����
        public float healthRegen = 1f;           // ü�� ����� ����
        public float manaRegen = 1f;             // ���� ����� ����
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

            // ���� �Ҹ�
            status.UseMana(manaAmount);

            // �� Ÿ�� ȿ�� ����
            foreach (TowerBase tower in towersInRage)
            {
                // ���� Ÿ�� ����
                BuffTower buffTower = tower.GetComponent<BuffTower>();
                // ���� Ÿ���� ������������ �ʵ���
                if (buffTower != null) continue;

                tower.BuffTower(buffContents, false);
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