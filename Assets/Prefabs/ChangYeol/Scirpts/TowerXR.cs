using Defend.Tower;
using Unity.VisualScripting;
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
            /*//�ʱ�ȭ
            buildManager = BuildManager.Instance;
            for (int i = 0; i < towerInfo.Length; i++)
            {
                towerInfo[i] = buildManager.buildMenu.towerinfo[i];
                towerInfo[i].upgradeTower = buildManager.buildMenu.towerinfo[i].upgradeTower;
                towerInfo[i].projectile.tower = buildManager.buildMenu.towerinfo[i].projectile.tower;
                towerInfo[i].projectile = buildManager.buildMenu.towerinfo[i].projectile;
            }*/
        }
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            buildManager.SelectTower(this);
            //if()
        }
        public void SellTower()
        {
            /*//���׷��̵� �ͷ��� �Ǹ�
            if (turret_upgrade != null)
            {
                Destroy(turret_upgrade);
                IsUpgrade = false;
                GameObject effect = Instantiate(SellImpectPrefab, GetBuildPosition(), Quaternion.identity);
                Destroy(effect, 2f);
                //���׷��̵��ͷ����� �ݰ����� �Ǹ�
                PlayerStats.AddMoney(blueprint.Getupgradecost());
            }*/
            //�⺻ �ͷ��� �Ǹ�
            if (this.gameObject != null && !buildManager.IsUpgrade)
            {
                Debug.Log("�Ǹ�");
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
            /*if (towerInfo[buildManager.buildMenu.indexs] == null)
            {
                Debug.Log("���׷��̵� �����߽��ϴ�");
                return;
            }
            if (towerInfo[buildManager.buildMenu.indexs] != null)
            {
                Debug.Log("�ͷ� ���׷��̵�");
                //Effect
                //GameObject effectGo = Instantiate(TowerImpectPrefab, GetBuildPosition(), Quaternion.identity);
                //Destroy(effectGo, 2f);

                //�ͷ� ���׷��̵� ����
                buildManager.IsUpgrade = true;

                //�ͷ� ���׷��̵� ����   
                tower_upgrade = Instantiate(towerInfo[buildManager.buildMenu.indexs].upgradeTower, transform.position, Quaternion.identity);
                tower_upgrade.AddComponent<BoxCollider>();
                tower_upgrade.AddComponent<TowerXR>();
                BoxCollider boxCollider = tower_upgrade.GetComponent<BoxCollider>();
                boxCollider.size = buildManager.buildMenu.boxes[buildManager.buildMenu.indexs].size;
                boxCollider.center = buildManager.buildMenu.boxes[buildManager.buildMenu.indexs].center;
                buildManager.DeselectTile();
            }*/
        }
    }
}