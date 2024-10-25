using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;

    public void Backward()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void PlayLevel(int levelIndex)
    {
        StartCoroutine(LoadSceneAsynchronously(levelIndex));   
    }

    IEnumerator LoadSceneAsynchronously (int levelIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            loadingBar.value = operation.progress;
            yield return null;
        }
    }
}
