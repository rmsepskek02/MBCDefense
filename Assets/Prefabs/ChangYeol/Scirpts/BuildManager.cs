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
        //Ÿ�Ͽ� ��ġ�� �ͷ��� ����(������, ��������)
        private TowerInfo towerInfo;

        //������ �ͷ��� �ִ���, �������� �������� �Ǽ� ���Ѵ�
        public bool CannotBuild => towerInfo == null;

        //������ �ͷ��� �Ǽ��� ����� ������ �ִ���
        public bool HasBuildMoney
        {
            get
            {
                if (towerInfo == null)
                    return false;

                return true;
            }

        }

        //Ÿ�� UI
        public TileUI tileUI;
        //������ Ÿ��
        private TileUI selectTile;
        #endregion

        public TowerInfo GetTurretToBuild()
        {
            return towerInfo;
        }

        //�Ű������� ���� �ͷ� �������� ��ġ�� �ͷ��� ����
        public void SetTurretToBuild(TowerInfo turret)
        {
            towerInfo = turret;
            DeselectTile();
        }
        //�Ű������� ������ Ÿ�� ������ ���´�
        public void SelectTile(TileUI tile)
        {
            //���� Ÿ���� �����ϸ� HideUI
            if (tile == selectTile)
            {
                DeselectTile();
                return;
            }

            //������ Ÿ�� �����ϱ�
            selectTile = tile;
            //������ �ͷ� �Ӽ��� �ʱ�ȭ
            towerInfo = null;
            //Debug.Log("Ÿ�� UI �����ֱ�");
            //tileUI.ShowTileUI(tile);
        }
        //���� ����
        public void DeselectTile()
        {
            //Debug.Log("Ÿ�� UI ���߱�");
            //tileUI.HidetileUI();
            //������ Ÿ�� �ʱ�ȭ�ϱ�
            selectTile = null;
        }
    }
}
