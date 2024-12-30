using Defend.Enemy;
using UnityEngine;

public class GameClearUI : MonoBehaviour
{
    ListSpawnManager listSpawnManager;
    public GameObject clearUI;
    private bool isGameClear = false;
    void Start()
    {
        listSpawnManager = FindAnyObjectByType<ListSpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameClear)
            return;
        GameClear();
    }
    //����Ŭ����
    void GameClear()
    {

        if (ListSpawnManager.enemyAlive <= 0 && listSpawnManager.waveCount >= 4 && !isGameClear)
        {
            isGameClear = true;
            clearUI.SetActive(true);
        }
    }
}
