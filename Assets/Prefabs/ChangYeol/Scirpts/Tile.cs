using Defend.Player;
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
        //�÷��̾� �޼� �׶� ���� ���־�
        public XRInteractorLineVisual lineVisual;
        //Ʈ���� Ű �Է�
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
            /*base.OnSelectEntered(args);
            if (!buildManager.playerState.SpendMoney(towerInfo[buildMenu.indexs].cost1))
            {
                buildManager.warningWindow.ShowWarning("Not Enough Money");
                return;
            }
            if (lineVisual.reticle && buildManager.playerState.SpendMoney(towerInfo[buildMenu.indexs].cost1))
            {
                tower = Instantiate(towerInfo[buildMenu.indexs].projectile.tower, GetBuildPosition(), Quaternion.identity);

                TowerBase towerBase = tower.GetComponent<TowerBase>();
                CastleUpgrade castleUpgrade = GetComponent<CastleUpgrade>();

                if (towerBase != null)
                {
                    towerBase.BuffTower(CastleUpgrade.buffContents, true);
                }
                GameObject effgo = Instantiate(TowerImpectPrefab, tower.transform.position, Quaternion.identity);
                Destroy(effgo, 2f);

                tower.AddComponent<BoxCollider>();
                tower.AddComponent<TowerXR>();
                BoxCollider box = tower.GetComponent<BoxCollider>();
                box.size = buildMenu.boxes[buildMenu.indexs].size;
                box.center = buildMenu.boxes[(buildMenu.indexs)].center;
            }*/
        }

        //Ÿ�� ��ġ ��ġ
        public Vector3 GetBuildPosition()
        {
            if (lineVisual.reticle)
            {
                return lineVisual.reticle.transform.position;
            }
            return Vector3.zero;
        }
        //Ÿ�� ����
        public void BuildTower(Vector3 size, Vector3 center)
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
            else if (!lineVisual.reticle)
            {
                //lineVisual.reticle = towerInfo[0].projectile.tower;
                lineVisual.reticle = buildMenu.falsetowers[buildMenu.indexs];
                lineVisual.reticle.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}