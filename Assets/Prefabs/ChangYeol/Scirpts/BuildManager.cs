using Defend.Enemy;
using Defend.Player;
using Defend.Tower;
using UnityEngine;

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

        //������ Ÿ���� �ִ���, �������� �������� �Ǽ� ���Ѵ�
        public bool CannotBuild => towerInfo == null;

        public PlayerState playerState;

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
        //������ Ÿ��
        //private Tile selectTile;
        private TowerXR tower;

        //������ ��
        public EnemyState enemyStats;
        //�� �Ӽ� UI
        public EnemyPropertiesUI EnemyproUI;
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
        //�Ű������� ������ Ÿ�� ������ ���´�
        /*public void SelectTile(Tile tile)
        {
            //���� Ÿ���� �����ϸ� HideUI
            if (tile == selectTile)
            {
                DeselectTile();
                return;
            }

            //������ Ÿ�� �����ϱ�
            selectTile = tile;
            //������ Ÿ�� �Ӽ��� �ʱ�ȭ
            towerInfo = null;
            //Debug.Log("Ÿ�� UI �����ֱ�");
            menu.ShowTileUI(tile);
        }*/
        public void SelectTile(TowerXR towerXR)
        {
            //���� Ÿ���� �����ϸ� HideUI
            if (towerXR == tower)
            {
                DeselectTile();
                return;
            }

            //������ Ÿ���� �����ϱ�
            towerXR = tower;
            //������ Ÿ�� �Ӽ��� �ʱ�ȭ
            towerInfo = null;
            //Debug.Log("Ÿ�� UI �����ֱ�");
            menu.ShowTileUI(towerXR);
        }
        //���� ����
        public void DeselectTile()
        {
            //Debug.Log("Ÿ�� UI ���߱�");
            menu.HidetileUI();
            //������ Ÿ�� �ʱ�ȭ�ϱ�
            tower = null;
        }

        public void SelectEnemy(EnemyState enemy)
        {
            //���� ���� �����ϸ� HideUI
            if (enemy == enemyStats)
            {
                DeselectTile();
                return;
            }

            //������ �� �����ϱ�
            enemy = enemyStats;
            //������ �� �Ӽ��� �ʱ�ȭ
        }

    }
}
