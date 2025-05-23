using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance { get; private set; }

    [Header("UI Screens")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject startGameUI;
    [SerializeField] private GameObject hud;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        HideAllScreens();
    }

    private void HideAllScreens()
    {
        gameOverScreen?.SetActive(false);
        pauseMenu?.SetActive(false);
        hud?.SetActive(true);
    }

    public void ShowGameOver()
    {
        gameOverScreen?.SetActive(true);
        hud?.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ShowPauseMenu()
    {
        pauseMenu?.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HidePauseMenu()
    {
        pauseMenu?.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ShowStartGameUI()
    {
        if (gameOverScreen != null)
            gameOverScreen.SetActive(true);

        if (startGameUI != null)
            startGameUI.SetActive(true);

        Time.timeScale = 0f;
    }

    public void HideStartGameUI()
    {
        startGameUI?.SetActive(false);
    }

    public void ShowHUD()
    {
        hud?.SetActive(true);
    }
}
