using Defend.Tower;
using UnityEngine;
using UnityEngine.UI;

namespace Defend.UI
{
    public class UpgradeAndSell : MonoBehaviour
    {
        #region Variables
        //�Ǹ� �� ������ Ÿ���� ����â
        public GameObject PropertiesUI;
        //���׷��̵� �� ������ Ÿ���� ���׷��̵� Ÿ�� ����â
        public GameObject DescriptionUI;
        private BuildManager buildManager;
        //���ù��� Ÿ��
        private TowerXR tower;

        //�⺻ Ÿ�� ���Ű���, �ǸŰ���, HP,MP, Attack, AttackSpeed
        public Upgrade basicText;
        //���׷��̵� Ÿ�� ���� �ؽ�Ʈ, ��ư, �ǸŰ��� �ؽ�Ʈ
        public Upgrade upGradeText;

        //���׷��̵� ��ư
        public Button upgradebutton;
        #endregion

        void Start()
        {
            //�ʱ�ȭ
            buildManager = BuildManager.Instance;
        }

        //�Ű������� ������ Ÿ�� ������ ���´�
        public void ShowTileUI(TowerXR towerXR)
        {
            //���ù��� Ÿ�� ����
            tower = towerXR;
            TowerInfo info = tower.GetComponent<TowerBase>().GetTowerInfo();

            //���׷��̵� ���� ǥ��
            if ((tower.Isupgradeone && tower.Isupgradetwo && tower.currentlevel == 3)|| 
                (tower.Isupgradeone && !tower.Isupgradetwo && tower.currentlevel == 3) 
                || !tower.Isupgradeone && !tower.Isupgradetwo && tower.currentlevel == 3)
            {
                basicText.image.sprite = tower.currentTower[tower.currentindex];
                basicText.name.text = buildManager.buildMenu.boxes[tower.currentindex].name;
                basicText.Buycost.text = "Sell : " + info.cost2.ToString() + " G";
                basicText.Sellcost.text = "";
                basicText.Hp.text = "Hp : " + info.maxHealth.ToString();
                basicText.Mp.text = "Mp : " + info.maxMana.ToString();
                basicText.Attack.text = "Attack : " + info.projectile.attack.ToString();
                basicText.AttackSpeed.text = "AttackSpeed : " + info.projectile.moveSpeed.ToString();
                basicText.UpgradeMoney.text = "";
                PropertiesUI.SetActive(true);
                DescriptionUI.SetActive(false);
            }
            else if((tower.Isupgradeone && !tower.Isupgradetwo && tower.currentlevel == 2) ||
                (!tower.Isupgradeone && !tower.Isupgradetwo && tower.currentlevel == 2))
            {
                //�⺻ �ͷ� �Ǹ� ���� ǥ��
                basicText.image.sprite = tower.currentTower[tower.currentindex];
                basicText.name.text = buildManager.buildMenu.boxes[tower.currentindex].name;
                basicText.Buycost.text = "Buy : " + info.cost1 + " G";
                basicText.Sellcost.text = "Sell : " + info.GetSellCost().ToString() + " G";
                basicText.Hp.text = "Hp : " + info.maxHealth.ToString();
                basicText.Mp.text = "Mp : " + info.maxMana.ToString();
                basicText.Attack.text = "Attack : " + info.projectile.attack.ToString();
                basicText.AttackSpeed.text = "AttackSpeed : " + info.projectile.moveSpeed.ToString();
                basicText.UpgradeMoney.text = "Upgrade : " + info.cost2;
                //���׷��̵� �Ǹ� ���� ǥ��
                upGradeText.image.sprite = tower.currentTower[tower.currentindex + 1];
                upGradeText.name.text = buildManager.buildMenu.boxes[tower.currentindex + 1].name;
                upGradeText.Buycost.text = "Buy : " + tower.upgradetowerInfo.cost2.ToString() + " G";
                upGradeText.Sellcost.text = "Sell : " + tower.upgradetowerInfo.GetSellCost().ToString() + " G";
                upGradeText.Hp.text = "Hp : " + tower.upgradetowerInfo.maxHealth.ToString();
                upGradeText.Mp.text = "Mp : " + tower.upgradetowerInfo.maxMana.ToString();
                upGradeText.Attack.text = "Attack : " + tower.upgradetowerInfo.projectile.attack.ToString();
                upGradeText.AttackSpeed.text = "AttackSpeed : " + tower.upgradetowerInfo.projectile.moveSpeed.ToString();
                PropertiesUI.SetActive(true);
                DescriptionUI.SetActive(true);
            }
            else if (!tower.Isupgradeone && !tower.Isupgradetwo && tower.currentlevel == 1)
            {
                //�⺻ �ͷ� �Ǹ� ���� ǥ��
                basicText.image.sprite = tower.currentTower[tower.currentindex];
                basicText.name.text = buildManager.buildMenu.boxes[tower.currentindex].name;
                basicText.Buycost.text = "Buy : " + info.cost1 + " G";
                basicText.Sellcost.text = "Sell : " + info.GetSellCost().ToString() + " G";
                basicText.Hp.text = "Hp : " + info.maxHealth.ToString();
                basicText.Mp.text = "Mp : " + info.maxMana.ToString();
                basicText.Attack.text = "Attack : " + info.projectile.attack.ToString();
                basicText.AttackSpeed.text = "AttackSpeed : " + info.projectile.moveSpeed.ToString();
                basicText.UpgradeMoney.text = "Upgrade : " + info.cost2;
                //���׷��̵� �Ǹ� ���� ǥ��
                upGradeText.image.sprite = tower.currentTower[tower.currentindex + 1];
                upGradeText.name.text = buildManager.buildMenu.boxes[tower.currentindex + 1].name;
                upGradeText.Buycost.text = "Buy : " + tower.upgradetowerInfo.cost2.ToString() + " G";
                upGradeText.Sellcost.text = "Sell : " + tower.upgradetowerInfo.GetSellCost().ToString() + " G";
                upGradeText.Hp.text = "Hp : " + tower.upgradetowerInfo.maxHealth.ToString();
                upGradeText.Mp.text = "Mp : " + tower.upgradetowerInfo.maxMana.ToString();
                upGradeText.Attack.text = "Attack : " + tower.upgradetowerInfo.projectile.attack.ToString();
                upGradeText.AttackSpeed.text = "AttackSpeed : " + tower.upgradetowerInfo.projectile.moveSpeed.ToString();
                PropertiesUI.SetActive(true);
                DescriptionUI.SetActive(true);
            }
        }
        //���������� UI �Ⱥ��̰� �ϱ�
        public void HidetileUI()
        {
            PropertiesUI.SetActive(false);
            //���ù��� Ÿ�� �ʱ�ȭ
            tower = null;
        }
    }
}