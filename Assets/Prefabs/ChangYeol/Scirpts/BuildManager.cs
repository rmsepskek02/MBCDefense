using Defend.Enemy;
using Defend.Player;
using Defend.Tower;
using System.Collections;
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
        [HideInInspector] public TowerInfo towerInfo;
        //�÷��̾�
        public PlayerState playerState;
        //���â
        public WarningWindow warningWindow;
        //�Ǹ� ��ư
        public Button Sellbutton;
        //���׷��̵� ��ư
        public Button UpgradeButton;
        //Ÿ�� UI
        public UpgradeAndSell menu;
        public BuildMenu buildMenu;
        //������ Ÿ��
        [HideInInspector] public TowerXR tower;
        //Ÿ�� �ȿ� �ִ� �Ӽ���
        public TowerBase[] towerBases;
        [HideInInspector] public EnemyPropertiesUI enemy;

        public AudioClip towerBuildSound;
        #endregion
        private void Start()
        {
            enemy = menu.GetComponent<EnemyPropertiesUI>();
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
