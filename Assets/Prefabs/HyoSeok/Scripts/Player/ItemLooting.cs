using Mono.Cecil;
using UnityEngine;
using static Defend.Interactive.Resources;


namespace Defend.item
{
    public class ItemLooting : MonoBehaviour
    {
        #region Variables
 

        [SerializeField] private float distance; //������ ���� �Ÿ�
        [SerializeField] private float speed;  //����ӵ�
        #endregion
      
        public ItemLooting()
        {
            //swich�� ���� ����
            //ResourceTypeEnum.Tree

            ////�±� resource�� ��� �÷��̾������� ������� 

            //// �÷��̾�� ����
            //ResourceManager.Instance.AddResources(currentResourceType.amount, currentResourceType.name.ToString());
        }
    }
}