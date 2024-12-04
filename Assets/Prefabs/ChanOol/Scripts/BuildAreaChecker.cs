using UnityEngine;

public class BuildAreaChecker : MonoBehaviour
{
    public Material shadow; // ���� ���� ǥ�� ���׸���
    public Material shadowRed; // ���� �Ұ� ǥ�� ���׸���
    [SerializeField] private bool isBuild; // ���� �����̸� true �Ұ��ϸ� false
    private Renderer objRenderer; // ���� �� ������Ʈ�� ������

    void Start()
    {
        objRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // Ÿ���� �����Ҽ�������
        if (isBuild == true)
        {
            CanBuildArea(); // ������ ���׸���
        }
        // Ÿ���� �����Ҽ�������
        else if (isBuild == false)
        {
            CannotBuildArea(); // ������ ���׸���
        }
    }

    public void CanBuildArea()
    {
        objRenderer.material = shadow; // ������ ���׸���
    }

    public void CannotBuildArea()
    {
        objRenderer.material = shadowRed; // ������ ���׸���
    }
}
