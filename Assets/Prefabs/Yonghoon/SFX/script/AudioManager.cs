using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Defend.Utillity;
using Defend.Enemy;

namespace Defend.Audio
{
    // AudioManager Ŭ������ Unity�� ����� �ͼ��� �����ϴ� ����� ����
    public class AudioManager : MonoBehaviour
    {
        // AudioMixer �迭�� ������Ʈ���� ����� ����� �ͼ����� ����
        // AudioMixer�� Unity�� ����� �ý��ۿ��� ���� ����� Ʈ���� �ͽ��ϰ� ó���ϴ� �� ��
        public AudioMixer[] AudioMixers;
        [SerializeField] public Slider m_AudioMasterSlider;
        [SerializeField] public Slider m_AudioBGMSlider;
        [SerializeField] public Slider m_AudioSFXSlider;

        public AudioClip peacefulBGM;
        public AudioClip direBGM;
        private AudioClip currentBGM; // ���� ��� ���� BGM

        private AudioSource audioSource;

        private void Awake()
        {
            m_AudioMasterSlider.onValueChanged.AddListener(value => AudioUtility.SetVolume(value, Constants.AUDIO_UTIL_MASTER));
            m_AudioBGMSlider.onValueChanged.AddListener(value => AudioUtility.SetVolume(value, Constants.AUDIO_UTIL_BGM));
            m_AudioSFXSlider.onValueChanged.AddListener(value => AudioUtility.SetVolume(value, Constants.AUDIO_UTIL_EFFECT));

            audioSource = GetComponent<AudioSource>();
            currentBGM = audioSource.clip;
        }

        private void Start()
        {
            //InitializeSliders();
        }

        private void Update()
        {
            // ���� BGM ���¸� ����
            AudioClip targetBGM = ListSpawnManager.enemyAlive > 0 || ListSpawnManager.isSpawn ? direBGM : peacefulBGM;

            // BGM�� ����Ǿ��� ���� ��ü
            if (currentBGM != targetBGM)
            {
                ChangeBGM(targetBGM);
            }
        }

        private void ChangeBGM(AudioClip clip)
        {
            currentBGM = clip; // ���� BGM ������Ʈ
            audioSource.clip = clip;
            audioSource.playOnAwake = true;
            audioSource.loop = true;
            audioSource.Play();
        }


        /// <summary>
        /// ������ ���(subPath)�� �ش��ϴ� AudioMixerGroup���� ã���ϴ�.
        /// </summary>
        /// <param name="subPath">����� �ͼ� �׷��� ��� (��: "Master/Effect").</param>
        /// <returns>ù ��°�� �߰ߵ� AudioMixer�� ��Ī �׷� �迭�� ��ȯ�ϰų�, ������ null�� ��ȯ.</returns>
        public AudioMixerGroup[] FindMatchingGroups(string subPath)
        {
            for (int i = 0; i < AudioMixers.Length; i++)
            {
                AudioMixerGroup[] results = AudioMixers[i].FindMatchingGroups(subPath);
                if (results != null && results.Length != 0)
                {
                    return results;
                }
            }

            return null;
        }

        /// <summary>
        /// AudioMixer�� �Ķ���� ���� �����մϴ�.
        /// </summary>
        /// <param name="name">������ �Ķ������ �̸� (��: "Volume").</param>
        /// <param name="value">������ �� (float Ÿ��).</param>
        public void SetFloat(string name, float value)
        {
            for (int i = 0; i < AudioMixers.Length; i++)
            {
                if (AudioMixers[i] != null)
                {
                    AudioMixers[i].SetFloat(name, value);
                    InitializeSliders();
                }
            }
            
        }

        /// <summary>
        /// AudioMixer�� �Ķ���� ���� �����ɴϴ�.
        /// </summary>
        /// <param name="name">������ �Ķ������ �̸�.</param>
        /// <param name="value">�ش� �Ķ���� ���� �Ҵ�� ���� (out Ű���� ���).</param>
        public void GetFloat(string name, out float value)
        {
            value = 0f;
            for (int i = 0; i < AudioMixers.Length; i++)
            {
                if (AudioMixers[i] != null)
                {
                    AudioMixers[i].GetFloat(name, out value);
                    break;
                }
            }
        }
        private void InitializeSliders()
        {
            // AudioManager���� ���� ���� ������ �����̴��� �ݿ�
            m_AudioMasterSlider.value = AudioUtility.GetVolume(Constants.AUDIO_UTIL_MASTER);
            m_AudioBGMSlider.value = AudioUtility.GetVolume(Constants.AUDIO_UTIL_BGM);
            m_AudioSFXSlider.value = AudioUtility.GetVolume(Constants.AUDIO_UTIL_EFFECT);
        }
    }
}