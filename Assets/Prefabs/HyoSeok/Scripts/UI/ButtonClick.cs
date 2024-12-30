using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.XR;
using UnityEngine.UI;
namespace Defend.UI
{
    /// <summary>
    /// ��ư Ŭ�� �̺�Ʈ
    /// </summary>
    /*
    public class ButtonClick : MonoBehaviour
    {
        #region Variables
        public AudioClip clickSound;
        private AudioSource audioSource;
        public Button[] buttons;
        #endregion


        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = clickSound; 
            audioSource.playOnAwake = false; // �ڵ� ��� ����

         
            foreach (Button button in buttons)
            {
                button.onClick.AddListener(OnButtonClick);
            }
        }

        public void OnButtonClick()
        {
            audioSource.clip = clickSound;
            audioSource.Play();
        }
    }
    */

    public class ButtonClick : MonoBehaviour
    {
        public AudioClip clickSound;    //Ŭ�� ����
        public AudioClip potalSound;    //��Ż ����
        public AudioMixerGroup audioMixerGroup;
        [SerializeField] private AudioSource audioSource;

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.outputAudioMixerGroup = audioMixerGroup;
            }
            else
            {
                Debug.LogWarning("Audio Source not found");
            }
            AddButtons();  // ��� ��ư�� AudioSource�� �߰�
        }

        // ��� ��ư�� AudioSource�� �߰��ϰ� Ŭ�� �̺�Ʈ�� �����ϴ� �޼���
        void AddButtons()
        {
            // �� �� ��� ��ư�� ã���ϴ�.
            Button[] buttons = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            //ã�ƿ� ��� ��ư �� "Teleport"��ư���� �ڷ���Ʈ ����, �ٸ� ��� ��ư���� Ŭ�� ���� AddListener�� �̺�Ʈ �Ҵ�
            foreach (Button button in buttons)
            {
                if (button.name.Contains("Teleport"))
                {
                    button.onClick.AddListener(() => PlayClickSound(potalSound));  // Ŭ�� �̺�Ʈ�� ���� ��� �߰�
                }
                else
                {
                    button.onClick.AddListener(() => PlayClickSound(clickSound));  // Ŭ�� �̺�Ʈ�� ���� ��� �߰�
                }

            }
        }

        // Ŭ�� �� Ŭ�� ���带 ����ϴ� �޼���
        void PlayClickSound(AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}