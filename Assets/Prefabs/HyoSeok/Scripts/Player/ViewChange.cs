using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Defend.Player
{
    public class ViewChange : MonoBehaviour
    {
        #region Variables
        //����
        PlayerState playerState;
        //ž��
        private bool isViewChange = false;
        private Transform currentTransform;
        private Vector3 originalPosition; // ���� ��ġ
        private Quaternion originalRotation; // ���� ȸ��

        //����͵�
        public GameObject[] hideObjects;
        //�÷��̾� ����
        public GameObject showObject;

        public InputActionProperty property;
        #endregion
        private void Start()
        {
            //����
            playerState = GetComponent<PlayerState>();

            //�ʱ�ȭ
            currentTransform = gameObject.transform;
            originalPosition = currentTransform.position;
            originalRotation = currentTransform.rotation;
        }
        private void Update()
        {
            if (property.action.WasCompletedThisFrame()|| Input.GetKeyDown(KeyCode.C))
            {
                ViewChangeGo();
            }
        }


        //ž����ȯ
        public void ViewChangeGo()
        {

            CinemachineBrain cinemachineBrain = gameObject.GetComponent<CinemachineBrain>();
            if (isViewChange)
            {
                // ž�信�� ���� ��ġ�� ���ư���
                currentTransform.position = originalPosition;
                currentTransform.rotation = originalRotation;
                cinemachineBrain.enabled = false;
                Time.timeScale = 1f;
                //��,���� ������ ���� ����
                for (int i = 0; i < hideObjects.Length; i++)
                {
                    hideObjects[i].SetActive(true);
                }
                showObject.SetActive(false);
            }
            else
            {
                // ���� ��ġ ����
                originalPosition = currentTransform.position;
                originalRotation = currentTransform.rotation;
                cinemachineBrain.enabled = true;

                //��,���� ������ ���� ����
                for (int i = 0; i < hideObjects.Length; i++)
                {
                    hideObjects[i].SetActive(false);
                }
                showObject.transform.position = originalPosition;
                Time.timeScale = 0f;
                //showObject.transform.position = originalPosition;
                showObject.SetActive(true);
            }

            isViewChange = !isViewChange;
        }
    }
}