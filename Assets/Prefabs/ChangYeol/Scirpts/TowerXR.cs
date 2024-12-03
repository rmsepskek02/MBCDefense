using Defend.Tower;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Defend.UI
{
    public class TowerXR : XRGrabInteractable
    {
        #region Variables
        public TowerInfo towerInfo;

        //����Ŵ��� ��ü
        private BuildManager buildManager;

        XRGrabInteractable xRGrab;

        BoxCollider boxCollider;
        #endregion

        private void Start()
        {
            buildManager = BuildManager.Instance;
            xRGrab = this.GetComponent<XRGrabInteractable>();
            boxCollider = this.GetComponent<BoxCollider>();
            xRGrab.colliders.Add(boxCollider);
            boxCollider.size = new Vector3(1, 2.516554f, 1);
            boxCollider.center = new Vector3(0, 0.2562535f, 0);
        }
        protected override void OnHoverEntered(HoverEnterEventArgs args)
        {
            base.OnHoverEntered(args);
            buildManager.DeselectTile();
        }
        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            buildManager.SelectTile(this);
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
            if (this != null)
            {
                Destroy(this);
                //IsUpgrade = false;
                //GameObject effect = Instantiate(SellImpectPrefab, GetBuildPosition(), Quaternion.identity);
                //Destroy(effect, 2f);
                //�⺻�ͷ����� �ݰ����� �Ǹ�
                //PlayerState.AddMoney();
            }
        }

        public void UpgradeTower()
        {
            Debug.Log("�ͷ� ���׷��̵�");
            /*if (blueprint == null)
            {
                //Debug.Log("���׷��̵� �����߽��ϴ�");
                return;
            }
            if (PlayerStats.UseMoney(blueprint.costUpgrade))
            {
                //Effect
                GameObject effectGo = Instantiate(TowerImpectPrefab, GetBuildPosition(), Quaternion.identity);
                Destroy(effectGo, 2f);

                //�ͷ� ���׷��̵� ����
                IsUpgrade = true;

                //�ͷ� ���׷��̵� ����
                turret_upgrade = Instantiate(TowerInfo.upgradeTower, GetBuildPosition(), Quaternion.identity);
                Destroy(turret);
            }*/
        }
    }
}