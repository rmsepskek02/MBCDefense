
using Defend.XR;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
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
        public Image targetImage;
        public Sprite handSprite; 
        public Sprite axeSprite;
        public Sprite pickaxeSprite;

        [SerializeField] private GameObject axe;
        [SerializeField] private GameObject pickAxe;
        public InputActionProperty property;
        #endregion

        private void Start()
        {
            targetImage.sprite = axeSprite;
            UpdateTool();
        }

        private void Update()
        {
            //if (InputManager.Instance == null)
            //{
            //    return;
            //}
            //   xŰ�Է½� ���ⱳü,
            if (property.action.WasCompletedThisFrame() || Input.GetKeyDown(KeyCode.Z))
            {
                
                //Debug.Log("wepon swap");
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
                    targetImage.sprite = pickaxeSprite;
                    return;
                case ToolType.Axe:
                    currentTool = ToolType.PickAxe;
                    targetImage.sprite = handSprite;
                    return;
                case ToolType.PickAxe:
                    currentTool = ToolType.None;
                    targetImage.sprite = axeSprite;
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