using TMPro;
using Unity.Cinemachine;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Defend.UI
{
    public class PlayerHandController : MonoBehaviour
    {
        #region Variables
        //�׽�Ʈ�� �ð�
        public TextMeshProUGUI timeText;
        public GameObject viewButton;
        public GameObject uiOnButton;
        private float time;

        //��ư �����¿���
        private bool isOnUi;

        //ž��
        private bool isViewChange = false;
        public Transform currentTransform;
        private Vector3 originalPosition; // ���� ��ġ
        private Quaternion originalRotation; // ���� ȸ��
        #endregion

        private void Start()
        {
            //�ʱ�ȭ
            currentTransform = gameObject.transform;
            originalPosition = currentTransform.position;
            originalRotation = currentTransform.rotation;
        }

        private void Update()
        {
            time += Time.deltaTime;
            timeText.text = time.ToString("F2");
        }

        public void ViewChange()
        {
            Debug.Log(isViewChange);
            CinemachineBrain cinemachineBrain = gameObject.GetComponent<CinemachineBrain>();
            if (isViewChange==true)
            {
                cinemachineBrain.enabled = isViewChange;
            }
            else
            {
                // ���� ��ġ�� ���ư���
                cinemachineBrain.enabled = isViewChange;
                currentTransform.position = originalPosition;
                currentTransform.rotation = originalRotation;
            }

            isViewChange = !isViewChange;
        }

        public void ViewButton()
        {
            if (isOnUi == true)
            {
                uiOnButton.SetActive(true);
                viewButton.SetActive(false);
            }
            else
            {
                uiOnButton.SetActive(false);
                viewButton.SetActive(true);
            }
        }
    }
}