using System;
using UnityEngine;

namespace Defend.UI
{
    public class PotalController : MonoBehaviour
    {
        #region Variables

        //��Ż ��ư
        public GameObject[] potalButton;
        public Transform[] potalsTransform;
        //�÷��̾� ��ġ��
        public Transform playerTransform;
        #endregion

   

        //��Ż �̵� ��ư
        public void Teleportation(int index)
        {
            if (index >= 0 && index < potalsTransform.Length)
            {
                playerTransform.position = potalsTransform[index].position;
            }
        }

        
    }
}