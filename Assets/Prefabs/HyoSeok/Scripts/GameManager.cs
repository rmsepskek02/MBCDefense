using Defend.Enemy;
using Defend.Player;
using MyVrSample;
using Unity.VisualScripting;
using UnityEngine;

namespace Defend.Manager
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
        //����
        PlayerState playerState;
        SpawnManager SpawnManager;

        //ui
        public GameObject clearUI;
        public GameObject gameoverUI;

  

        //sfs
        public AudioSource clearSound;
        public AudioSource gameoverSound;
        #endregion



        void GameClear()
        {

            if (SpawnManager.enemyAlive <= 0)
            {
                //����Ŭ���� â �߱�
                clearUI.SetActive(true);
            
                //��������
                Time.timeScale = 0; 
            }

        }

        void GameOver()
        {

            if (playerState.health <= 0)
            {
                //���ӿ��� â ����
                gameoverUI.SetActive(true);
                //���ӿ��� ����

                //��������
                Time.timeScale = 0;
            }


        }
    }
}