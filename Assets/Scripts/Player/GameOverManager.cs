using UnityEngine;
using System.Collections;

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
        StartCoroutine(ShowGameOverAfterDelay());
    }

    private IEnumerator ShowGameOverAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);

        gameUIManager.ShowGameOver();
        gameUIManager.ShowPauseMenu();

        Time.timeScale = 0f;
        CharacterMovement.Instance.SetMovement(false);
    }

    private void OnDestroy()
    {
        healthManager.OnDeath -= HandleGameOver;
    }
}
