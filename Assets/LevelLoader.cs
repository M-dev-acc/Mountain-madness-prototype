using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour

public void LoadLevel (int SceneIndex)

{
    StartCoroutine(LoadSceneAsynchronously(SceneIndex));
}

IEnumerator LoadAsynchronously(int sceneIndex)
{
    AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

    while (!operation.isDone)
    {
        Debug.Log(operation.progress);

        yield return null;
    }
}
{
 
}
