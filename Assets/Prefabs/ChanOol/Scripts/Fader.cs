using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fader : MonoBehaviour
{
    #region Variables
    [SerializeField]
    [Range(0.01f, 10f)]
    private float fadeTime; // Fade Speed ��

    [SerializeField] private AnimationCurve fadeCurve;  // ���̵� ȿ���� ����Ǵ� ���� ���� ��� ������ ����
    public Image image;                                 // Fader �̹��� (�������� �̹���)
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // �ʱ�ȭ
        image = GetComponentInChildren<Image>();

        // ����
        StartCoroutine(FadeOutIn());
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Fade Out/In ȿ�� �Ѽ�Ʈ �Լ�
    private IEnumerator FadeOutIn()
    {
        while (true)
        {
            yield return StartCoroutine(Fade(0, 1));    // FadeOut

            yield return StartCoroutine(Fade(1, 0));    // FadeIn

            break;
        }
    }

    // Fader(1, 0) : FadeIn
    // Fader(0, 1) : FadeOut

    private IEnumerator Fade(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            // fadeTime���� ����� fadeTime �ð�����
            // percent�� ���� 0���� 1�� ����
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            // ���İ��� Start���� End���� fadeTime �ð����� ��ȭ
            Color color = image.color;
            // color.a = Mathf.Lerp(start, end, percent);
            color.a = Mathf.Lerp(start, end, fadeCurve.Evaluate(percent));
            image.color = color;

            yield return null;
        }
    }
}
