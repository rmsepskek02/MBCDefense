using Defend.XR;
using UnityEngine;

namespace Defend.UI
{
    public class PlayerUI : MonoBehaviour
    {
        #region Variables
        [SerializeField] private GameObject gameMenuCanvas;
        [SerializeField] private GameObject buildCanvas;
        [SerializeField] private GameObject upgradeCanvas;
        [SerializeField] private GameObject shopCanvas;
        [SerializeField] private GameObject skilCanvas;
        private bool isOpen;
        #endregion

        private void Update()
        {
            //Y��ư ������ �޴� ����
            if (InputManager.Instance.GetRightSecondaryButton() || Input.GetKeyDown(KeyCode.X))
            {
                if (!isOpen)
                {
                    // �޴��� ������ ���� ���, gameMenuCanvas ����
                    isOpen = true;
                    gameMenuCanvas.SetActive(true);
                }
                else
                {
                  
                    CloseAllCanvases();
                }
            }
        }
        // �޴��� ���� �ִ� ��� ��� ĵ���� �ݱ�
        public void CloseAllCanvases()
        {
            isOpen = false;
            gameMenuCanvas.SetActive(false);
            buildCanvas.SetActive(false);
            upgradeCanvas.SetActive(false);
            shopCanvas.SetActive(false);
            skilCanvas.SetActive(false);
        }

        public void Build()
        {
            //Debug.Log("buildopen");
            gameMenuCanvas.SetActive(false);
            buildCanvas.SetActive(true);
        }

        public void Upgrade()
        {
            //Debug.Log("Upgradeopen");
            gameMenuCanvas.SetActive(false);
            upgradeCanvas.SetActive(true );
        }

        public void Shop()
        {
            //Debug.Log("Shopopen");
            gameMenuCanvas.SetActive(false);
            shopCanvas.SetActive(true);
        }

        public void Skill()
        {
            //Debug.Log("Skillopen");
            gameMenuCanvas.SetActive(false);
            skilCanvas.SetActive(true);
        }
    }
}