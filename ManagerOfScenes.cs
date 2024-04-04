using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerOfScenes : MonoBehaviour
{
    public string sceneName;

    public void RestartScene()
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void ChangeGameScene()
    {
        SceneManager.LoadScene("Gameplay");
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
