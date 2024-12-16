using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;
using Defend.Utillity;

public class AudioMixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer m_AudioMixer;
    [SerializeField] private Slider m_MusicMasterSlider;
    [SerializeField] private Slider m_MusicBGMSlider;
    [SerializeField] private Slider m_MusicSFXSlider;

    private void Awake()
    {
        m_MusicMasterSlider.onValueChanged.AddListener(AudioUtility.SetMasterVolume);
        m_MusicBGMSlider.onValueChanged.AddListener(AudioUtility.SetBGMVolume);
        m_MusicSFXSlider.onValueChanged.AddListener(AudioUtility.SetSFXVolume);
    }

    /*
    Mathf.Log10�� ����ϴ� ������ �����̴��� ���� ���� �α� �����Ϸ� ��ȯ�Ͽ� ���ú� ������ ���� ������ ���߰�, ����ڿ��� �� �ڿ������� ���� ��ȭ�� ����
    Mathf.Log10(flaot value)�Լ��� value�� 0�϶� -Infinity�� ��ȯ�ϹǷ� �����̴� ������ Min Value�� 0.001�� ������ ��� �Ǵ� �ڵ忡 ó���������
    */

    public void SetMasterVolume(float volume)
    {
        m_AudioMixer.SetFloat("MASTER", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        m_AudioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    public void SetEffectVolume(float volume)
    {
        m_AudioMixer.SetFloat("EFFECT", Mathf.Log10(volume) * 20);
    }
}