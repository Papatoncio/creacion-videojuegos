using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private void Awake()
    {
        if (LevelManager.instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void GameOver()
    {
        UIManager _ui = GetComponent<UIManager>();

        if (_ui != null)
        {
            _ui.ToggleDeathPanel();
            Time.timeScale = 0;
        }
    }

    public void WinGame()
    {
        UIManager _ui = GetComponent<UIManager>();

        if (_ui != null)
        {
            _ui.ToggleWinPanel();
            Time.timeScale = 0;
        }
    }

    public void Menu()
    {
        SceneManager.LoadSceneAsync(0);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
