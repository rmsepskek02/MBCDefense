using Defend.TestScript;
using Mono.Cecil;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using Defend.Interactive;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.PlayerLoop;
using Unity.VisualScripting;
using Defend.Projectile;
using Defend.Tower;
namespace Defend.Player
{
    /// <summary>
    /// Castle���� ����(���׷��̵�)
    /// </summary>
    public class CastleUpgrade : MonoBehaviour
    {
        #region Variables
        //����
        public static BuffContents buffContents = new BuffContents();
        HealthBasedCastle HealthBasedCastle;
        TowerBase[] towerbase;
        PlayerState playerState;
        Health health;
        public GameObject castle;
        //ü���ӵ�
        private float RgAmount;

        //��Ż Ȱ��ȭ
        public GameObject potal;
        public Renderer[] potalsColor;
        public GameObject[] potalsEffect;

        //���� ���ſ���
        public bool isPotalActive = false;
        public bool isMoveSpeedUp = false;
        public bool isAutoGain = false;
        //private bool isMoneyUp = false;
        //private bool isTreeUp = false;
        //private bool isRockUp = false;
        //��ư ��Ȱ��ȭ
        public Button[] btnList;

        //�̵��ӵ� ����
        public DynamicMoveProvider dynamicMove;

        //���
        //private float upgradeCost1 = 100f;  //�ӽÿ�
        private float CostFullHP = 100f;    //hpȸ��
        private float[] CostHPUpgrade = { 100f, 250f, 500f };
        private float[] CostHPTimeUpgrade = { 250f, 500f, 1000f };
        private float[] CostArmorUpgrade = { 250f, 500f, 1000f }; //����

        // Ÿ�� ���׷��̵�
        private float[] CostTowerATKUpgrade = new float[50];
        private float[] CostTowerATKSpeed = new float[50];
        private float[] CostTowerATKRange = new float[50];


        //�ڿ����׷��̵�
        private float[] CostMoneyGainUpgrade = { 100f, 250f, 500f };
        private float[] CostTreeGainUpgrade = { 100f, 250f, 500f };
        private float[] CostRockGainUpgrade = { 100f, 250f, 500f };

        //�������׷��̵�
        private float CostMoveSpeedUpgrade = 200f;
        private float CostPotalActivateUpgrade = 300f;
        private float CostAutoGainUpgrade = 400f;



        //���׷��̵庰 ��ġ������
        private float[] IncreaseHPUpgrade = { 100f, 200f, 300f }; //hp
        private float[] IncreaseHPTimeUpgrade = { 1f, 3f, 5f }; //�ʴ� 2, 5 ,10
        private float[] IncreaseMoneyGainUpgrade = { 1.1f, 1.2f, 1.5f };
        private float[] IncreaseTreeGainUpgrade = { 1.1f, 1.2f, 1.5f };
        private float[] increaseRockGainUpgrade = { 1.1f, 1.2f, 1.5f };
        private float[] increaseArmorUpgrade = { 5f, 10f, 20f };
        //private float increaseTowerATKUpgrade = 1f;
        //private float increaseTowerATKSpeedUpgrade = 1f;
        //private float increaseTowerATKRangeUpgrade = 1f;

        //���׷��̵� �ܰ�
        public int currentHPUpgradeLevel = 0; //hp
        public int currentHPTimeUpgradeLevel = 0; //ü��
        public int currentMoneyGainUpgradeLevel = 0;
        public int currentTreeGainUpgradeLevel = 0;
        public int currentRockGainUpgradeLevel = 0;
        public int currenteArmorUpgradeLevel = 0;
        public int currentTowerATKUpgradeLevel = 0;
        public int currentTowerATKSpeedUpgradeLevel = 0;
        public int currentTowerATKRangeUpgradeLevel = 0;

        //Ÿ�����׷��̵��
        public int atkLevel = 0;
        public int atkSpeedLevel = 0;
        public int atkRangeLevel = 0;

        //�׽�Ʈ�� �ؽ�Ʈ
        public TextMeshProUGUI test1;
        public TextMeshProUGUI test2;
        public TextMeshProUGUI test3;
        public TextMeshProUGUI test4;

        //�� ��� �ؽ�Ʈ
        public TextMeshProUGUI[] PriceTexts;
        #endregion
        private void Start()
        {

            //����
            health = castle.GetComponent<Health>();
            playerState = Object.FindAnyObjectByType<PlayerState>();
            //�ʱ�ȭ
            RgAmount = health.RgAmount;


       
            initializeUpgradeCosts();
    

        }

        private void Update()
        {
            UpdateButtonStates();
            PriceUpdate();
            //UpdateButtonStates();
            test1.text = $"curent HP = {health.CurrentHealth}";
            test2.text = $"hpmax ={health.maxHealth}";
            test3.text = $"hp rex={health.RgAmount}";
            test4.text = $"curent money = {playerState.FormatMoney()}";
        }

        //Ÿ�� ���׷��̵� ���ݺ���
        private void initializeUpgradeCosts()
        {
            float baseCost = 100f; // �⺻ ����
            float increment = 50f;  // ���׷��̵� �� �����ϴ� ����

            for (int i = 0; i < 50; i++)
            {
                CostTowerATKUpgrade[i] = baseCost + (i * increment);
                CostTowerATKSpeed[i] = baseCost + (i * increment);
                CostTowerATKRange[i] = baseCost + (i * increment);
            }
        }

        //ü�� ��� ȸ��
        public void FullonHP()
        {
            //Debug.Log("fullonhp spend money");
            //100�� �Ҹ�� Ǯ�� //Ǯ���϶� ���� ���� 
            if (health != null && health.CurrentHealth != health.maxHealth && playerState.money >= CostFullHP)
            {
                health.CurrentHealth = health.maxHealth;
                playerState.SpendMoney(CostFullHP);
                UpdateButtonStates();
            }

        }

        //ü�� �ø���
        public void HPUpgrade()
        {
            //Debug.Log("HPUpgrade spend money");
            if (currentHPUpgradeLevel < CostHPUpgrade.Length && health != null && health.maxHealth < 1000f && playerState.money >= CostHPUpgrade[currentHPUpgradeLevel])
            {
                // �ܰ躰 ü�� ����
                health.IncreaseMaxHealth(IncreaseHPUpgrade[currentHPUpgradeLevel]);
                playerState.SpendMoney(CostHPUpgrade[currentHPUpgradeLevel]);
                currentHPUpgradeLevel++;
                UpdateButtonStates();
            }

        }

        //ü��ȸ���� 1�ʿ� interval ��ŭ ȸ��
        public void HPTimeUpgrade()
        {
            if (health != null && currentHPTimeUpgradeLevel < IncreaseHPTimeUpgrade.Length && RgAmount < 5f && playerState.money >= CostHPTimeUpgrade[currentHPTimeUpgradeLevel])
            {

                playerState.SpendMoney(CostHPTimeUpgrade[currentHPTimeUpgradeLevel]);

                RgAmount++;
                health.HPTime(RgAmount, IncreaseHPTimeUpgrade[currentHPTimeUpgradeLevel]);
                currentHPTimeUpgradeLevel++;
                UpdateButtonStates();

            }
        }

        //�ڿ�ŉ�淮
        public void MoneyGain()
        {
            //Debug.Log("MoneyGain spend money");
            if (currentMoneyGainUpgradeLevel < CostMoneyGainUpgrade.Length && playerState.money >= CostMoneyGainUpgrade[currentMoneyGainUpgradeLevel])
            {
                playerState.SpendMoney(CostMoneyGainUpgrade[currentMoneyGainUpgradeLevel]);
                ResourceManager.Instance.UpgradeResourceGain("money", IncreaseMoneyGainUpgrade[currentMoneyGainUpgradeLevel]); // ������ ����
                currentMoneyGainUpgradeLevel++;
                //isMoneyUp = true; // MoneyGain ���׷��̵� Ȱ��ȭ
                UpdateButtonStates();
            }
        }
        public void TreeGain()
        {
            //Debug.Log("TreeGain spend money");
            if (currentTreeGainUpgradeLevel < CostTreeGainUpgrade.Length && playerState.money >= CostTreeGainUpgrade[currentTreeGainUpgradeLevel])
            {
                playerState.SpendMoney(CostTreeGainUpgrade[currentTreeGainUpgradeLevel]);
                ResourceManager.Instance.UpgradeResourceGain("tree", IncreaseTreeGainUpgrade[currentTreeGainUpgradeLevel]); // ������ ����
                currentTreeGainUpgradeLevel++;
                //isTreeUp = true; // TreeGain ���׷��̵� Ȱ��ȭ
                UpdateButtonStates();
            }
        }
        public void RockGain()
        {
            //Debug.Log("RockGain spend money");
            if (currentRockGainUpgradeLevel < CostRockGainUpgrade.Length && playerState.money >= CostRockGainUpgrade[currentRockGainUpgradeLevel])
            {
                playerState.SpendMoney(CostRockGainUpgrade[currentRockGainUpgradeLevel]);
                ResourceManager.Instance.UpgradeResourceGain("rock", increaseRockGainUpgrade[currentRockGainUpgradeLevel]); // ������ ����
                currentRockGainUpgradeLevel++;
                //isRockUp = true; // RockGain ���׷��̵� Ȱ��ȭ
                UpdateButtonStates();
            }
        }

        //�÷��̾� �̵��ӵ���(�Ź߸��)
        public void MoveSpeed()
        {
            //Debug.Log("MoveSpeed spend money");
            if (isMoveSpeedUp == false && playerState.money >= CostMoveSpeedUpgrade)
            {
                dynamicMove.moveSpeed = 10f;
                playerState.SpendMoney(CostMoveSpeedUpgrade);
                isMoveSpeedUp = true;
                UpdateButtonStates();
            }

        }

        //��Ż Ȱ��ȭ
        public void PotalActivate()
        {
            //Debug.Log("PotalActivate spend money");
            if (isPotalActive == false && playerState.money >= CostPotalActivateUpgrade)
            {
                for (int i = 0; i < potalsColor.Length; i++)
                {
                    potalsColor[i].material.color = Color.red;
                    potalsEffect[i].SetActive(true);
                }
                XRSimpleInteractable xRSimpleInteractable = potal.GetComponent<XRSimpleInteractable>();
                xRSimpleInteractable.enabled = true;
                playerState.SpendMoney(CostPotalActivateUpgrade);
                isPotalActive = true;
                UpdateButtonStates();
            }

        }

        //������ ŉ�� ���� ����
        public void AutoGain()
        {
            if (isAutoGain == false && playerState.money >= CostAutoGainUpgrade)
            {
                ResourceManager.speed = 10f;
                ResourceManager.distance = 10f;
                playerState.SpendMoney(CostAutoGainUpgrade);
                isAutoGain = true;
            }
        }

        //ĳ�� ���� ���׷��̵�
        public void ArmorUpgrade()
        {
            if (currenteArmorUpgradeLevel < CostArmorUpgrade.Length && health != null && playerState.money >= CostArmorUpgrade[currenteArmorUpgradeLevel])
            {
                health.ChangedArmor(increaseArmorUpgrade[currenteArmorUpgradeLevel]);
                playerState.SpendMoney(CostArmorUpgrade[currenteArmorUpgradeLevel]);
                currenteArmorUpgradeLevel++;
                UpdateButtonStates();
            }

        }

        //Ÿ�� ���ݷ� ���׷��̵�
        public void TowerATKUpgrade()
        {
            towerbase = FindObjectsByType<TowerBase>(FindObjectsSortMode.None);


            if (currentTowerATKUpgradeLevel < CostTowerATKUpgrade.Length && playerState.money >= (CostTowerATKUpgrade[currentTowerATKUpgradeLevel]))
            {
                playerState.SpendMoney(CostTowerATKUpgrade[currentTowerATKUpgradeLevel]);
                currentTowerATKUpgradeLevel++;

                //���ݷ� ��
                if (buffContents != null)
                {
                    buffContents.atk = 0f;
                    buffContents.atk++;
                    atkLevel++;

                }
                foreach (var tower in towerbase)
                {
                    tower.BuffTower(buffContents, true);
                }


                UpdateButtonStates();
            }
        }
        //Ÿ�� ���ݼӵ� ���׷��̵�
        public void TowerATKSpeedUpgrade()
        {
            towerbase = FindObjectsByType<TowerBase>(FindObjectsSortMode.None);
            if (currentTowerATKSpeedUpgradeLevel < CostTowerATKSpeed.Length && playerState.money >= (CostTowerATKSpeed[currentTowerATKSpeedUpgradeLevel]))
            {
                playerState.SpendMoney(CostTowerATKSpeed[currentTowerATKSpeedUpgradeLevel]);
                currentTowerATKSpeedUpgradeLevel++;

                //Ÿ�� ���ݼӵ� ��
                if (buffContents != null)
                {
                    buffContents.shootDelay = 0f;
                    buffContents.shootDelay++;
                    atkSpeedLevel++;

                }
                foreach (var tower in towerbase)
                {
                    tower.BuffTower(buffContents, true);
                }
                UpdateButtonStates();
            }
        }
        //Ÿ�� ���� ���� ���׷��̵�
        public void TowerATKRangeUpgrade()
        {
            towerbase = FindObjectsByType<TowerBase>(FindObjectsSortMode.None);
            if (currentTowerATKRangeUpgradeLevel < CostTowerATKRange.Length && playerState.money >= (CostTowerATKRange[currentTowerATKRangeUpgradeLevel]))
            {
                playerState.SpendMoney(CostTowerATKRange[currentTowerATKRangeUpgradeLevel]);
                currentTowerATKRangeUpgradeLevel++;
                // ���� ���� ���� ���� �߰�             
                if (buffContents != null)
                {
                    buffContents.atkRange = 0f;
                    buffContents.atkRange++;
                    atkRangeLevel++;

                }
                foreach (var tower in towerbase)
                {
                    tower.BuffTower(buffContents, true);
                }
                UpdateButtonStates();
            }
        }




        //��ư ���� ������Ʈ
        private void UpdateButtonStates()
        {
            btnList[0].interactable = playerState.money >= CostFullHP && health.CurrentHealth != health.maxHealth; // FullHP ��ư
            btnList[1].interactable = currentHPUpgradeLevel < CostHPUpgrade.Length && health.maxHealth < 1000f && playerState.money > (CostHPUpgrade[currentHPUpgradeLevel]); // HPUpgrade ��ư
            btnList[2].interactable = currentHPTimeUpgradeLevel < CostHPTimeUpgrade.Length && RgAmount < 5f && playerState.money > CostHPTimeUpgrade[currentHPTimeUpgradeLevel]; // HPTimeUpgrade ��ư
            btnList[3].interactable = currenteArmorUpgradeLevel < CostArmorUpgrade.Length && playerState.money > CostArmorUpgrade[currenteArmorUpgradeLevel]; // �Ƹ�
            btnList[4].interactable = currentTowerATKUpgradeLevel < CostTowerATKUpgrade.Length && playerState.money >= CostTowerATKUpgrade[currentTowerATKUpgradeLevel]; // Ÿ�� ATK ���׷��̵�
            btnList[5].interactable = currentTowerATKSpeedUpgradeLevel < CostTowerATKSpeed.Length && playerState.money >= CostTowerATKSpeed[currentTowerATKSpeedUpgradeLevel]; // Ÿ�� ���� �ӵ� ���׷��̵�
            btnList[6].interactable = currentTowerATKRangeUpgradeLevel < CostTowerATKRange.Length && playerState.money >= CostTowerATKRange[currentTowerATKRangeUpgradeLevel]; // Ÿ�� ���� ���׷��̵�
            btnList[7].interactable = currentMoneyGainUpgradeLevel < CostMoneyGainUpgrade.Length && playerState.money >= CostMoneyGainUpgrade[currentMoneyGainUpgradeLevel]; // MoneyGain ��ư
            btnList[8].interactable = currentTreeGainUpgradeLevel < CostTreeGainUpgrade.Length && playerState.money >= CostTreeGainUpgrade[currentTreeGainUpgradeLevel]; // TreeGain ��ư
            btnList[9].interactable = currentRockGainUpgradeLevel < CostRockGainUpgrade.Length && playerState.money >= CostRockGainUpgrade[currentRockGainUpgradeLevel]; // RockGain ��ư
            btnList[10].interactable = playerState.money >= CostMoveSpeedUpgrade && !isMoveSpeedUp; // MoveSpeed ��ư
            btnList[11].interactable = playerState.money >= CostPotalActivateUpgrade && !isPotalActive; // PotalActivate ��ư
            btnList[12].interactable = playerState.money >= CostAutoGainUpgrade && !isAutoGain; //AutoGain ��ư
        }

        //���� ������Ʈ 
        void PriceUpdate()
        {
            PriceTexts[0].text = $"{CostFullHP} G";
            PriceTexts[1].text = GetUpgradePriceText(currentHPUpgradeLevel, CostHPUpgrade);
            PriceTexts[2].text = GetUpgradePriceText(currentHPTimeUpgradeLevel, CostHPTimeUpgrade);
            PriceTexts[3].text = GetUpgradePriceText(currenteArmorUpgradeLevel, CostArmorUpgrade);
            PriceTexts[4].text = GetUpgradePriceText(currentTowerATKUpgradeLevel, CostTowerATKUpgrade);
            PriceTexts[5].text = GetUpgradePriceText(currentTowerATKSpeedUpgradeLevel, CostTowerATKSpeed);
            PriceTexts[6].text = GetUpgradePriceText(currentTowerATKRangeUpgradeLevel, CostTowerATKRange);

            PriceTexts[7].text = GetUpgradePriceText(currentMoneyGainUpgradeLevel, CostMoneyGainUpgrade);
            PriceTexts[8].text = GetUpgradePriceText(currentTreeGainUpgradeLevel, CostTreeGainUpgrade);
            PriceTexts[9].text = GetUpgradePriceText(currentRockGainUpgradeLevel, CostRockGainUpgrade);
            PriceTexts[10].text = $"{CostMoveSpeedUpgrade} G";
            PriceTexts[11].text = $"{CostPotalActivateUpgrade} G";
            PriceTexts[12].text = $"{CostAutoGainUpgrade} G";
        }

        // ���׷��̵� ���� �ؽ�Ʈ�� ��ȯ�ϴ� �޼���
        private string GetUpgradePriceText(int upgradeLevel, float[] costArray)
        {
            return upgradeLevel < costArray.Length ? $"{costArray[upgradeLevel]} G" : "Max";
        }
    }
}