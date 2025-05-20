using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControlManager : MonoBehaviour
{
    private bool isGamePaused = false;

    void Start()
    {
        GameUIManager.Instance.ShowStartGameUI();
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // GameUIManager.Instance.HidePauseMenu();
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
