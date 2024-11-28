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

        //������ �ͷ��� �Ǽ��� ����� ������ �ִ���
        public bool HasBuildMoney
        {
            get
            {
                if (towerInfo == null)
                    return false;
                //���߿� Ÿ�� ��ġ ���
                return HasMoney(2,2,2);
            }
        }

        //Ÿ�� UI
        public UpgradeAndSell menu;
        //������ Ÿ��
        private Tile selectTile;
        #endregion

        public TowerInfo GetTurretToBuild()
        {
            return towerInfo;
        }

        //�Ű������� ���� �ͷ� �������� ��ġ�� �ͷ��� ����
        public void SetTurretToBuild(TowerInfo Tower)
        {
            towerInfo = Tower;
            DeselectTile();
        }
        //�Ű������� ������ Ÿ�� ������ ���´�
        public void SelectTile(Tile tile)
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
        }
        //���� ����
        public void DeselectTile()
        {
            //Debug.Log("Ÿ�� UI ���߱�");
            menu.HidetileUI();
            //������ Ÿ�� �ʱ�ȭ�ϱ�
            selectTile = null;
        }

        public bool HasMoney(int moneyamount, int treeamount, int rockamount)
        {
            return playerState.money >= moneyamount && playerState.tree >= treeamount 
                && playerState.rock >= rockamount;
        }

    }
}
