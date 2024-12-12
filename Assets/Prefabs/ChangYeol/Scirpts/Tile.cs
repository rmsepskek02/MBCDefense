using Defend.Player;
using Defend.Tower;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactors.Visuals;
using UnityEngine.XR.Interaction.Toolkit.UI;

namespace Defend.UI
{
    public class Tile : XRSimpleInteractable
    {
        #region Variables
        //Ÿ�Ͽ� ��ġ�� Ÿ�� ���ӿ�����Ʈ ��ü
        [HideInInspector] public GameObject tower;
        //���� ���õ� Ÿ�� TowerInfo(prefab, cost, ....)
        public TowerInfo towerInfo;
        //����Ŵ��� ��ü
        private BuildManager buildManager;
        //��ġ�ϸ� �����Ǵ� ����Ʈ
        public GameObject TowerImpectPrefab;
        //�÷��̾� ��ġ
        public BuildMenu buildMenu;
        //�÷��̾� �� ����
        public XRRayInteractor rayInteractor;
        //�÷��̾� �޼� ��ƼŬ ���־�
        private XRInteractorReticleVisual reticleVisual;
        //��ġ�� Ÿ���� �����ִ� ���� ������Ʈ
        private GameObject reticlePrefabs;
        //Ʈ���� Ű �Է�
        public InputActionProperty property;
        //Ÿ�� ������ ������ ���̾� ����
        public InteractionLayerMask layerMask;
        //Ÿ�� ��ġ ��ġ
        private Vector3 hitPoint;
        #endregion

        private void Start()
        {
            //�ʱ�ȭ
            buildManager = BuildManager.Instance;
            reticleVisual = rayInteractor.GetComponent<XRInteractorReticleVisual>();
        }
        private void Update()
        {
            //Trigger ��ư ������ reticle = null
            if (property.action.WasPressedThisFrame())
            {
                buildMenu.isReticle = false;
            }

            if (rayInteractor == null) return;

            // ���� ����ĳ��Ʈ ��Ʈ ���� ��������
            if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
            {
                hitPoint = hit.point; // Reticle�� ǥ���ϴ� ��ġ
            }
            IsBuildTower();
        }
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            //Ÿ�� ��ġ
            SetBuildTower();
        }
        // ������ ��ȿ���� �˻��ϴ� �Լ�
        private bool IsLineVisualValid()
        {
            // XRRayInteractor�� ��Ʈ ������ ������
            if (rayInteractor.TryGetHitInfo(out Vector3 hitPosition, out Vector3 hitNormal,
                out int hitIndex, out bool isValidTarget))
            {
                return isValidTarget; // Ÿ���� ��ȿ���� ��ȯ
            }
            return false;
        }
        // ReticlePrefab�� �����ϴ� �Լ�
        private void SetReticlePrefab(GameObject prefab)
        {
            Destroy(prefab);
            prefab = null;
            prefab = buildMenu.falsetowers[buildMenu.indexs];
            reticleVisual.reticlePrefab = prefab;
            prefab.GetComponent<BoxCollider>().enabled = false;
        }
        //Ÿ�� ��ġ ��ġ
        private Vector3 GetBuildPosition()
        {
            return hitPoint;
        }
        //Ÿ�� ��ġ ��ġ�� �����ش�
        private void IsBuildTower()
        {
            if (rayInteractor == null || reticleVisual == null)
                return;
            // ������ ��ȿ���� Ȯ��
            if (IsLineVisualValid())
            {
                if ((!buildMenu.isReticle && !buildMenu.istowerup) || !buildMenu.istowerup || !buildMenu.isReticle)
                {
                    reticleVisual.reticlePrefab = null;
                    reticlePrefabs = null;
                    return;
                }
                // ���� ��� ReticlePrefab Ȱ��ȭ�� �Ǿ��� ��ġ
                else if (reticleVisual.reticlePrefab == null && buildMenu.isReticle)
                {
                    SetReticlePrefab(reticlePrefabs);
                    if(reticleVisual.reticlePrefab != null)
                    {
                        rayInteractor.uiHoverEntered.AddListener(UIEnterReticle);
                        rayInteractor.uiHoverExited.AddListener(UIExitReticle);
                    }
                }
            }
            else
            {
                // ������ ���� ��� ReticlePrefab ��Ȱ��ȭ
                if (reticleVisual.reticlePrefab != null && buildMenu.isReticle)
                {
                    reticleVisual.reticlePrefab = null;
                }
            }
        }
        void UIEnterReticle(UIHoverEventArgs args)
        {
            buildMenu.isReticle = false;
        }
        void UIExitReticle(UIHoverEventArgs uIHover)
        {
            buildMenu.isReticle = true;
        }
        //Ÿ�� ��ġ
        private void SetBuildTower()
        {
            if (!buildMenu.istowerup) return;
            if (!buildManager.playerState.SpendMoney(buildMenu.towerinfo[buildMenu.indexs].cost1))
            {
                buildManager.warningWindow.ShowWarning("Not Enough Money");
                return;
            }
            if (buildManager.playerState.SpendMoney(buildMenu.towerinfo[buildMenu.indexs].cost1) && buildMenu.isReticle)
            {
                buildMenu.towerinfo[buildMenu.indexs].projectile.attack += CastleUpgrade.buffContents.atk;

                tower = Instantiate(buildMenu.towerinfo[buildMenu.indexs].projectile.tower,
                    GetBuildPosition(), Quaternion.identity);

                GameObject effgo = Instantiate(TowerImpectPrefab, tower.transform.position, Quaternion.identity);
                Destroy(effgo, 2f);

                tower.AddComponent<BoxCollider>();
                tower.AddComponent<TowerXR>();
                BoxCollider box = tower.GetComponent<BoxCollider>();
                TowerXR towerXR = tower.GetComponent<TowerXR>();
                towerXR.interactionLayers = layerMask;
                box.isTrigger = false;
                box.size = buildMenu.boxes[buildMenu.indexs].size + new Vector3(-0.5f,0,-0.5f);
                box.center = buildMenu.boxes[(buildMenu.indexs)].center;
                buildMenu.buildpro.SetActive(false);
            }
        }
    }
}