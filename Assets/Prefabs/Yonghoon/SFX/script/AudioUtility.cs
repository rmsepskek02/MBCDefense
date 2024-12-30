using UnityEngine;
using UnityEngine.Audio;
using Defend.Audio;

namespace Defend.Utillity
{
    /// <summary>
    /// �� Ŭ������ ����� ���� ��ƿ��Ƽ ����� ����
    /// AudioManager�� AudioMixer�� ���� ����� �׷��� �����ϰ�, ������ �����ϰų�,
    /// Ư�� ��ġ���� ���� ȿ���� ����ϴ� ����� ����.
    /// </summary>
    public class AudioUtility
    {
        // AudioManager�� �ν��Ͻ��� �����ϴ� ���� ����.
        // AudioManager�� ��� ����� �ͽ� �� �׷� ���� ����� ����մϴ�.
        static AudioManager s_AudioManager;

        // ���� ������ ����� ����� �׷��� �����մϴ�.
        public enum AudioGroups
        {
            BGM,
            EFFECT,
            SKill,
            ObtainItem,
            BuffAndDebuff
        }

        // Ư�� ��ġ���� ���� ȿ��(AudioClip)�� �����ϰ� ����մϴ�.
        public static void CreateSFX(AudioClip clip, Vector3 position, AudioGroups audioGroup, float spatialBlend = 1f, float rolloffDistanceMin = 1f, float maxDistance = 15f)
        {
            // �� ������Ʈ ����: ���� ȿ���� ����� �ӽ� ������Ʈ�� ����ϴ�.
            GameObject impactSfxInstance = new GameObject("SFX_" + clip.name);
            // ������Ʈ ��ġ ���� : ȿ������ �߻��� ���� ����
            impactSfxInstance.transform.position = position;

            //Hierarchy / SFXContainer ������ ������Ʈ�� ����
            impactSfxInstance.transform.SetParent(GameObject.Find("SFXContainer").transform);

            // AudioSource ������Ʈ�� �߰��Ͽ� ���带 ����մϴ�.
            AudioSource source = impactSfxInstance.AddComponent<AudioSource>();
            // ����� Ŭ�� ����
            source.clip = clip;
            // 3D ���� ���� ���� ���� (0 �̸� 2D, 1 �̸� 3D)
            source.spatialBlend = spatialBlend;
            // �Ҹ� ���谡 ���۵Ǵ� �ּ� �Ÿ�
            source.minDistance = rolloffDistanceMin;
            source.maxDistance = maxDistance;

            // Custom Rolloff Curve ���� 
            // �� �̹� ������Ʈ������ �÷��̾� ��ġ�� maxDistance �̻� �־����� �Ǹ�
            // �Ҹ��� �ƿ� �鸮�� �ʵ��� ����
            AnimationCurve customRolloff = new AnimationCurve();
            customRolloff.AddKey(0, 1f);              // 0 �Ÿ����� ���� 100%
            customRolloff.AddKey(rolloffDistanceMin, 1f);  // MinDistance������ ����
            customRolloff.AddKey(maxDistance, 0f);    // MaxDistance���� ���� 0%

            source.rolloffMode = AudioRolloffMode.Custom;
            source.SetCustomCurve(AudioSourceCurveType.CustomRolloff, customRolloff);

            // ��� ����� �ͼ� �׷� ����
            source.outputAudioMixerGroup = GetAudioGroup(audioGroup);

            // ���� ��� ����
            source.Play();
            // ���� �ð��� ������ ������Ʈ�� �ڵ����� �����ϴ� ������Ʈ �߰�
            TimedSelfDestruct timedSelfDestruct = impactSfxInstance.AddComponent<TimedSelfDestruct>();
            // Ŭ�� ���̸�ŭ ���� �� ����
            timedSelfDestruct.LifeTime = clip.length;
        }

        // ����� �׷�(AudioMixerGroup)�� ��ȯ�մϴ�.
        public static AudioMixerGroup GetAudioGroup(AudioGroups group)
        {
            if (s_AudioManager == null)
                s_AudioManager = GameObject.FindAnyObjectByType<AudioManager>();

            // AudioManager�� ���� ������ �׷� �̸��� �ͼ� �׷��� ã���ϴ�.
            var groups = s_AudioManager.FindMatchingGroups(group.ToString());

            if (groups.Length > 0)
                return groups[0];

            Debug.LogWarning("Didn't find audio group for " + group.ToString());
            return null;
        }

        /*
            Mathf.Log10�� ����ϴ� ������ �����̴��� ���� ���� �α� �����Ϸ� ��ȯ�Ͽ� ���ú� ������ ���� ������ ���߰�, ����ڿ��� �� �ڿ������� ���� ��ȭ�� ����
            Mathf.Log10(flaot value)�Լ��� value�� 0�϶� -Infinity�� ��ȯ�ϹǷ� �����̴� ������ Min Value�� 0.001�� ������ ��� �Ǵ� �ڵ忡 ó���������
        */
        public static void SetMasterVolume(float value)
        {
            if (s_AudioManager == null)
                s_AudioManager = GameObject.FindAnyObjectByType<AudioManager>();

            if (value <= 0)
                value = 0.001f;
            float valueInDb = Mathf.Log10(value) * 20;

            s_AudioManager.SetFloat("MASTER", valueInDb);
        }

        public static float GetMasterVolume()
        {
            if (s_AudioManager == null)
                s_AudioManager = GameObject.FindAnyObjectByType<AudioManager>();

            s_AudioManager.GetFloat("MASTER", out var valueInDb);
            return Mathf.Pow(10f, valueInDb / 20.0f);
        }

        public static void SetBGMVolume(float value)
        {
            if (s_AudioManager == null)
                s_AudioManager = GameObject.FindAnyObjectByType<AudioManager>();

            if (value <= 0)
                value = 0.001f;
            float valueInDb = Mathf.Log10(value) * 20;

            s_AudioManager.SetFloat("BGM", valueInDb);
        }

        public static float GetBGMVolume()
        {
            if (s_AudioManager == null)
                s_AudioManager = GameObject.FindAnyObjectByType<AudioManager>();

            s_AudioManager.GetFloat("BGM", out var valueInDb);
            return Mathf.Pow(10f, valueInDb / 20.0f);
        }

        public static void SetSFXVolume(float value)
        {
            if (s_AudioManager == null)
                s_AudioManager = GameObject.FindAnyObjectByType<AudioManager>();

            if (value <= 0)
                value = 0.001f;
            float valueInDb = Mathf.Log10(value) * 20;

            s_AudioManager.SetFloat("EFFECT", valueInDb);
        }

        public static float GetSFXVolume()
        {
            if (s_AudioManager == null)
                s_AudioManager = GameObject.FindAnyObjectByType<AudioManager>();

            s_AudioManager.GetFloat("EFFECT", out var valueInDb);
            return Mathf.Pow(10f, valueInDb / 20.0f);
        }

        public static float GetVolume(string parameterName)
        {
            if (s_AudioManager == null)
                s_AudioManager = GameObject.FindAnyObjectByType<AudioManager>();

            s_AudioManager.GetFloat(parameterName, out var valueInDb);
            //Debug.Log($"{valueInDb} 1");
            //Debug.Log($"{Mathf.Pow(10f, valueInDb / 20.0f)} 2");
            return Mathf.Pow(10f, valueInDb / 20.0f);
        }


        public static void SetVolume(float value, string parameterName)
        {
            if (s_AudioManager == null)
                s_AudioManager = GameObject.FindAnyObjectByType<AudioManager>();

            if (value <= 0)
                value = 0.001f;

            float valueInDb = Mathf.Log10(value) * 20;

            s_AudioManager.SetFloat(parameterName, valueInDb);
        }
    }
}
