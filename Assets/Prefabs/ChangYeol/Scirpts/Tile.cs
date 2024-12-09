using Defend.Tower;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors.Visuals;

namespace Defend.UI
{
    public class Tile : XRSimpleInteractable
    {
        #region Variables
        //Ÿ�Ͽ� ��ġ�� Ÿ�� ���ӿ�����Ʈ ��ü
        [HideInInspector] public GameObject tower;
        [HideInInspector] public GameObject tower_upgrade;
        //���� ���õ� Ÿ�� TowerInfo(prefab, cost, ....)
        public TowerInfo[] towerInfo;
        //����Ŵ��� ��ü
        private BuildManager buildManager;
        //��ġ�ϸ� �����Ǵ� ����Ʈ
        public GameObject TowerImpectPrefab;
        //�÷��̾� ��ġ
        public BuildMenu buildMenu;
        //Ÿ�� ���׷��̵� ����
        public bool IsUpgrade { get; private set; }
        //
        public XRInteractorLineVisual lineVisual;
        public InputActionProperty property;
        #endregion

        private void Start()
        {
            //�ʱ�ȭ
            buildManager = BuildManager.Instance;
        }
        private void Update()
        {
            if (property.action.WasPressedThisFrame())
            {
                if (lineVisual.reticle)
                {
                    Destroy(lineVisual.reticle);
                    lineVisual.reticle = null;
                    buildMenu.BuildUI.SetActive(true);
                }
            }
        }
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            if (!buildManager.playerState.SpendMoney(buildMenu.towerinfo[buildMenu.indexs].cost1))
            {
                buildManager.warningWindow.ShowWarning("Not Enough Money");
                return;
            }
            if (lineVisual.reticle && buildManager.playerState.SpendMoney(buildMenu.towerinfo[buildMenu.indexs].cost1))
            {
                tower = Instantiate(buildMenu.towerinfo[buildMenu.indexs].projectile.tower, GetBuildPosition(), Quaternion.identity);
                GameObject effgo = Instantiate(TowerImpectPrefab, tower.transform.position, Quaternion.identity);
                Destroy(effgo,2f);
                
                tower.AddComponent<BoxCollider>();
                tower.AddComponent<TowerXR>();
                BoxCollider box = tower.GetComponent<BoxCollider>();
                box.size = buildMenu.boxes[buildMenu.indexs].size;
                box.center = buildMenu.boxes[(buildMenu.indexs)].center;
            }
            /*if(reticleVisual.reticlePrefab)
            {
                tower = Instantiate(reticleVisual.reticlePrefab, reticleVisual.reticlePrefab.transform.position,Quaternion.identity);
            }*/
        }
        //Ÿ�� ��ġ ��ġ
        public Vector3 GetBuildPosition()
        {
            if(lineVisual.reticle)
            {
                Debug.Log("towerselectssss");
                return lineVisual.reticle.transform.position;
            }
            return Vector3.zero;
        }
        //Ÿ�� ����
        public void BuildTower(Vector3 size, Vector3 center, int index)
        {
            //��ġ�� �ͷ��� �Ӽ��� �������� (�ͷ� ������, �Ǽ����, ���׷��̵� ������, ���׷��̵� ���...)
            towerInfo[0] = buildManager.GetTowerToBuild();
            //���� �����Ѵ� 100, 250
            //Debug.Log($"�ͷ� �Ǽ����: {blueprint.cost}");
            //lineVisual.reticle�� towerInfo�� ������ upgradetower�� ����
            if (lineVisual.reticle)
            {
                Destroy(lineVisual.reticle);
                lineVisual.reticle = null;
                //lineVisual.reticle = towerInfo[0].projectile.tower;
                lineVisual.reticle = buildMenu.falsetowers[buildMenu.indexs];
                lineVisual.reticle.GetComponent<BoxCollider>().enabled = false;
                return;
            }
            else if(!lineVisual.reticle)
            {
                //lineVisual.reticle = towerInfo[0].projectile.tower;
                lineVisual.reticle = buildMenu.falsetowers[buildMenu.indexs];
                lineVisual.reticle.GetComponent<BoxCollider>().enabled = false;
            }
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
            if (tower != null)
            {
                Destroy(tower);
                //IsUpgrade = false;
                //GameObject effect = Instantiate(SellImpectPrefab, GetBuildPosition(), Quaternion.identity);
                //Destroy(effect, 2f);
                //�⺻�ͷ����� �ݰ����� �Ǹ�
                buildManager.playerState.AddMoney(1);
            }
        }

        public void UpgradeTower(Vector3 size, Vector3 center)
        {
            
            if (tower == null)
            {
                Debug.Log("���׷��̵� �����߽��ϴ�");
                return;
            }
            if(tower)
            {
                towerInfo[0] = buildManager.GetTowerToBuild();
                Debug.Log("�ͷ� ���׷��̵�");
                //Effect
                //GameObject effectGo = Instantiate(TowerImpectPrefab, GetBuildPosition(), Quaternion.identity);
                //Destroy(effectGo, 2f);

                //�ͷ� ���׷��̵� ����
                IsUpgrade = true;
                buildMenu.indexs += 1;

                //�ͷ� ���׷��̵� ����   
                tower_upgrade = Instantiate(towerInfo[0].upgradeTower, tower.transform.position, Quaternion.identity);
                tower_upgrade.AddComponent<TowerXR>();
                tower_upgrade.AddComponent<BoxCollider>();
                BoxCollider boxCollider = tower.GetComponent<BoxCollider>();
                boxCollider.size = size;
                boxCollider.center = center;
                Destroy(tower);
                tower_upgrade = tower;
                tower_upgrade = null;
            }
        }
    }
}