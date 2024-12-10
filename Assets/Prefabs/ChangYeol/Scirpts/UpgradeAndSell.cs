using Defend.Tower;
using MyFps;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Defend.UI
{
    public class UpgradeAndSell : MonoBehaviour
    {
        #region Variables
        public GameObject PropertiesUI;
        public GameObject DescriptionUI;

        private BuildManager buildManager;
        private TowerInfo towerInfo;
        //���ù��� Ÿ��
        private TowerXR tower;
        public Tile tile;

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
            /*if(tower.towerInfo[buildManager.buildMenu.indexs].projectile.tower == tower.towerInfo[buildManager.buildMenu.indexs].upgradeTower || tile.IsUpgrade)
            {
                //���׷��̵� �Ǹ� ���� ǥ��
                basicText.image.sprite = buildManager.buildMenu.towerSprite[buildManager.buildMenu.indexs];
                basicText.name.text = tower.towerInfo[buildManager.buildMenu.indexs].upgradeTower.ToString();
                basicText.Buycost.text = "Buy : " + tower.towerInfo[buildManager.buildMenu.indexs].cost2.ToString() + " G";
                basicText.Sellcost.text = "Sell : " + tower.towerInfo[buildManager.buildMenu.indexs].GetSellCost().ToString() + " G";
                basicText.Hp.text = "Hp : " + tower.towerInfo[buildManager.buildMenu.indexs].maxHealth.ToString();
                basicText.Mp.text = "Mp : " + tower.towerInfo[buildManager.buildMenu.indexs].maxMana.ToString();
                basicText.Attack.text = "Attack : " + tower.towerInfo[buildManager.buildMenu.indexs].projectile.attack.ToString();
                basicText.AttackSpeed.text = "AttackSpeed : " + tower.towerInfo[buildManager.buildMenu.indexs].projectile.moveSpeed.ToString();
                PropertiesUI.SetActive(true);
                DescriptionUI.SetActive(false);
            }
            else if (tower.towerInfo[buildManager.buildMenu.indexs].projectile.tower != tower.towerInfo[buildManager.buildMenu.indexs].upgradeTower && buildManager.IsUpgrade)
            {
                //�⺻ �ͷ� �Ǹ� ���� ǥ��
                basicText.image.sprite = buildManager.buildMenu.towerSprite[buildManager.buildMenu.indexs+1];
                basicText.name.text = tower.towerInfo[buildManager.buildMenu.indexs + 1].projectile.tower.ToString();
                basicText.Buycost.text = "Buy : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].cost1 + " G";
                basicText.Sellcost.text = "Sell : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].GetSellCost().ToString() + " G";
                basicText.Hp.text = "Hp : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].maxHealth.ToString();
                basicText.Mp.text = "Mp : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].maxMana.ToString();
                basicText.Attack.text = "Attack : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].projectile.attack.ToString();
                basicText.AttackSpeed.text = "AttackSpeed : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].projectile.moveSpeed.ToString();
                //���׷��̵� �Ǹ� ���� ǥ��
                basicText.image.sprite = buildManager.buildMenu.towerSprite[buildManager.buildMenu.indexs + 2];
                upGradeText.name.text = tower.towerInfo[buildManager.buildMenu.indexs + 2].upgradeTower.ToString();
                upGradeText.Buycost.text = "Buy : " + tower.towerInfo[buildManager.buildMenu.indexs + 2].cost2.ToString() + " G";
                upGradeText.Sellcost.text = "Sell : " + tower.towerInfo[buildManager.buildMenu.indexs + 2].GetSellCost().ToString() + " G";
                upGradeText.Hp.text = "Hp : " + tower.towerInfo[buildManager.buildMenu.indexs + 2].maxHealth.ToString();
                upGradeText.Mp.text = "Mp : " + tower.towerInfo[buildManager.buildMenu.indexs + 2].maxMana.ToString();
                upGradeText.Attack.text = "Attack : " + tower.towerInfo[buildManager.buildMenu.indexs + 2].projectile.attack.ToString();
                upGradeText.AttackSpeed.text = "AttackSpeed : " + tower.towerInfo[buildManager.buildMenu.indexs + 2].projectile.moveSpeed.ToString();
                PropertiesUI.SetActive(true);
                DescriptionUI.SetActive(true);
            }
            else if((tower.towerInfo[buildManager.buildMenu.indexs].projectile.tower != tower.towerInfo[buildManager.buildMenu.indexs].upgradeTower && !buildManager.IsUpgrade) || tower.towerInfo[buildManager.buildMenu.indexs].projectile.tower != tower.towerInfo[buildManager.buildMenu.indexs].upgradeTower)
            {
                //�⺻ �ͷ� �Ǹ� ���� ǥ��
                basicText.image.sprite = buildManager.buildMenu.towerSprite[buildManager.buildMenu.indexs];
                basicText.name.text = tower.towerInfo[buildManager.buildMenu.indexs].projectile.tower.ToString();
                basicText.Buycost.text = "Buy : " + tower.towerInfo[buildManager.buildMenu.indexs].cost1 + " G";
                basicText.Sellcost.text = "Sell : " + tower.towerInfo[buildManager.buildMenu.indexs].GetSellCost().ToString() + " G";
                basicText.Hp.text = "Hp : " + tower.towerInfo[buildManager.buildMenu.indexs].maxHealth.ToString();
                basicText.Mp.text = "Mp : " + tower.towerInfo[buildManager.buildMenu.indexs].maxMana.ToString();
                basicText.Attack.text = "Attack : " + tower.towerInfo[buildManager.buildMenu.indexs].projectile.attack.ToString();
                basicText.AttackSpeed.text = "AttackSpeed : " + tower.towerInfo[buildManager.buildMenu.indexs].projectile.moveSpeed.ToString();
                //���׷��̵� �Ǹ� ���� ǥ��
                upGradeText.image.sprite = buildManager.buildMenu.towerSprite[buildManager.buildMenu.indexs + 1];
                upGradeText.name.text = tower.towerInfo[buildManager.buildMenu.indexs].upgradeTower.ToString();
                upGradeText.Buycost.text = "Buy : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].cost2.ToString() + " G";
                upGradeText.Sellcost.text = "Sell : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].GetSellCost().ToString() + " G";
                upGradeText.Hp.text = "Hp : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].maxHealth.ToString();
                upGradeText.Mp.text = "Mp : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].maxMana.ToString();
                upGradeText.Attack.text = "Attack : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].projectile.attack.ToString();
                upGradeText.AttackSpeed.text = "AttackSpeed : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].projectile.moveSpeed.ToString();
                PropertiesUI.SetActive(true);
                DescriptionUI.SetActive(true);
            }*/
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