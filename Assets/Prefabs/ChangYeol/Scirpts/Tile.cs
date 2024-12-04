using Defend.Player;
using Defend.Tower;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors.Visuals;

namespace Defend.UI
{
    public class Tile : XRSimpleInteractable
    {
        #region Variables
        //Ÿ�Ͽ� ��ġ�� Ÿ�� ���ӿ�����Ʈ ��ü
        public GameObject[] towers;
        private GameObject tower;
        [HideInInspector] public GameObject tower_upgrade;
        //���� ���õ� Ÿ�� TowerInfo(prefab, cost, ....)
        public TowerInfo[] towerInfo;
        private Image[] towerimage;

        private Vector3 offset;

        //����Ŵ��� ��ü
        private BuildManager buildManager;
        //��ġ�ϸ� �����Ǵ� ����Ʈ
        public GameObject TowerImpectPrefab;
        //�÷��̾� ��ġ
        public BuildMenu buildMenu;
        //Ÿ�� ���׷��̵� ����
        public bool IsUpgrade { get; private set; }
        [SerializeField] private float distance = 1.5f;

        public XRInteractorReticleVisual reticleVisual;
        public XRInteractorLineVisual lineVisual;
        #endregion

        private void Start()
        {
            //�ʱ�ȭ

            buildManager = BuildManager.Instance;
        }
        protected override void OnHoverEntering(HoverEnterEventArgs args)
        {
            base.OnHoverEntering(args);
        }
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            tower = Instantiate(lineVisual.reticle, GetBuildPosition(), Quaternion.identity);
            tower.AddComponent<BoxCollider>();
            //BoxCollider box = tower.GetComponent<BoxCollider>();
            tower.SetActive(true);
            Destroy(lineVisual.reticle);
            lineVisual.reticle = null;
            Debug.Log($"{reticleVisual.reticlePrefab},{lineVisual.reticle}");
        }
        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
        }
        //Ÿ�� ��ġ ��ġ
        public Vector3 GetBuildPosition()
        {
            Debug.Log("towerselectssss");
            return lineVisual.reticle.transform.position;
        }
        //Ÿ�� ����
        public void BuildTower(Vector3 size, Vector3 center,int index)
        {
            //��ġ�� �ͷ��� �Ӽ��� �������� (�ͷ� ������, �Ǽ����, ���׷��̵� ������, ���׷��̵� ���...)
            towerInfo[0] = buildManager.GetTowerToBuild();
            //���� �����Ѵ� 100, 250
            //Debug.Log($"�ͷ� �Ǽ����: {blueprint.cost}");
            //Ÿ�� ����
            //tower = Instantiate(towerInfo[1].upgradeTower, GetBuildPosition(), Quaternion.identity);
            if (!reticleVisual.reticlePrefab)
            {
                //lineVisual.reticle = towers[index];
                lineVisual.reticle = towerInfo[0].upgradeTower;
                //Ÿ���� ���� �� �ִ� ������Ʈ �߰�
                /*tower.AddComponent<BoxCollider>();
                BoxCollider boxCollider = towerInfo[1].upgradeTower.GetComponent<BoxCollider>();
                boxCollider.size = size;
                boxCollider.center = center;
                boxCollider.enabled = false;*/
            }
            //towerInfo[1].upgradeTower.AddComponent<TowerXR>();
            //Ÿ�� ���� ����Ʈ
            /*GameObject effgo = Instantiate(TowerImpectPrefab, GetBuildPosition(), Quaternion.identity);
            //Ÿ�� �ڽ����� ����
            effgo.transform.parent = transform;

            Destroy(effgo, 1.5f);*/
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
                towerInfo[1] = buildManager.GetTowerToBuild();
                Debug.Log("�ͷ� ���׷��̵�");
                //Effect
                //GameObject effectGo = Instantiate(TowerImpectPrefab, GetBuildPosition(), Quaternion.identity);
                //Destroy(effectGo, 2f);

                //�ͷ� ���׷��̵� ����
                IsUpgrade = true;

                //�ͷ� ���׷��̵� ����   
                tower_upgrade = Instantiate(towerInfo[2].upgradeTower, tower.transform.position, Quaternion.identity);
                tower_upgrade.AddComponent<BoxCollider>();
                tower_upgrade.AddComponent<TowerXR>();
                BoxCollider boxCollider = tower.GetComponent<BoxCollider>();
                boxCollider.size = size;
                boxCollider.center = center;
                Destroy(tower);
                tower = tower_upgrade;
                tower_upgrade = null;
            }
        }
    }
}