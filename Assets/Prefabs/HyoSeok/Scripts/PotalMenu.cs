using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;

namespace MyVrSample
{
    /// <summary>
    /// ������ ��Ż �޴��� ����
    /// </summary>
    public class PotalMenu : MonoBehaviour
    {
        #region Variables

        //[SerializeField] private float distance = 1.5f;
        //��Ż
        public GameObject potalMenu;
        //��Ż ��ư
        public GameObject[] potalButton;
        public Transform[] potalsTransform;
        //�÷��̾� ��ġ��
        public Transform playerTransform;

        //��Ż ����
        public AudioClip potalSound;
        private AudioSource audioSource;

      
        #endregion

        private void Start()
        {
        
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        //��Ż UI ����
        public void OnPotalUI()
        {
            potalMenu.SetActive(true);

        }

        //��Ż �̵� ��ư
        public void Teleportation(int index)
        {
          
            if (index >= 0 && index < potalsTransform.Length)
            {
                playerTransform.position = potalsTransform[index].position;
               
            }
        }

        //UI �ݱ�
        public void OffPotalUI()
        {
            potalMenu.SetActive(false);
        }

        //��Ż ����
        public void OnButtonClick()
        {
            audioSource.clip = potalSound;
            audioSource.Play();

     
        }
    }
}