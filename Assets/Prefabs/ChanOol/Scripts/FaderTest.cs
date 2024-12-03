using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FaderTest : MonoBehaviour
{
    #region Variables
    private Image image; // Fader �̹���
    public AnimationCurve curve;
    #endregion

    private void Start()
    {
        // �ʱ�ȭ
        image = GetComponentInChildren<Image>();
    }

    // FadeOut (���� ��ο���)
    public void FadeTo()
    {
        StartCoroutine(FadeOut());
    }

    // FadeIn (���� �����)
    public void FromFade()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeOut()
    {
        // 1�� ���� Image Alpha�� 0 -> 1 (���� ��ο���)
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            image.color = new Color(0f, 0f, 0f, a);
            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        // 1�� ���� Image Alpha�� 1 -> 0 (���� �����)
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            image.color = new Color(0f, 0f, 0f, a);
            yield return null;
        }
    }
}