using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadSpecificScene(int indexScene)
    {
        SceneManager.LoadScene(indexScene);
    }

    public void Pause(Canvas canvasPause)
    {
        Time.timeScale = (Time.timeScale == 0) ? 1 : 0;
        canvasPause.enabled = (canvasPause.enabled) ? false : true; 
    }
}
