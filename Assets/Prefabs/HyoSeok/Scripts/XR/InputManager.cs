using UnityEngine;
using UnityEngine.XR;
namespace Defend.XR
{
    public class InputManager : MonoBehaviour
    {
        #region Variables
        public static InputManager Instance { get; private set; }

        private InputDevice leftHandDevice;
        private InputDevice rightHandDevice;
        #endregion

        private void Awake()
        {
            // �̱��� �ν��Ͻ� ����
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            // �Է� ��ġ �ʱ�ȭ
            leftHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            rightHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        }

        // ���� ���� �⺻ ��ư (A ��ư)
        public bool GetLeftPrimaryButton()
        {
            //Debug.Log("222222");
            leftHandDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool value);
            return value;
        }

        // ������ ���� �⺻ ��ư (B ��ư)
        public bool GetRightPrimaryButton()
        {
            rightHandDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool value);
            return value;
        }

        // ���� ���� ���� ��ư (X ��ư)
        public bool GetLeftSecondaryButton()
        {
            //Debug.Log("33333");
            leftHandDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool value);
            return value;
        }

        // ������ ���� ���� ��ư (Y ��ư)
        public bool GetRightSecondaryButton()
        {
            rightHandDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool value);
            return value;
        }

        // ���� ���� �׸� ��ư (�׸��� ���� ��)
        public bool GetLeftGripButton()
        {
            leftHandDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool value);
            return value;
        }

        // ������ ���� �׸� ��ư (�׸��� ���� ��)
        public bool GetRightGripButton()
        {
            rightHandDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool value);
            return value;
        }

        // ���� ���� Ʈ���� ��ư (Ʈ���Ÿ� ���� ��)
        public bool GetLeftTriggerButton()
        {
            leftHandDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool value);
            return value;
        }

        // ������ ���� Ʈ���� ��ư (Ʈ���Ÿ� ���� ��)
        public bool GetRightTriggerButton()
        {
            rightHandDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool value);
            return value;
        }

        // ���� ���� �޴� ��ư (�޴� ����)
        public bool GetLeftMenuButton()
        {
            leftHandDevice.TryGetFeatureValue(CommonUsages.menuButton, out bool value);
            return value;
        }

        // ������ ���� �޴� ��ư (�޴� ����)
        public bool GetRightMenuButton()
        {
            rightHandDevice.TryGetFeatureValue(CommonUsages.menuButton, out bool value);
            return value;
        }

        // ���� ���� ��ġ�е� Ŭ�� ��ư
        public bool GetLeftTouchpadButton()
        {
            leftHandDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool value);
            return value;
        }

        // ������ ���� ��ġ�е� Ŭ�� ��ư
        public bool GetRightTouchpadButton()
        {
            rightHandDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool value);
            return value;
        }

        // ���� ���� ��ġ�е� �Ƴ��α� �Է�
        public Vector2 GetLeftTouchpadAxis()
        {
            leftHandDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 value);
            return value;
        }

        // ������ ���� ��ġ�е� �Ƴ��α� �Է�
        public Vector2 GetRightTouchpadAxis()
        {
            rightHandDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 value);
            return value;
        }

        // ���� ���� ��ġ�е� ��ġ ����
        public bool GetLeftTouchButton()
        {
            leftHandDevice.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out bool value);
            return value;
        }

        // ������ ���� ��ġ�е� ��ġ ����
        public bool GetRightTouchButton()
        {
            rightHandDevice.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out bool value);
            return value;
        }
    }
}