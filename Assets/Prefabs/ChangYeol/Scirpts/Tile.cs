using Defend.Tower;
using Defend.Utillity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactors.Visuals;

namespace Defend.UI
{
    public class Tile : XRSimpleInteractable
    {
        #region Variables
        //Ÿ�Ͽ� ��ġ�� Ÿ�� ���ӿ�����Ʈ ��ü
        [HideInInspector] public GameObject tower;
        //���� ���õ� Ÿ�� TowerInfo(prefab, cost, ....)
        [HideInInspector] public TowerInfo towerInfo;
        //����Ŵ��� ��ü
        private BuildManager buildManager;
        //��ġ�ϸ� �����Ǵ� ����Ʈ
        public GameObject[] TowerImpectPrefab;
        //�÷��̾� ��ġ
        public BuildMenu buildMenu;
        //�÷��̾� �޼� ����
        public XRRayInteractor leftRayInteractor;
        //�÷��̾� �޼� ��ƼŬ ���־�
        [HideInInspector]public XRInteractorReticleVisual leftReticleVisual;
        //��ġ�� Ÿ���� �����ִ� ���� ������Ʈ
        [SerializeField] public GameObject reticlePrefabs;
        //Ʈ���� Ű �Է�
        public InputActionProperty property;
        public InputActionProperty leftSelect;
        //Ÿ�� ������ ������ ���̾� ����
        public InteractionLayerMask layerMask;
        //Ÿ�� ��ġ ��ġ
        private Vector3 leftHitPoint;
        // �ͷ���
        public GameObject terrain;
        #endregion

        private void Start()
        {
            //�ʱ�ȭ
            buildManager = BuildManager.Instance;
            leftReticleVisual = leftRayInteractor.GetComponent<XRInteractorReticleVisual>();
            //rightReticleVisual = rightRayInteractor.GetComponent<XRInteractorReticleVisual>();
        }
        private void Update()
        {
            //Trigger ��ư ������ reticle = null
            if (property.action.WasPressedThisFrame())
            {
                leftReticleVisual.reticlePrefab = null;
                //buildMenu.istrigger = false;
                return;
            }
            /*if (leftSelect.action.WasPressedThisFrame() && leftReticleVisual.enabled == true)
            {
                SetBuildTower();
            }*/
            if (leftReticleVisual == null/* || rightReticleVisual == null*/) return;

            // Ray�� UI�� ���ϰ� �ִ� ���
            if (leftRayInteractor.TryGetCurrentUIRaycastResult(out RaycastResult result))
            {
                leftReticleVisual.enabled = false;
            }
            else
            {
                IsBuildTower();
            }
        }
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            //Ÿ�� ��ġ
            SetBuildTower();
        }
        /*// ������ ��ȿ���� �˻��ϴ� �Լ�
        private bool IsLineVisualValid()
        {
            // XRRayInteractor�� ��Ʈ ������ ������
            if (leftRayInteractor.TryGetHitInfo(out Vector3 hitPosition, out Vector3 hitNormal,
                out int hitIndex, out bool isValidTarget))
            {
                return isValidTarget; // Ÿ���� ��ȿ���� ��ȯ
            }
            return false;
        }*/
        //Ÿ�� ��ġ ��ġ
        private Vector3 GetBuildPosition()
        {
            return leftHitPoint;
        }
        //Ÿ�� ��ġ ��ġ�� �����ش�
        private void IsBuildTower()
        {
            if (leftRayInteractor == null || leftReticleVisual == null)
                return;

            if (leftRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit lefthit))
            {
                // ���� ����ĳ��Ʈ ��Ʈ ���� ��������
                leftHitPoint = lefthit.point;

                // Terrain���� Ȯ��
                if (lefthit.collider.gameObject == terrain)
                {
                    leftReticleVisual.enabled = true;
                }
                else
                {
                    leftReticleVisual.enabled = false;
                }
            }
        }
        //Ÿ�� ��ġ
        private void SetBuildTower()
        {
            if (leftReticleVisual.reticlePrefab == null) return;
            if(leftRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit lefthit))
            {
                leftHitPoint = lefthit.point;
                TowerXR tower = lefthit.collider.gameObject.GetComponent<TowerXR>();
                if (tower != null)
                {
                    return;
                }
            }
            if (buildManager.playerState.SpendMoney(buildManager.towerBases[buildMenu.indexs].GetTowerInfo().cost1) 
                //&& buildMenu.isReticle 
                && buildMenu.towerinfo[buildMenu.indexs].isLock)
            {
                tower = Instantiate(buildManager.towerBases[buildMenu.indexs].GetTowerInfo().projectile.tower,
                    GetBuildPosition(), Quaternion.identity);
                GameObject effgo = Instantiate(TowerImpectPrefab[0], tower.transform.position, Quaternion.identity);
                Destroy(effgo, 2f);
                tower.transform.parent = buildManager.transform;
                //ȿ���� ���
                AudioUtility.CreateSFX(buildManager.towerBuildSound, tower.transform.position, AudioUtility.AudioGroups.EFFECT, 1);

                tower.AddComponent<BoxCollider>();
                tower.AddComponent<TowerXR>();
                BoxCollider box = tower.GetComponent<BoxCollider>();
                TowerXR towerXR = tower.GetComponent<TowerXR>();
                towerXR.interactionLayers = layerMask;
                box.isTrigger = false;
                box.size = buildMenu.boxes[buildMenu.indexs].size + new Vector3(0.5f, 0, 0.5f);
                box.center = buildMenu.boxes[(buildMenu.indexs)].center;
            }
        }
    }
}