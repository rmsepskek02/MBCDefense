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



    // FadeOut
    public void FadeTo()
    {
        // FadeOut ���� �� ���� �ʱ�ȭ
        // image.color = new Color(0f, 0f, 0f, 0f);
        StartCoroutine(FadeOut());
    }

    // FadeIn
    public void FromFade()
    {
        // FadeIn ���� �� ���� �ʱ�ȭ
        // image.color = new Color(0f, 0f, 0f, 1f);
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeOut()
    {
        // 1�� ���� image alpha 0 -> 1
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
        // 1�� ���� image alpha 1 -> 0
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