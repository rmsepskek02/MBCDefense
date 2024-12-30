using UnityEngine;
using Defend.Audio;

namespace Defend.Utillity
{
    /// <summary>
    /// �����(SFX[Effect]) ȿ�� ����� AudioUtility.cs -> AudioCreateSFX()�� ����
    /// �� ������Ʈ�� ����� AudioSource(Audio Clip)�� �ٿ��ְ� ���带 �����
    /// ������ ���̸�ŭ ����� �ϰ� ��������� �� ������Ʈ�� Destroy���ִ� ����
    /// LifeTime -> AudioClip.length �� ���� ������ �ʱ�ȭ��
    /// </summary>
    public class TimedSelfDestruct : MonoBehaviour
    {
        public float LifeTime = 1f;

        float m_SpawnTime;

        void Awake()
        {
            m_SpawnTime = Time.time;
        }

        void Update()
        {
            if (Time.time > m_SpawnTime + LifeTime)
            {
                Destroy(gameObject);
            }
        }
    }
}