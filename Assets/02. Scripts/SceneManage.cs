using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public void SceneLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadToRedHatScene()
    {
        SceneManager.LoadScene("RedHat");
        Time.timeScale = 1f;
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}
