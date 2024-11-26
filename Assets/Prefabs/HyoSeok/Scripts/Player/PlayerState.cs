
using UnityEngine;

namespace Defend.Player
{
    public class PlayerState : MonoBehaviour
    {
        #region Variables
        public int money = 100; //��
        public int health = 20; //ü��(��)
        public int tree; //�ڿ�(����)
        public int rock; //�ڿ�(��)
        #endregion

        private void Update()
        {
       
        }

        //�� , ���� , �� ���� üũ
        public void ShowStatus()
        {
            //����Ű������ ���̰��ϰų� â �׻������
            Debug.Log($"Money = {money} Tree = {tree} Rock = {rock}");
        }

        //������ �Ա�
        public void TakeDamage(int amount)
        {
            if (health <= amount)
            {
                health = 0;

                //GameOver
                GameOver();
            }
        }

        //GameOver
        public void GameOver()
        {
            //���ӿ��� �����߉�
            Debug.Log("GameOver");
        }

        //�� ����
        public void AddMoney(int amount)
        {
            money += amount;
        }

        //�� �Һ�
        public bool SpendMoney(int amount)
        {
            if (money >= amount)
            {
                money -= amount;    //�� ����ϸ� �Һ�
                return true;
            }
            else
            {
                //���� �Ұ� ui����
                Debug.Log("Not Enough Money");
                return false;       //�� �����ϸ� ���źҰ�
            }
        }
        //���� ����
        public void AddTree(int amount)
        {
            tree += amount;
        }
        //�� ����
        public void AddRock(int amount)
        {
            rock += amount;
        }


        // �ڿ�(���� , �� �Һ�)
        public bool SpendResources(int requiredTree = 0, int requiredRock = 0)
        {
            // �ڿ��� ������� Ȯ��
            if (tree >= requiredTree && rock >= requiredRock)
            {
                // �ڿ� �Һ�
                tree -= requiredTree;
                rock -= requiredRock;
                return true;
            }
            else
            {
                //���� �Ұ� ui����
                Debug.Log("Not Enough Money");
                return false;
            }
        }
    }
}