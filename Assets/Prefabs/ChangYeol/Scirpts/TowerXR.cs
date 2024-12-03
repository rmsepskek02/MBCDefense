using Defend.Tower;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Defend.UI
{
    public class TowerXR : XRGrabInteractable
    {
        #region Variables
        public TowerInfo towerInfo;

        private NormalTower normal;

        //����Ŵ��� ��ü
        private BuildManager buildManager;
        #endregion

        private void Start()
        {
            //����
            Rigidbody rigidbody = this.GetComponent<Rigidbody>();
            normal = GetComponent<NormalTower>();
            //�ʱ�ȭ
            buildManager = BuildManager.Instance;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationX;
            rigidbody.constraints = RigidbodyConstraints.FreezePositionX;
            rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                buildManager.SelectTile(this);
            }
        }
        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            //buildManager.DeselectTile();
        }
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            buildManager.SelectTile(this);
        }

        /*protected override void OnActivated(ActivateEventArgs args)
        {
            base.OnActivated(args);
            buildManager.SelectTile(this,normal);
        }*/
        void OnActionUI()
        {
            Debug.Log("act");
            buildManager.SelectTile(this);
        }
    }
}