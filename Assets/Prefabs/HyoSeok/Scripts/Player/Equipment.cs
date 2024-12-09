
using Defend.XR;
using UnityEngine;

namespace Defend.Player
{
    /// <summary>
    /// ���� �ý��� , ��ü
    /// </summary>
    public class Equipment : MonoBehaviour
    {
        #region Variables
        private enum ToolType { None, Axe, PickAxe }
        private ToolType currentTool = ToolType.None;

        [SerializeField] private GameObject axe;
        [SerializeField] private GameObject pickAxe;
        #endregion

        private void Start()
        {
            UpdateTool();
        }

        private void Update()
        {
            //if (InputManager.Instance == null)
            //{
            //    return;
            //}
            //   xŰ�Է½� ���ⱳü,
            if (InputManager.Instance.GetLeftSecondaryButton() || Input.GetKeyDown(KeyCode.Z))
            {
                
                Debug.Log("wepon swap");
                Swap();
                UpdateTool();
            }
        }

        //��������� ��ȯ
        void Swap()
        {
            switch (currentTool)
            {
                case ToolType.None:
                    currentTool = ToolType.Axe;
                    return;
                case ToolType.Axe:
                    currentTool = ToolType.PickAxe;
                    return;
                case ToolType.PickAxe:
                    currentTool = ToolType.None;
                    return;
            }

        }
        //���� ������Ʈ
        void UpdateTool()
        {
            axe.SetActive(currentTool == ToolType.Axe);
            pickAxe.SetActive(currentTool == ToolType.PickAxe);
        }

    }
}