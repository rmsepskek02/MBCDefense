using Defend.Enemy;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Defend.UI
{
    public class EnemyXRSimple : XRSimpleInteractable
    {
        #region Variables
        //�ٸ� ��ũ��Ʈ���� ����ϱ� ���� public
        public EnemyState enemyState;

        private BuildManager buildManager;
        #endregion
        private void Start()
        {
            //�ʱ�ȭ
            buildManager = BuildManager.Instance;
        }
        /*protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            enemyState = buildManager.GetEnemyToBuild();
            buildManager.SetEnemyToBuild(enemyState);
            buildManager.SelectEnemy(this);
        }
        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            
            buildManager.DeselectEnemy();
        }*/
    }
}
