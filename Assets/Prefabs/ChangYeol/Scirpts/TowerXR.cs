using Defend.Player;
using Defend.Tower;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Defend.UI
{
    public class TowerXR : XRSimpleInteractable
    {
        #region Variables
        [SerializeField]private TowerInfo towerInfo;
        public TowerInfo upgradetowerInfo;
        public int currentindex = BuildManager.instance.buildMenu.indexs;
        public Sprite[] currentTower = new Sprite[12];
        private GameObject tower_upgrade;
        private TowerBase towerBase;
        public int currentlevel = BuildManager.instance.buildMenu.levelindex;
        public bool Isupgradeone;
        public bool Isupgradetwo;
        //����Ŵ��� ��ü
        private BuildManager buildManager;
        #endregion
        protected override void Awake()
        {
            base.Awake();
            //�ʱ�ȭ
            buildManager = BuildManager.Instance;
            for (int i = 0; i < buildManager.buildMenu.towerSprite.Length; i++)
            {
                currentTower[i] = buildManager.buildMenu.towerSprite[i];
            }
            //����
            towerBase = GetComponent<TowerBase>();
            towerInfo = towerBase.GetTowerInfo();
            CastleUpgrade castle = buildManager.buildMenu.GetComponent<CastleUpgrade>();
            towerInfo.projectile.attack += castle.atkLevel;
            towerInfo.projectile.moveSpeed += castle.atkSpeedLevel;
            towerInfo.projectile.attackRange += castle.atkRangeLevel;
            if (towerBase.GetTowerInfo().upgradeTower)
            {
                upgradetowerInfo = towerBase.GetTowerInfo().upgradeTower.GetComponent<TowerBase>().GetTowerInfo();
                upgradetowerInfo.projectile.attack += castle.atkLevel;
                upgradetowerInfo.projectile.moveSpeed += castle.atkSpeedLevel;
                upgradetowerInfo.projectile.attack += castle.atkRangeLevel;
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
                buildManager.playerState.AddMoney(towerInfo.GetSellCost());
                buildManager.DeselectTile();
            }
            else if (!this.gameObject)
            {
                Debug.Log("�Ǹ����� ���߽��ϴ�");
            }
        }

        public void UpgradeTower()
        {
            if (towerInfo == null)
            {
                Debug.Log("���׷��̵� �����߽��ϴ�");
                return;
            }
            if (towerInfo != null)
            {
                //Effect
                //GameObject effectGo = Instantiate(TowerImpectPrefab, GetBuildPosition(), Quaternion.identity);
                //Destroy(effectGo, 2f);
                if (towerInfo.upgradeTower &&
                    buildManager.playerState.SpendMoney(towerInfo.cost2) && buildManager.playerState.SpendResources())
                {
                    if ((currentlevel == 1 && !Isupgradeone) || (currentlevel == 2 && !Isupgradeone))
                    {
                        //�ͷ� ���׷��̵� ����
                        tower_upgrade = Instantiate(towerInfo.upgradeTower, transform.position, Quaternion.identity);
                        tower_upgrade.AddComponent<BoxCollider>();
                        tower_upgrade.AddComponent<TowerXR>();
                        TowerXR tower = tower_upgrade.GetComponent<TowerXR>();
                        tower.currentindex += 1;
                        tower.currentlevel += 1;
                        tower.Isupgradeone = true;
                        BoxCollider boxCollider = tower_upgrade.GetComponent<BoxCollider>();
                        boxCollider.size = buildManager.buildMenu.boxes[currentindex].size;
                        boxCollider.center = buildManager.buildMenu.boxes[currentindex].center;
                        Destroy(this.gameObject);
                        tower_upgrade = null;
                        buildManager.DeselectTile();
                        buildManager.buildMenu.istowerup = false;
                    }
                    else if (currentlevel == 2 && Isupgradeone)
                    {
                        //�ͷ� ���׷��̵� ����
                        tower_upgrade = Instantiate(towerInfo.upgradeTower, transform.position, Quaternion.identity);
                        tower_upgrade.AddComponent<BoxCollider>();
                        tower_upgrade.AddComponent<TowerXR>();
                        TowerXR tower = tower_upgrade.GetComponent<TowerXR>();
                        tower.currentindex += 2;
                        tower.currentlevel += 2;
                        tower.Isupgradeone = true;
                        tower.Isupgradetwo = true;
                        BoxCollider boxCollider = tower_upgrade.GetComponent<BoxCollider>();
                        boxCollider.size = buildManager.buildMenu.boxes[currentindex].size;
                        boxCollider.center = buildManager.buildMenu.boxes[currentindex].center;
                        Destroy(this.gameObject);
                        tower_upgrade = null;
                        buildManager.DeselectTile();
                        buildManager.buildMenu.istowerup = false;
                    }
                }
            }
        }
    }
}