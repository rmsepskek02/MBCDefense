using UnityEngine;

namespace Defend.Enemy
{
    public class CameraController : MonoBehaviour
    {
        //�ʵ�
        #region Variables
        //ī�޶� �̵� �ӵ�
        public float moveSpeed = 10f;

        //��� ����
        public float border = 10f;

        //�� �̵� �ӵ�
        public float zoomSpeed = 10f;
        public float minY = 10f;
        public float maxY = 40f;

        //�̵� �Ұ���: true,  �̵�����:false;
        private bool isCannotMove = false;
        #endregion

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                isCannotMove = !isCannotMove;
            }

            if (isCannotMove)
                return;

            //wsad, arrow key �Է�
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                this.transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                this.transform.Translate(Vector3.back * Time.deltaTime * moveSpeed, Space.World);
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                this.transform.Translate(Vector3.left * Time.deltaTime * moveSpeed, Space.World);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.Translate(Vector3.right * Time.deltaTime * moveSpeed, Space.World);
            }

            //���콺 ��ġ���� �޾ƿͼ� �� ��ũ��
            float mouseX = Input.mousePosition.x;
            float mouseY = Input.mousePosition.y;

            if (mouseY >= (Screen.height - border) && mouseY < Screen.height)
            {
                this.transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
            }
            if (mouseY >= 0 && mouseY < border)
            {
                this.transform.Translate(Vector3.back * Time.deltaTime * moveSpeed, Space.World);
            }
            if (mouseX >= 0 && mouseX < border)
            {
                this.transform.Translate(Vector3.left * Time.deltaTime * moveSpeed, Space.World);
            }
            if (mouseX >= (Screen.width - border) && mouseX < Screen.width)
            {
                this.transform.Translate(Vector3.right * Time.deltaTime * moveSpeed, Space.World);
            }

            //���콺 �� ��ũ�� �� ó�� - ���� �ܾƿ�
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            Vector3 zoomMove = this.transform.position;
            zoomMove.y -= (scroll * 1000) * Time.deltaTime * zoomSpeed;
            zoomMove.y = Mathf.Clamp(zoomMove.y, minY, maxY);
            this.transform.position = zoomMove;

        }
    }
}