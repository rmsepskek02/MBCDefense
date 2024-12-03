using Defend.TestScript;
using Mono.Cecil;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using Defend.Interactive;
using TMPro;
using UnityEngine.Playables;
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
        private float upgradeCost1 = 100f;  //�ӽÿ�
        //private float CostFullHP = 100f;    //hpȸ��
        private float[] CostHPUpgrade = { 100f, 250f, 500f };
        //private float[] CostHPTimeUpgrade = { 250f, 500f, 1000f };
        //private float[] CostMoneyGainUpgrade = { 100f, 250f, 500f };
        //private float[] CostTreeGainUpgrade = { 100f, 250f, 500f };
        //private float[] CostRockGainUpgrade = { 100f, 250f, 500f };
        //private float CostMoveSpeedUpgrade = 200f;
        //private float CostPotalActivateUpgrade = 300f;
        //private float CostAutoGainUpgrade = 400f;

        //���׷��̵庰 ��ġ������
        private float[] IncreaseHPUpgrade = { 100f, 200f, 300f }; //hp
        //private float[] IncreaseHPTimeUpgrade = { 1f, 3f, 5f }; //�ʴ� 2, 5 ,10
        //private float[] IncreaseMoneyGainUpgrade = { };
        //private float[] IncreaseTreeGainUpgrade = { };
        //private float[] increaseRockGainUpgrade = { };

        //���׷��̵� �ܰ�
        private int currentHPUpgradeLevel = 0; //hp
        //private int currentHPTimeUpgradeLevel = 0; //ü��
        //private int currentMoneyGainUpgrade = 0;
        //private int currentTreeGainUpgrade = 0;
        //private int currentRockGainUpgrade = 0;

        //�׽�Ʈ�� �ؽ�Ʈ
        public TextMeshProUGUI test1;
        public TextMeshProUGUI test2;
        public TextMeshProUGUI test3;
        public TextMeshProUGUI test4;
      
        #endregion
        private void Start()
        {

            //����
            health = castle.GetComponent<Health>();
            playerState = gameObject.GetComponent<PlayerState>();
            //�ʱ�ȭ
            RgAmount = health.RgAmount;


            UpdateButtonStates();

        }

        private void Update()
        {
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
            if (health != null && health.CurrentHealth != health.maxHealth && playerState.SpendMoney(upgradeCost1))
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
            //Debug.Log("HPTimeUpgrade spend money");
            if (health != null && RgAmount < 5f && playerState.SpendMoney(upgradeCost1))
            {
                RgAmount++;
                health.HPTime(RgAmount, 1f);
                UpdateButtonStates();
            }


        }

        //�ڿ�ŉ�淮
        public void MoneyGain()
        {
            //Debug.Log("MoneyGain spend money");
            if (isMoneyUp == false && playerState.SpendMoney(upgradeCost1))
            {
                ResourceManager.Instance.UpgradeResourceGain("money", 1.2f); //20% �� ���
                playerState.SpendMoney(100f);
                isMoneyUp = true;
                UpdateButtonStates();
            }
        }
        public void TreeGain()
        {
            //Debug.Log("TreeGain spend money");
            if (isTreeUp == false && playerState.SpendMoney(upgradeCost1))
            {
                ResourceManager.Instance.UpgradeResourceGain("tree", 1.5f); //50% �� ���
                playerState.SpendMoney(100f);
                isTreeUp = true;
                UpdateButtonStates();
            }
        }
        public void RockGain()
        {
            //Debug.Log("RockGain spend money");
            if (isRockUp == false && playerState.SpendMoney(upgradeCost1))
            {
                ResourceManager.Instance.UpgradeResourceGain("rock", 1.5f); //50% �� ���
                playerState.SpendMoney(100f);
                isRockUp = true;
                UpdateButtonStates();
            }


        }

        //�÷��̾� �̵��ӵ���(�Ź߸��)
        public void MoveSpeed()
        {
            //Debug.Log("MoveSpeed spend money");
            if (isMoveSpeedUp == false && playerState.SpendMoney(upgradeCost1))
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
            if (isPotalActive == false && playerState.SpendMoney(upgradeCost1))
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

        }


        //��ư ���� ������Ʈ
        private void UpdateButtonStates()
        {
            
            btnList[0].interactable = playerState.money > upgradeCost1 && health.CurrentHealth != health.maxHealth; // FullHP ��ư
            btnList[1].interactable = currentHPUpgradeLevel < CostHPUpgrade.Length && health.maxHealth < 1000f && playerState.money > (CostHPUpgrade[currentHPUpgradeLevel]); // HPUpgrade ��ư
            btnList[2].interactable = playerState.money > upgradeCost1 && RgAmount < 5f; // HPTimeUpgrade ��ư
            btnList[3].interactable = playerState.money > upgradeCost1 && !isMoneyUp; // MoneyGain ��ư
            btnList[4].interactable =playerState.money > upgradeCost1 && !isTreeUp; // TreeGain ��ư
            btnList[5].interactable = playerState.money > upgradeCost1 && !isRockUp; // RockGain ��ư
            btnList[6].interactable = playerState.money > upgradeCost1 && !isMoveSpeedUp; // MoveSpeed ��ư
            btnList[7].interactable = playerState.money > upgradeCost1 && !isPotalActive; // PotalActivate ��ư
            btnList[8].interactable = playerState.money > upgradeCost1 && !isAutoGain; //AutoGain ��ư
        }

    }
}