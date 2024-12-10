using Defend.Player;
using Defend.Tower;
using Unity.VisualScripting;
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
        private Sprite TowerSprite;

        //������ Ÿ���� �ִ���, �������� �������� �Ǽ� ���Ѵ�
        public bool CannotBuild => towerInfo == null;

        public PlayerState playerState;

        public WarningWindow warningWindow;

        public Button Sellbutton;
        public Button UpgradeButton;

        public bool IsUpgrade;

        //������ Ÿ���� �Ǽ��� ����� ������ �ִ���
        public bool HasBuildMoney
        {
            get
            {
                if (towerInfo == null)
                    return false;
                //���߿� Ÿ�� ��ġ ���
                return playerState.SpendMoney(towerInfo.cost2);
            }
        }

        //Ÿ�� UI
        public UpgradeAndSell menu;
        public BuildMenu buildMenu;
        //������ Ÿ��
        private TowerXR tower;

        /*//������ ��
        public EnemyState enemyStats;
        //�� �Ӽ� UI
        public EnemyPropertiesUI EnemyproUI;
        private EnemyXRSimple xRSimple;*/
        #endregion

        public TowerInfo GetTowerToBuild()
        {
            return towerInfo;
        }
        //�Ű������� ���� Ÿ�� �������� ��ġ�� Ÿ���� ����
        public void SetTowerToBuild(TowerInfo Tower)
        {
            towerInfo = Tower;
            /*TowerSprite = sprite;*/
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
        /*public EnemyState GetEnemyToBuild()
        {
            return enemyStats;
        }
        public void SetEnemyToBuild(EnemyState enemy)
        {
            enemyStats = enemy;
            DeselectEnemy();
        }*/
        /*public void SelectEnemy(EnemyXRSimple enemy)
        {
            //���� ���� �����ϸ� HideUI
            if (enemy == xRSimple)
            {
                DeselectTile();
                return;
            }

            //������ �� �����ϱ�
            enemy = xRSimple;
            //������ �� �Ӽ��� �ʱ�ȭ
            enemyStats = null;
            EnemyproUI.ShowProUI(enemy);
        }
        public void DeselectEnemy()
        {
            EnemyproUI.HideProUI();

            xRSimple = null;
        }*/
    }
}
