using Defend.Tower;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Defend.UI
{
    public class TowerXR : XRSimpleInteractable
    {
        #region Variables
        public TowerInfo[] towerInfo = new TowerInfo[12];

        private GameObject tower_upgrade;

        //����Ŵ��� ��ü
        private BuildManager buildManager;

        #endregion

        private void Start()
        {
            //�ʱ�ȭ
            buildManager = BuildManager.Instance;
            for (int i = 0; i < towerInfo.Length; i++)
            {
                towerInfo[i] = buildManager.buildMenu.towerinfo[i];
                towerInfo[i].upgradeTower = buildManager.buildMenu.towerinfo[i].upgradeTower;
                towerInfo[i].projectile.tower = buildManager.buildMenu.towerinfo[i].projectile.tower;
                towerInfo[i].projectile = buildManager.buildMenu.towerinfo[i].projectile;
            }
        }
        protected override void OnHoverEntered(HoverEnterEventArgs args)
        {
            base.OnHoverEntered(args);
            buildManager.buildMenu.isReticle = false;
        }
        protected override void OnHoverExited(HoverExitEventArgs args)
        {
            base.OnHoverExited(args);
            buildManager.buildMenu.isReticle = true;
        }
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            buildManager.SelectTower(this);
        }
        public void SellTower()
        {
            //�⺻ �ͷ��� �Ǹ�
            if (this.gameObject != null)
            {
                Destroy(this.gameObject);
                //IsUpgrade = false;
                //GameObject effect = Instantiate(SellImpectPrefab, GetBuildPosition(), Quaternion.identity);
                //Destroy(effect, 2f);
                //�⺻�ͷ����� �ݰ����� �Ǹ�
                buildManager.playerState.AddMoney(towerInfo[buildManager.buildMenu.indexs].GetSellCost());
                buildManager.DeselectTile();
            }
            else if (!this.gameObject)
            {
                Debug.Log("�Ǹ����� ���߽��ϴ�");
            }
        }

        public void UpgradeTower()
        {
            if (towerInfo[buildManager.buildMenu.indexs] == null)
            {
                Debug.Log("���׷��̵� �����߽��ϴ�");
                return;
            }
            if (towerInfo[buildManager.buildMenu.indexs] != null)
            {
                //Effect
                //GameObject effectGo = Instantiate(TowerImpectPrefab, GetBuildPosition(), Quaternion.identity);
                //Destroy(effectGo, 2f);
                if (towerInfo[buildManager.buildMenu.indexs].upgradeTower &&
                    buildManager.playerState.SpendMoney(towerInfo[buildManager.buildMenu.indexs].cost2))
                {
                    //�ͷ� ���׷��̵� ����
                    buildManager.IsUpgrade = true;
                    //�ͷ� ���׷��̵� ����
                    tower_upgrade = Instantiate(towerInfo[buildManager.buildMenu.indexs].upgradeTower,
                        transform.position, Quaternion.identity);
                    tower_upgrade.AddComponent<BoxCollider>();
                    tower_upgrade.AddComponent<TowerXR>();
                    BoxCollider boxCollider = tower_upgrade.GetComponent<BoxCollider>();
                    boxCollider.size = buildManager.buildMenu.boxes[buildManager.buildMenu.indexs].size;
                    boxCollider.center = buildManager.buildMenu.boxes[buildManager.buildMenu.indexs].center;
                    Destroy(this.gameObject);
                    tower_upgrade = null;
                    buildManager.DeselectTile();
                    buildManager.buildMenu.levelindex += 1;
                    buildManager.buildMenu.istowerup = false;
                }
            }
        }
    }
}