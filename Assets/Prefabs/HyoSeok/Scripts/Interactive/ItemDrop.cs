using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


namespace Defend.item
{
    /// <summary>
    /// ������ ��� 
    /// </summary>
    public class ItemDrop : MonoBehaviour
    {
       
        #region Variables
        public List<Item> items = new List<Item>();
        #endregion

        //������ ���� �����ؼ� �Ⱦ�(�����)
        protected Item PickItem()
        {
            int sum = 0;
            foreach (var item in items)
            {
                //��� ������ ���� ���ϱ�
                sum += item.weight;
            }

            var rnd = Random.Range(0, sum);

            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                if (item.weight > rnd) return item;
                else rnd -= item.weight;
            }

            return null;
        }

        ////������ ����
        //public void DropItem(Vector3 pos)
        //{
        //    var item = PickItem();
        //    if (item == null) return;

        //    Instantiate(item.prefab, pos, Quaternion.identity);
        //}
    }
}