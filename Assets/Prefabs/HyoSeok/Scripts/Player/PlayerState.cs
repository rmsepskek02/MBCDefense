
using Defend.Manager;
using Defend.UI;
using UnityEngine;

namespace Defend.Player
{
    public class PlayerState : MonoBehaviour
    {
        #region Variables
        //����Ŵ��� ��ü
        private BuildManager buildManager;
        public float money = 100; //��
        //public float health = 20; //ü��(��)
        public float tree; //�ڿ�(����)
        public float rock; //�ڿ�(��)

        #endregion

        void Start()
        {
            //�ʱ�ȭ
            buildManager = BuildManager.Instance;
            money = GameManager.Instance.data.money;
            tree = GameManager.Instance.data.tree;
            rock = GameManager.Instance.data.rock;
        }
       public string FormatMoney(float amount = 0)
        {
            amount = money;
            if (amount >= 1000)
            {
                return (amount / 1000).ToString("F1") + "K";
            }
            return amount.ToString();
        }

        //�� , ���� , �� ���� üũ
        public void ShowStatus()
        {
            //����Ű������ ���̰��ϰų� â �׻������
            Debug.Log($"Money = {money} Tree = {tree} Rock = {rock}");
        }

        ////������ �Ա�
        //public void TakeDamage(float amount)
        //{
        //    if (health <= amount)
        //    {
        //        health = 0;

        //        //GameOver
        //        GameOver();
        //    }
        //}

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
                buildManager.warningWindow.ShowWarning("Not Enough Money");
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
                buildManager.warningWindow.ShowWarning("Not Enough Money");
                return false;
            }
        }
    }
}