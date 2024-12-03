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
        public GameObject tileUI;

        private BuildManager buildManager;
        private TowerInfo towerInfo;
        //���ù��� Ÿ��
        private TowerXR tower;
        //private Tile targetTile;

        //�⺻ Ÿ�� ���Ű���, �ǸŰ���, HP,MP, Attack, AttackSpeed
        public Upgrade basicText;
        //���׷��̵� Ÿ�� ���� �ؽ�Ʈ, ��ư, �ǸŰ��� �ؽ�Ʈ
        //public Upgrade upGradeText;

        //���׷��̵� ��ư
        public Button upgradebutton;
        public Button upgradeUIbutton;

        //�÷��̾� ī�޶� ��ġ
        public Transform head;
        [SerializeField] private float distance = 1.5f;
        #endregion

        void Start()
        {
            //�ʱ�ȭ
            buildManager = BuildManager.Instance;
        }

        //�Ű������� ������ Ÿ�� ������ ���´�
        /*public void ShowTileUI(Tile tile)
        {
            //���ù��� Ÿ�� ����
            targetTile = tile;

            //Ÿ���� ��ġ�� ��ġ �������� �����ش�
            this.transform.position = head.position + new Vector3(head.forward.x + -0.5f, 0, head.forward.z).normalized * distance;
            this.transform.LookAt(new Vector3(head.position.x, this.transform.position.y, head.position.z));
            this.transform.forward *= -1;
            //���׷��̵� ���� ǥ��
            if (!targetTile)
            {
                //���׷��̵� �Ǹ� ���� ǥ��
                //sellcost.text = targetTile.blueprint.Getupgradecost().ToString() + " G";
                //upgradecost.text = "Done";
                upgradebutton.interactable = false;
            }
            else if(targetTile)
            {
                //�⺻ �ͷ� �Ǹ� ���� ǥ��
                basicText.name.text = targetTile.towerInfo.upgradeTower.name;
                basicText.Buycost.text = "Buy : " +  targetTile.towerInfo.cost2.ToString() + " G";
                basicText.Sellcost.text = "Sell : " + targetTile.towerInfo.GetSellCost().ToString() + " G";
                basicText.Hp.text = "Hp : " + targetTile.towerInfo.maxHealth.ToString();
                basicText.Mp.text = "Mp : " + targetTile.towerInfo.maxMana.ToString();
                basicText.Attack.text = "Attack : " + targetTile.towerInfo.projectile.attack.ToString();
                basicText.AttackSpeed.text = "AttackSpeed : " + targetTile.towerInfo.projectile.moveSpeed.ToString();
                upgradebutton.interactable = true;
            }
            tileUI.SetActive(true);
        }*/
        public void ShowTileUI(TowerXR towerXR)
        {
            //���ù��� Ÿ�� ����
            tower = towerXR;

            //Ÿ���� ��ġ�� ��ġ �������� �����ش�
            this.transform.position = head.position + new Vector3(head.forward.x + -0.5f, 0, head.forward.z).normalized * distance;
            this.transform.LookAt(new Vector3(head.position.x, this.transform.position.y, head.position.z));
            this.transform.forward *= -1;
            //���׷��̵� ���� ǥ��
            if (!tower)
            {
                //���׷��̵� �Ǹ� ���� ǥ��
                //sellcost.text = targetTile.blueprint.Getupgradecost().ToString() + " G";
                //upgradecost.text = "Done";
                upgradebutton.interactable = false;
            }
            else if (tower)
            {
                //�⺻ �ͷ� �Ǹ� ���� ǥ��
                basicText.name.text = tower.towerInfo.upgradeTower.name;
                basicText.Buycost.text = "Buy : " + tower.towerInfo.cost2.ToString() + " G";
                basicText.Sellcost.text = "Sell : " + tower.towerInfo.GetSellCost().ToString() + " G";
                basicText.Hp.text = "Hp : " + tower.towerInfo.maxHealth.ToString();
                basicText.Mp.text = "Mp : " + tower.towerInfo.maxMana.ToString();
                basicText.Attack.text = "Attack : " + tower.towerInfo.projectile.attack.ToString();
                basicText.AttackSpeed.text = "AttackSpeed : " + tower.towerInfo.projectile.moveSpeed.ToString();
                upgradebutton.interactable = true;
            }
            tileUI.SetActive(true);
        }
        //���������� UI �Ⱥ��̰� �ϱ�
        public void HidetileUI()
        {
            tileUI.SetActive(false);
            //���ù��� Ÿ�� �ʱ�ȭ
            tower = null;
        }

        public void Selled()
        {
            tower.SellTower();
            buildManager.DeselectTile();
        }
        public void Upgraded()
        {
            tower.UpgradeTower();
            buildManager.DeselectTile();
        }
    }
}