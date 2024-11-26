using Defend.Player;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    private PlayerState playerState;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            playerState = Object.FindAnyObjectByType<PlayerState>(); // PlayerState�� ã��
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // �ڿ� �߰�
    public void AddResources(int amount)
    {
       

        //���̸�
        playerState.AddRock(amount);
        
        //������
        playerState.AddTree(amount);

        //�����ڿ� ǥ��
        playerState.ShowStatus();
    }
}