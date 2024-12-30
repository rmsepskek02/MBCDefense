using Defend.Manager;
using UnityEngine;
using UnityEngine.UI;

public class PreferencesUI : MonoBehaviour
{
    public Button saveButton;
    public Button loadButton;
    public Button newGameButton;
    public Button quitButton;
    public Button centerButton;
    public Button cheatButton;
    
    void Start()
    {
        // Save
        saveButton.onClick.AddListener(GameManager.Instance.SaveGameData);
        // Load
        loadButton.onClick.AddListener(GameManager.Instance.LoadGameData);
        // NewGame
        newGameButton.onClick.AddListener(GameManager.Instance.RestartGame);
        // Quit
        quitButton.onClick.AddListener(GameManager.Instance.QuitGame);
        // MoveCenter
        centerButton.onClick.AddListener(GameManager.Instance.PlayerTransformSenter);
        // Cheating
        cheatButton.onClick.AddListener(GameManager.Instance.Cheating);
    }
}
