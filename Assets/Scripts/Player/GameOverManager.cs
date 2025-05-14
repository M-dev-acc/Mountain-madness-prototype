using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    private HealthManager healthManager;
    private GameUIManager gameUIManager;

    private void Start()
    {
        healthManager = HealthManager.Instance;
        gameUIManager = GameUIManager.Instance;

        healthManager.OnDeath += HandleGameOver;
    }

    private void HandleGameOver()
    {
        new WaitForSeconds(3f);

        gameUIManager.ShowGameOver();
        gameUIManager.ShowPauseMenu();

        // Optionally: Pause the game
        Time.timeScale = 0f;
        CharacterMovement.Instance.SetMovement(false);
    }

    private void OnDestroy()
    {
        healthManager.OnDeath -= HandleGameOver;
    }
}
