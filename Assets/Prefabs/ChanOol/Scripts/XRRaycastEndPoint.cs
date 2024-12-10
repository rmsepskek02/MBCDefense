using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class XRRaycastEndPoint : MonoBehaviour
{
    public XRRayInteractor rayInteractor;  // XR ��Ʈ�ѷ��� Ray Interactor
    private GameObject buildArea;  // �ڽ� ������Ʈ Build Area ������
    //private bool isBuildAreaActive = false;  // Build Area Ȱ�� ���� ĳ��
    private Renderer buildAreaRenderer; // Build Area�� Renderer ����

    void Start()
    {

        // �ڽ� ������Ʈ�� �̸����� ã�Ƽ� ����
        buildArea = transform.Find("BuildArea")?.gameObject;

    }

    void Update()
    {
        if (rayInteractor == null || buildArea == null) return;

        RaycastHit hit;
        bool isBuildAreaActive = rayInteractor.TryGetCurrent3DRaycastHit(out hit);

        // Hit �Ȱ� ������ isBuildAreaActive true
        if (isBuildAreaActive == true)
        {
            buildArea.SetActive(true);

            // Build Area ��ġ ������Ʈ
            buildArea.transform.position = hit.point;

            // ǥ���� ����� ������� x�� 90���� ����
            buildArea.transform.rotation = Quaternion.Euler(90f, 0f, 0f); // x�� 90�� ȸ���� ����
        }
        else
        {
            isBuildAreaActive = false;
            buildArea.SetActive(false);
        }
    }
}