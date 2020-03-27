using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void Replay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadSpecificSceneZoom(int indexScene)
    {
        Time.timeScale = 1;
        Transition.Instance.Zoom();
        StartCoroutine(WaitLoadScene(indexScene));
    }

    private IEnumerator WaitLoadScene(int scene)
    {
        yield return new WaitForSeconds(1f);
        LoadSpecificScene(scene);
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
