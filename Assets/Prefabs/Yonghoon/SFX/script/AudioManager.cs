using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Defend.Utillity
{
    // AudioManager Ŭ������ Unity�� ����� �ͼ��� �����ϴ� ����� ����
    public class AudioManager : MonoBehaviour
    {
        // AudioMixer �迭�� ������Ʈ���� ����� ����� �ͼ����� ����
        // AudioMixer�� Unity�� ����� �ý��ۿ��� ���� ����� Ʈ���� �ͽ��ϰ� ó���ϴ� �� ��
        public AudioMixer[] AudioMixers;
        [SerializeField] private Slider m_AudioMasterSlider;
        [SerializeField] private Slider m_AudioBGMSlider;
        [SerializeField] private Slider m_AudioSFXSlider;

        private void Awake()
        {
            m_AudioMasterSlider.onValueChanged.AddListener(AudioUtility.SetMasterVolume);
            m_AudioBGMSlider.onValueChanged.AddListener(AudioUtility.SetBGMVolume);
            m_AudioSFXSlider.onValueChanged.AddListener(AudioUtility.SetSFXVolume);
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

    }
}