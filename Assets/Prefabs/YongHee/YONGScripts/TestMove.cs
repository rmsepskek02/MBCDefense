using Defend.Tower;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    #region Variables
    public Transform target;
    public TowerBase[] towers;
    #endregion
    void Start()
    {
        towers = FindObjectsByType<TowerBase>(FindObjectsSortMode.None);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            // �� TowerBase ������Ʈ�� ���� BuffTower ȣ��
            foreach (TowerBase tower in towers)
            {
                //tower.BuffTower();
            }
        }
    }
}
