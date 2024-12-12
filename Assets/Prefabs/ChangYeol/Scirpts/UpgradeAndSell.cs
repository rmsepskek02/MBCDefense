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

            //���׷��̵� ���� ǥ��
            if (buildManager.buildMenu.levelindex == 3 && (buildManager.buildMenu.indexs + 1) % 3 == 0)
            {
                //���� Ÿ�� �Ǹ� ���� ǥ��
                basicText.image.sprite = buildManager.buildMenu.towerSprite[buildManager.buildMenu.indexs];
                basicText.name.text = buildManager.buildMenu.boxes[buildManager.buildMenu.indexs].name;
                basicText.Buycost.text = "Buy : " + tower.towerInfo[buildManager.buildMenu.indexs].cost2.ToString() + " G";
                basicText.Sellcost.text = "Sell : " + tower.towerInfo[buildManager.buildMenu.indexs].GetSellCost().ToString() + " G";
                basicText.Hp.text = "Hp : " + tower.towerInfo[buildManager.buildMenu.indexs].maxHealth.ToString();
                basicText.Mp.text = "Mp : " + tower.towerInfo[buildManager.buildMenu.indexs].maxMana.ToString();
                basicText.Attack.text = "Attack : " + tower.towerInfo[buildManager.buildMenu.indexs].projectile.attack.ToString();
                basicText.AttackSpeed.text = "AttackSpeed : " + tower.towerInfo[buildManager.buildMenu.indexs].projectile.moveSpeed.ToString();
                basicText.UpgradeMoney.text = "";
                PropertiesUI.SetActive(true);
                DescriptionUI.SetActive(false);
            }
            else if (buildManager.buildMenu.levelindex == 1 || (buildManager.buildMenu.levelindex == 2 && !buildManager.IsUpgrade))
            {
                //�⺻ �ͷ� �Ǹ� ���� ǥ��
                basicText.image.sprite = buildManager.buildMenu.towerSprite[buildManager.buildMenu.indexs];
                basicText.name.text = buildManager.buildMenu.boxes[buildManager.buildMenu.indexs].name;
                basicText.Buycost.text = "Buy : " + tower.towerInfo[buildManager.buildMenu.indexs].cost1 + " G";
                basicText.Sellcost.text = "Sell : " + tower.towerInfo[buildManager.buildMenu.indexs].GetSellCost().ToString() + " G";
                basicText.Hp.text = "Hp : " + tower.towerInfo[buildManager.buildMenu.indexs].maxHealth.ToString();
                basicText.Mp.text = "Mp : " + tower.towerInfo[buildManager.buildMenu.indexs].maxMana.ToString();
                basicText.Attack.text = "Attack : " + tower.towerInfo[buildManager.buildMenu.indexs].projectile.attack.ToString();
                basicText.AttackSpeed.text = "AttackSpeed : " + tower.towerInfo[buildManager.buildMenu.indexs].projectile.moveSpeed.ToString();
                basicText.UpgradeMoney.text = "Upgrade : " + tower.towerInfo[buildManager.buildMenu.indexs].cost2;
                //���׷��̵� �Ǹ� ���� ǥ��
                upGradeText.image.sprite = buildManager.buildMenu.towerSprite[buildManager.buildMenu.indexs + 1];
                upGradeText.name.text = buildManager.buildMenu.boxes[buildManager.buildMenu.indexs + 1].name;
                upGradeText.Buycost.text = "Buy : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].cost2.ToString() + " G";
                upGradeText.Sellcost.text = "Sell : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].GetSellCost().ToString() + " G";
                upGradeText.Hp.text = "Hp : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].maxHealth.ToString();
                upGradeText.Mp.text = "Mp : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].maxMana.ToString();
                upGradeText.Attack.text = "Attack : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].projectile.attack.ToString();
                upGradeText.AttackSpeed.text = "AttackSpeed : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].projectile.moveSpeed.ToString();
                PropertiesUI.SetActive(true);
                DescriptionUI.SetActive(true);
            }
            else if (buildManager.buildMenu.levelindex == 2 && buildManager.IsUpgrade)
            {
                //�⺻ �ͷ� �Ǹ� ���� ǥ��
                basicText.image.sprite = buildManager.buildMenu.towerSprite[buildManager.buildMenu.indexs + 1];
                basicText.name.text = buildManager.buildMenu.boxes[buildManager.buildMenu.indexs + 1].name;
                basicText.Buycost.text = "Buy : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].cost1 + " G";
                basicText.Sellcost.text = "Sell : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].GetSellCost().ToString() + " G";
                basicText.Hp.text = "Hp : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].maxHealth.ToString();
                basicText.Mp.text = "Mp : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].maxMana.ToString();
                basicText.Attack.text = "Attack : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].projectile.attack.ToString();
                basicText.AttackSpeed.text = "AttackSpeed : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].projectile.moveSpeed.ToString();
                basicText.UpgradeMoney.text = "Upgrade : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].cost2;
                //���׷��̵� �Ǹ� ���� ǥ��
                upGradeText.image.sprite = buildManager.buildMenu.towerSprite[buildManager.buildMenu.indexs + 2];
                upGradeText.name.text = buildManager.buildMenu.boxes[buildManager.buildMenu.indexs + 2].name;
                upGradeText.Buycost.text = "Buy : " + tower.towerInfo[buildManager.buildMenu.indexs + 2].cost2.ToString() + " G";
                upGradeText.Sellcost.text = "Sell : " + tower.towerInfo[buildManager.buildMenu.indexs + 2].GetSellCost().ToString() + " G";
                upGradeText.Hp.text = "Hp : " + tower.towerInfo[buildManager.buildMenu.indexs + 2].maxHealth.ToString();
                upGradeText.Mp.text = "Mp : " + tower.towerInfo[buildManager.buildMenu.indexs + 2].maxMana.ToString();
                upGradeText.Attack.text = "Attack : " + tower.towerInfo[buildManager.buildMenu.indexs + 2].projectile.attack.ToString();
                upGradeText.AttackSpeed.text = "AttackSpeed : " + tower.towerInfo[buildManager.buildMenu.indexs + 2].projectile.moveSpeed.ToString();
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