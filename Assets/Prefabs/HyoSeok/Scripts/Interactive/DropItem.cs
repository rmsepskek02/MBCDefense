using Defend.Player;
using Defend.Interactive;
using UnityEngine;

namespace Defend.item
{
    public class DropItem : MonoBehaviour
    {
        #region Variables
        public float amount;       // �ڿ� ��
        //�������� ���׷��̵� �Ұ�
        public float distance = 5f; // �÷��̾�� ���� �Ÿ�
        public float speed = 1.0f; // �÷��̾�� ���� �ӵ�
        private PlayerState playerState;
        #endregion

        private void Awake()
        {
            playerState = Object.FindAnyObjectByType<PlayerState>(); // PlayerState�� ã��
        }

        private void Update()
        {
            MagnetItem();
        }

        //�÷��̾ �Ÿ��� ������ �÷��̾�� �̵�
        void MagnetItem()
        {

        }

        //������ ����
        private void OnTriggerEnter(Collider other)
        {
            OnDestroy();
        }

        //���Ž� ����Ʈ������ �ڿ��ѱ��
        private void OnDestroy()
        {
            //ŉ�� ����Ʈ

            //�ڿ��ѱ��
            //ResourceManager.Instance.AddResources(currentResourceType.amount, currentResourceType.name.ToString());
        }
    }
}