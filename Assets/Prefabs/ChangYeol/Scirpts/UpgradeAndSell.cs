using Defend.Tower;
using TMPro;
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
        private Tile targetTile;

        //���׷��̵� ���� �ؽ�Ʈ, ��ư, �ǸŰ��� �ؽ�Ʈ
        public TextMeshProUGUI upgradecost;

        //���׷��̵� ��ư
        public Button upgradebutton;
        //�Ǹ� ��ư
        public TextMeshProUGUI sellcost;

        //�÷��̾� ī�޶� ��ġ
        public Transform head;
        #endregion

        void Start()
        {
            //�ʱ�ȭ
            buildManager = BuildManager.Instance;


        }

        //�Ű������� ������ Ÿ�� ������ ���´�
        public void ShowTileUI(Tile tile)
        {
            //���ù��� Ÿ�� ����
            targetTile = tile;

            //Ÿ���� ��ġ�� ��ġ �������� �����ش�
            this.transform.position = targetTile.GetBuildPosition();

            //���׷��̵� ���� ǥ��
            if (targetTile)
            {
                //���׷��̵� �Ǹ� ���� ǥ��
                //sellcost.text = targetTile.blueprint.Getupgradecost().ToString() + " G";
                upgradecost.text = "Done";
                upgradebutton.interactable = false;
            }
            else
            {
                //�⺻ �ͷ� �Ǹ� ���� ǥ��
                sellcost.text = targetTile.towerInfo.GetSellCost().ToString() + " G";
                upgradecost.text = targetTile.towerInfo.cost2.ToString() + " G";
                upgradebutton.interactable = true;
            }
            tileUI.SetActive(true);
        }
        //���������� UI �Ⱥ��̰� �ϱ�
        public void HidetileUI()
        {
            tileUI.SetActive(false);
            //���ù��� Ÿ�� �ʱ�ȭ
            targetTile = null;
        }

        public void Selled()
        {
            targetTile.SellTower();
        }
        public void Upgraded()
        {
            targetTile.UpgradeTower();
        }
    }
}