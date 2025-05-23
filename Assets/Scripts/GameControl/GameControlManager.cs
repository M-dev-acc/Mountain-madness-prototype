using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControlManager : MonoBehaviour
{
    private bool isGamePaused = false;
    public static bool gameRestarted = false;

    void Start()
    {
        if (!gameRestarted)
        {
            GameUIManager.Instance.ShowStartGameUI();
        }
        else
        {
            // Resume game normally after restart
            Time.timeScale = 1f;
            GameUIManager.Instance.HideStartGameUI();
            GameUIManager.Instance.ShowHUD();

            gameRestarted = false; // Reset flag
        }
    }

    private void Update()
    {
        // Toggle pause with Escape or Android back button
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void StartGame()
    {
        GameUIManager.Instance.HideStartGameUI();
        GameUIManager.Instance.ShowHUD();
        Time.timeScale = 1f;
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isGamePaused = true;
        GameUIManager.Instance.ShowPauseMenu();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
        GameUIManager.Instance.HidePauseMenu();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        gameRestarted = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
