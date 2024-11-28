
using UnityEngine;

namespace Defend.Player
{
    public class PlayerState : MonoBehaviour
    {
        #region Variables
        public float money = 100; //��
        public float health = 20; //ü��(��)
        public float tree; //�ڿ�(����)
        public float rock; //�ڿ�(��)
        #endregion

        //�� , ���� , �� ���� üũ
        public void ShowStatus()
        {
            //����Ű������ ���̰��ϰų� â �׻������
            Debug.Log($"Money = {money} Tree = {tree} Rock = {rock}");
        }

        //������ �Ա�
        public void TakeDamage(float amount)
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
        public void AddMoney(float amount)
        {
            money += amount;
        }

        //�� �Һ�
        public bool SpendMoney(float amount)
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
        public void AddTree(float amount)
        {
            tree += amount;
        }
        //�� ����
        public void AddRock(float amount)
        {
            rock += amount;
        }


        // �ڿ�(���� , �� �Һ�)
        public bool SpendResources(float requiredTree = 0, float requiredRock = 0)
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