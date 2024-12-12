using Defend.Player;
using Defend.Tower;
using UnityEngine;
using UnityEngine.UI;

namespace Defend.UI
{
    public class BuildManager : MonoBehaviour
    {
        #region Singleton
        public static BuildManager instance;
        public static BuildManager Instance
        {
            get { return instance; }
        }

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        #endregion

        #region Variables
        //Ÿ�Ͽ� ��ġ�� Ÿ���� ����(������, ��������)
        private TowerInfo towerInfo;
        //�÷��̾�
        public PlayerState playerState;
        //���â
        public WarningWindow warningWindow;
        //�Ǹ� ��ư
        public Button Sellbutton;
        //���׷��̵� ��ư
        public Button UpgradeButton;
        //Ÿ�� ���׷��̵� ����
        public bool IsUpgrade;
        //Ÿ�� UI
        public UpgradeAndSell menu;
        public BuildMenu buildMenu;
        //������ Ÿ��
        private TowerXR tower;
        #endregion

        public TowerInfo GetTowerToBuild()
        {
            return towerInfo;
        }
        //�Ű������� ���� Ÿ�� �������� ��ġ�� Ÿ���� ����
        public void SetTowerToBuild(TowerInfo Tower)
        {
            towerInfo = Tower;
            DeselectTile();
        }
        public void SelectTower(TowerXR towerXR)
        {
            //���� Ÿ���� �����ϸ� HideUI
            if (towerXR == tower)
            {
                DeselectTile();
                return;
            }
            //������ Ÿ���� �����ϱ�
            tower = towerXR;
            //������ Ÿ�� �Ӽ��� �ʱ�ȭ
            towerInfo = null;
            //Debug.Log("Ÿ�� UI �����ֱ�");
            menu.ShowTileUI(towerXR);
            UpgradeButton.onClick.AddListener(towerXR.UpgradeTower);
            Sellbutton.onClick.AddListener(towerXR.SellTower);
        }
        //���� ����
        public void DeselectTile()
        {
            //Debug.Log("Ÿ�� UI ���߱�");
            menu.HidetileUI();
            UpgradeButton.onClick.RemoveAllListeners();
            Sellbutton.onClick.RemoveAllListeners();
            //������ Ÿ�� �ʱ�ȭ�ϱ�
            tower = null;
        }
    }
}
