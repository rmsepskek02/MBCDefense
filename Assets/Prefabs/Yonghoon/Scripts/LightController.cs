using UnityEngine;

public class LightController : MonoBehaviour
{
    private Light[] lights; // �ڽĵ鿡�� �޸� Light ������Ʈ�� �޾ƿ� ����
    public bool isOn = false; // ������ ���� ����
    private float[] originalIntensities; // �� ����Ʈ�� �ʱ� ��� ����
    private bool isUpdating = false; // ��� ������Ʈ ����
    [SerializeField] private float timerControl = 5f;

    private void Start()
    {
        // ��� �ڽ� ����Ʈ ��������
        lights = GetComponentsInChildren<Light>();

        // �ʱ� ��⸦ ������ �迭 ����
        originalIntensities = new float[lights.Length];

        for (int i = 0; i < lights.Length; i++)
        {
            originalIntensities[i] = lights[i].intensity; // �ʱ� ��⸦ ����
            lights[i].intensity = 0f; // ���� �� ���� ���·� ����
        }
    }

    public void SetLampState(bool turnOn)
    {
        isOn = turnOn;
        isUpdating = true; // ���� ���� �� ������Ʈ Ȱ��ȭ
    }

    private void Update()
    {
        if (!isUpdating) return; // ������Ʈ�� �ʿ����� ������ ����

        bool allLightsUpdated = true;

        for (int i = 0; i < lights.Length; i++)
        {
            float targetIntensity = isOn ? originalIntensities[i] : 0f; // ���� ���� ���� ���, ���� ���� 0
            float currentIntensity = lights[i].intensity;

            // ��⸦ ���������� ����
            lights[i].intensity = Mathf.MoveTowards(currentIntensity, targetIntensity, Time.deltaTime * timerControl);

            // ��ǥ ��⿡ �������� �ʾ����� ��� ������Ʈ
            if (!Mathf.Approximately(lights[i].intensity, targetIntensity))
            {
                allLightsUpdated = false;
            }
        }

        // ��� ����Ʈ�� ��ǥ ��⿡ �����ϸ� ������Ʈ ����
        if (allLightsUpdated)
        {
            isUpdating = false;
        }
    }
}
