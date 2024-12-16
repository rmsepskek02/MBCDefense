using Defend.Enemy;
using Defend.Player;
using TMPro;
using Unity.Cinemachine;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Defend.UI
{
    public class PlayerHandController : MonoBehaviour
    {
        #region Variables
        //����
        PlayerState playerState;
        ListSpawnManager listSpawnManager;
        public TextMeshProUGUI[] texts;
  
        public GameObject uiOnButton;
        public GameObject viewCanvas;

  
        //��ư �����¿���
        private bool isOnUi;

        #endregion

        private void Start()
        {
            //����
            playerState = GetComponent<PlayerState>();
            listSpawnManager = Object.FindAnyObjectByType<ListSpawnManager>();
        

        }

        private void Update()
        {

            if (listSpawnManager != null)
            {
                texts[0].text = listSpawnManager.countdown.ToString("F0"); //����Ÿ�̸�
            }
            texts[1].text = playerState.FormatMoney();  //��
            texts[2].text = playerState.tree.ToString();  //����
            texts[3].text = playerState.rock.ToString();  //��
            //texts[4].text = listSpawnManager.waveCount.ToString();  //�������
            texts[5].text = ListSpawnManager.enemyAlive.ToString();  //���糲���ִ����Ǽ�

        }



        //ui �ѱ�
        public void ShowButton()
        {
            if (isOnUi == true)
            {
                uiOnButton.SetActive(true);
                viewCanvas.SetActive(false);
              
            }
            else
            {
                uiOnButton.SetActive(false);
                viewCanvas.SetActive(true);
            
            }
        }

        //ui ����
        public void hideButton()
        {
            if (isOnUi == true)
            {
                uiOnButton.SetActive(false);
                viewCanvas.SetActive(true);

            }
            else
            {
                uiOnButton.SetActive(true);
                viewCanvas.SetActive(false);
              
            }
        }

      

       

    }
}