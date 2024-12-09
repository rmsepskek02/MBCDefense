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
namespace Defend.Player
{
    /// <summary>
    /// Castle���� ����(���׷��̵�)
    /// </summary>
    public class CastleUpgrade : MonoBehaviour
    {
        #region Variables
        //����
        HealthBasedCastle HealthBasedCastle;
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
        private bool isPotalActive = false;
        private bool isMoveSpeedUp = false;
        private bool isMoneyUp = false;
        private bool isTreeUp = false;
        private bool isRockUp = false;
        private bool isAutoGain = false;
        //��ư ��Ȱ��ȭ
        public Button[] btnList;

        //�̵��ӵ� ����
        public DynamicMoveProvider dynamicMove;

        //���
        //private float upgradeCost1 = 100f;  //�ӽÿ�
        private float CostFullHP = 100f;    //hpȸ��
        private float[] CostHPUpgrade = { 100f, 250f, 500f };
        private float[] CostHPTimeUpgrade = { 250f, 500f, 1000f };
        //private float[] CostArmorUpgrade = { 250f, 500f, 1000f }; //����
        //private float CostHPTimeUpgrade = 100f
        //private float CostHPTimeUpgrade = 100f
        //private float CostHPTimeUpgrade = 100f
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
        private float[] IncreaseMoneyGainUpgrade = {1.2f,1.5f,2.0f };
        private float[] IncreaseTreeGainUpgrade = { 1.2f, 1.5f, 2.0f };
        private float[] increaseRockGainUpgrade = { 1.2f, 1.5f, 2.0f };
        //private float[] increaseRockGainUpgrade = { 1.2f, 1.5f, 2.0f };
        //private float[] increaseRockGainUpgrade = { 1.2f, 1.5f, 2.0f };
        //private float[] increaseRockGainUpgrade = { 1.2f, 1.5f, 2.0f };
        //private float[] increaseRockGainUpgrade = { 1.2f, 1.5f, 2.0f };

        //���׷��̵� �ܰ�
        private int currentHPUpgradeLevel = 0; //hp
        private int currentHPTimeUpgradeLevel = 0; //ü��
        private int currentMoneyGainUpgradeLevel = 0;
        private int currentTreeGainUpgradeLevel = 0;
        private int currentRockGainUpgradeLevel = 0;
       /* private int currentRockGainUpgradeLevel = 0;
        private int currentRockGainUpgradeLevel = 0;
        private int currentRockGainUpgradeLevel = 0;
        private int currentRockGainUpgradeLevel = 0;*/

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


            UpdateButtonStates();

        }

        private void Update()
        {

            PriceUpdate();
            test1.text = $"curent HP = {health.CurrentHealth}";
            test2.text = $"hpmax ={health.maxHealth}";
            test3.text = $"hp rex={health.RgAmount}";
            test4.text = $"curent money = {playerState.money}";
        }

        //ü�� ��� ȸ��
        public void FullonHP()
        {
            //Debug.Log("fullonhp spend money");
            //100�� �Ҹ�� Ǯ�� //Ǯ���϶� ���� ���� 
            if (health != null && health.CurrentHealth != health.maxHealth && playerState.SpendMoney(CostFullHP))
            {
                health.CurrentHealth = health.maxHealth;
                playerState.SpendMoney(100f);
                UpdateButtonStates();
            }

        }

        //ü�� �ø���
        public void HPUpgrade()
        {
            //Debug.Log("HPUpgrade spend money");
            if (currentHPUpgradeLevel < CostHPUpgrade.Length && health != null && health.maxHealth < 1000f && playerState.SpendMoney(CostHPUpgrade[currentHPUpgradeLevel]))
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
            if (health != null && currentHPTimeUpgradeLevel < IncreaseHPTimeUpgrade.Length && RgAmount < 5f)
            {
                float cost = CostHPTimeUpgrade[currentHPTimeUpgradeLevel]; 
                if (playerState.SpendMoney(cost))
                {
                    RgAmount++;
                    health.HPTime(RgAmount, IncreaseHPTimeUpgrade[currentHPTimeUpgradeLevel]);
                    currentHPTimeUpgradeLevel++;
                    UpdateButtonStates();
                }
            }
        }

        //�ڿ�ŉ�淮
        public void MoneyGain()
        {
            //Debug.Log("MoneyGain spend money");
            if (!isMoneyUp && currentMoneyGainUpgradeLevel < CostMoneyGainUpgrade.Length && playerState.SpendMoney(CostMoneyGainUpgrade[currentMoneyGainUpgradeLevel]))
            {
                ResourceManager.Instance.UpgradeResourceGain("money", IncreaseMoneyGainUpgrade[currentMoneyGainUpgradeLevel]); // ������ ����
                currentMoneyGainUpgradeLevel++;
                isMoneyUp = true; // MoneyGain ���׷��̵� Ȱ��ȭ
                UpdateButtonStates();
            }
        }
        public void TreeGain()
        {
            //Debug.Log("TreeGain spend money");
            if (!isTreeUp && currentTreeGainUpgradeLevel < CostTreeGainUpgrade.Length && playerState.SpendMoney(CostTreeGainUpgrade[currentTreeGainUpgradeLevel]))
            {
                ResourceManager.Instance.UpgradeResourceGain("tree", IncreaseTreeGainUpgrade[currentTreeGainUpgradeLevel]); // ������ ����
                currentTreeGainUpgradeLevel++;
                isTreeUp = true; // TreeGain ���׷��̵� Ȱ��ȭ
                UpdateButtonStates();
            }
        }
        public void RockGain()
        {
            //Debug.Log("RockGain spend money");
            if (!isRockUp && currentRockGainUpgradeLevel < CostRockGainUpgrade.Length && playerState.SpendMoney(CostRockGainUpgrade[currentRockGainUpgradeLevel]))
            {
                ResourceManager.Instance.UpgradeResourceGain("rock", increaseRockGainUpgrade[currentRockGainUpgradeLevel]); // ������ ����
                currentRockGainUpgradeLevel++;
                isRockUp = true; // RockGain ���׷��̵� Ȱ��ȭ
                UpdateButtonStates();
            }
        }

        //�÷��̾� �̵��ӵ���(�Ź߸��)
        public void MoveSpeed()
        {
            //Debug.Log("MoveSpeed spend money");
            if (isMoveSpeedUp == false && playerState.SpendMoney(CostMoveSpeedUpgrade))
            {
                dynamicMove.moveSpeed = 10f;
                playerState.SpendMoney(100f);
                isMoveSpeedUp = true;
                UpdateButtonStates();
            }

        }

        //��Ż Ȱ��ȭ
        public void PotalActivate()
        {
            //Debug.Log("PotalActivate spend money");
            if (isPotalActive == false && playerState.SpendMoney(CostPotalActivateUpgrade))
            {
                for (int i = 0; i < potalsColor.Length; i++)
                {
                    potalsColor[i].material.color = Color.red;
                    potalsEffect[i].SetActive(true);
                }
                XRSimpleInteractable xRSimpleInteractable = potal.GetComponent<XRSimpleInteractable>();
                xRSimpleInteractable.enabled = true;
                playerState.SpendMoney(100f);
                isPotalActive = true;
                UpdateButtonStates();
            }

        }

        //������ ŉ�� ���� ����
        public void AutoGain()
        {
            ResourceManager.speed = 20f;
            ResourceManager.distance = 500f;
        }

        //ĳ�� ���� ���׷��̵�
        public void ArmorUpgrade()
        {

        }

        //Ÿ�� ���ݷ� ���׷��̵�
        public void TowerATKUpgrade()
        {

        }
        //Ÿ�� ���ݼӵ� ���׷��̵�
        public void TowerATKSpeedUpgrade()
        {

        }
        //Ÿ�� ���� ���� ���׷��̵�
        public void TowerATKRangeUpgrade()
        {

        }




        //��ư ���� ������Ʈ
        private void UpdateButtonStates()
        {
            
            btnList[0].interactable = playerState.money >= CostFullHP && health.CurrentHealth != health.maxHealth; // FullHP ��ư
            btnList[1].interactable = currentHPUpgradeLevel < CostHPUpgrade.Length && health.maxHealth < 1000f && playerState.money > (CostHPUpgrade[currentHPUpgradeLevel]); // HPUpgrade ��ư
            btnList[2].interactable = currentHPTimeUpgradeLevel < CostHPTimeUpgrade.Length && RgAmount < 5f && playerState.money > CostHPTimeUpgrade[currentHPTimeUpgradeLevel]; // HPTimeUpgrade ��ư
            btnList[3].interactable = currentMoneyGainUpgradeLevel < CostMoneyGainUpgrade.Length && !isMoneyUp && playerState.money >= CostMoneyGainUpgrade[currentMoneyGainUpgradeLevel]; // MoneyGain ��ư
            btnList[4].interactable = currentTreeGainUpgradeLevel < CostTreeGainUpgrade.Length && !isTreeUp && playerState.money >= CostTreeGainUpgrade[currentTreeGainUpgradeLevel]; // TreeGain ��ư
            btnList[5].interactable = currentRockGainUpgradeLevel < CostRockGainUpgrade.Length && !isRockUp && playerState.money >= CostRockGainUpgrade[currentRockGainUpgradeLevel]; // RockGain ��ư
            btnList[6].interactable = playerState.money >= CostMoveSpeedUpgrade && !isMoveSpeedUp; // MoveSpeed ��ư
            btnList[7].interactable = playerState.money >= CostPotalActivateUpgrade && !isPotalActive; // PotalActivate ��ư
            btnList[8].interactable = playerState.money >= CostAutoGainUpgrade && !isAutoGain; //AutoGain ��ư
        }

        //���� ������Ʈ 
        void PriceUpdate()
        {
            PriceTexts[0].text = $"{CostFullHP} G";
            PriceTexts[1].text = GetUpgradePriceText(currentHPUpgradeLevel, CostHPUpgrade);
            PriceTexts[2].text = GetUpgradePriceText(currentHPTimeUpgradeLevel, CostHPTimeUpgrade);
            PriceTexts[3].text = GetUpgradePriceText(currentMoneyGainUpgradeLevel, CostMoneyGainUpgrade);
            PriceTexts[4].text = GetUpgradePriceText(currentTreeGainUpgradeLevel, CostTreeGainUpgrade);
            PriceTexts[5].text = GetUpgradePriceText(currentRockGainUpgradeLevel, CostRockGainUpgrade);
            PriceTexts[6].text = $"{CostMoveSpeedUpgrade} G";
            PriceTexts[7].text = $"{CostPotalActivateUpgrade} G";
            PriceTexts[8].text = $"{CostAutoGainUpgrade} G";
        }

        // ���׷��̵� ���� �ؽ�Ʈ�� ��ȯ�ϴ� �޼���
        private string GetUpgradePriceText(int upgradeLevel, float[] costArray)
        {
            return upgradeLevel < costArray.Length ? $"{costArray[upgradeLevel]} G" : "Max";
        }
    }
}