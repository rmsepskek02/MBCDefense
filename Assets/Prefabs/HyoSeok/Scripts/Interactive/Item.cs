using UnityEngine;

namespace Defend.item
{
    [System.Serializable]
    public class Item
    {
        #region Variables
        public string itemName;
        public Sprite itemIcon;
        //����
        public float count;
        //������ ������
        public Transform prefab;
        //����
        [Multiline]
        public string description;
        //�� �������� ���� ����
        public int weight;  
        #endregion
    }
}