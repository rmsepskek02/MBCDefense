using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    #region Variables
    public Image image; // Fader �̹��� �ֱ�
    #endregion

    private void Start()
    {
        // �ʱ�ȭ
        // Fader�� �ڽ� ������Ʈ�� FaderImage�� ��������
        image = GetComponentInChildren<Image>();

        // ������ FaderImage �̹��� r,g,b,a �� �ʱ�ȭ
        image.color = new Color(0f, 0f, 0f, 0f);
    }

    private void Update()
    {
        // image.color
        Color color = image.color;

        // ���� ��(a)�� 1���� ������ ���� �� ����
        if (color.a < 1f)
        {
            // Time.deltaTime ��ŭ �����ϱ⶧���� 1���� 0���� 1�ʵ��� ����
            color.a += Time.deltaTime;
        }

        //�ٲ� ���� ������ image.color�� ����
        image.color = color;
    }

}

