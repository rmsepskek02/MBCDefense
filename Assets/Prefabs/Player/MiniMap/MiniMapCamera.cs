using UnityEngine;

namespace Defend.UI
{
    public class MiniMapCamera : MonoBehaviour
    {
        #region Variables
        //player ��ġ
        public Transform player;
        //�̴ϸ� UI
        public GameObject miniMapUI;
        //���� ��ġ
        private Transform currentPosition;
        //

        //�̴ϸ� ī�޶�
        private Camera miniMapcamera;
        #endregion
        private void Start()
        {
            //����
            miniMapcamera = GetComponent<Camera>();
            //���� player ��ġ ����
            //currentPosition = Camera.main.transform;
        }

        private void LateUpdate()
        {
            //�̴ϸʿ��� �������� �̴ϸ� ī�޶� player ��ġ�� ����ٴѴ�
            miniMapcamera.transform.position = new Vector3(player.transform.position.x, miniMapcamera.transform.position.y, player.transform.position.z);
            //���� player ��ġ ����
            currentPosition = Camera.main.transform;
        }
        //������ ���� UI�� ��Ȱ��ȭ �������� ���� ���� Ȱ��ȭ
        void PlayerMoveUI()
        {
            //player�� �����̰� �ִ��� �ƴ���
            if (player.position == currentPosition.position)
            {
                miniMapUI.SetActive(false);
            }
            else
            {
                miniMapUI.SetActive(true);
            }
        }
    }
}