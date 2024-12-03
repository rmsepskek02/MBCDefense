using Defend.Tower;
using Defend.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildMenu : MonoBehaviour
{
    #region Variables
    private BuildManager buildManager;
    //�⺻ Ÿ�� ������
    public TowerInfo cannonTower;
    //���� UI Ȱ��ȭ /��Ȱ��ȭ
    public GameObject buildUI;

    //public InputActionProperty showButton;

    //private bool IsButton;
    public Tile tile;
    #endregion

    private void Start()
    {
        //�ʱ�ȭ
        buildManager = BuildManager.Instance;
    }


    //�⺻ �ͷ� ��ư�� Ŭ���� ȣ��
    public void SelectCannonTower()
    {
        //Debug.Log("�⺻ �ͷ��� ���� �Ͽ����ϴ�");
        //��ġ�� �ͷ��� �⺻ �ͷ�(������)�� ����
        buildManager.SetTowerToBuild(cannonTower);
        Debug.Log("�����");
        //tile.BuildTower();
    }
}
