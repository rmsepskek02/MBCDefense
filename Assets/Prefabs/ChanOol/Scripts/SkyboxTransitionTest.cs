using System.Collections;
using UnityEngine;

public class SkyboxTransitionTest : MonoBehaviour
{
    public Material skybox1; // ù ��° ��ī�̹ڽ�
    public Material skybox2; // �� ��° ��ī�̹ڽ�
    public float transitionSpeed = 1.0f; // ��ȯ �ӵ�
    public Material blendedSkybox;

    private float lerpValue = 0.0f;
    private bool isTransitioning = false;

    void Start()
    {
        // Blended Material�� RenderSettings.skybox�� ����
        RenderSettings.skybox = blendedSkybox;
        StartCoroutine(enumerator());
    }

    void Update()
    {
        if (isTransitioning)
        {
            // Lerp ����
            lerpValue += Time.deltaTime * transitionSpeed;
            blendedSkybox.SetFloat("_Blend", lerpValue);

            // �Ϸ�Ǹ� ����
            if (lerpValue >= 1.0f)
            {
                isTransitioning = false;
                lerpValue = 0.0f;

                // ���� ��ī�̹ڽ� ����
                RenderSettings.skybox = skybox2;
            }
        }
    }

    // ��ȯ ����
    public void StartTransition()
    {
        isTransitioning = true;
        RenderSettings.skybox = blendedSkybox; // Blended Material ����
        blendedSkybox.SetTexture("_Sky1", skybox1.GetTexture("_MainTex"));
        blendedSkybox.SetTexture("_Sky2", skybox2.GetTexture("_MainTex"));
    }

    IEnumerator enumerator()
    {
        //Debug.Log("Enumerator started");
        yield return new WaitForSeconds(3f);

        //Debug.Log("Transition started");
        StartTransition();
    }
}

