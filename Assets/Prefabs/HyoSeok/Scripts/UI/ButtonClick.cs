using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
namespace Defend.UI
{
    /// <summary>
    /// ��ư Ŭ�� �̺�Ʈ
    /// </summary>
    public class ButtonClick : MonoBehaviour
    {
        #region Variables
        public AudioClip clickSound;
        private AudioSource audioSource;
        public Button[] buttons;
        #endregion


        void Start()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = clickSound; 
            audioSource.playOnAwake = false; // �ڵ� ��� ����

         
            foreach (Button button in buttons)
            {
                button.onClick.AddListener(OnButtonClick);
            }
        }

        public void OnButtonClick()
        {
            audioSource.Play();
        }
    }
}