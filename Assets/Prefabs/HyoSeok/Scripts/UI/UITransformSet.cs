
using UnityEngine;


namespace Defend.UI
{
    /// <summary>
    /// ui ��ġ�� �÷��̾� �տ� �ٿ�� �뵵
    /// </summary>
    public class UITransformSet : MonoBehaviour
    {
        #region Variables
        public GameObject[] Canvases;
        public Transform player;
        [SerializeField] private float distance = 1.5f;
        public float yPos = 0;
        #endregion

        //�տ�����
        void Update()
        {
            for(int i = 0; i < Canvases.Length; i++)
            {
                //show ����
                if (Canvases[i].activeSelf)
                {
                    Canvases[i].transform.position = player.position + new Vector3(player.forward.x, yPos, player.forward.z).normalized * distance;
                    Canvases[i].transform.LookAt(new Vector3(player.position.x, Canvases[i].transform.position.y, player.position.z));
                    Canvases[i].transform.forward *= -1;
                }
            }
         
        }
    }


}