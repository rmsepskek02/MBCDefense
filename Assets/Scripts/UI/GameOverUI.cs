using Defend.Manager;
using Defend.TestScript;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    Health health;
    public GameObject castle;
    public GameObject gameoverUI;
    private bool isGameOver = false;
    void Start()
    {
        health = castle.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
            return;
        GameOverGo();
    }

    void GameOverGo()
    {
        if (health.CurrentHealth <= 0 && !isGameOver)
        {
            health.RgAmount = 0;
            isGameOver = true;
            gameoverUI.SetActive(true);
        }
    }
}
