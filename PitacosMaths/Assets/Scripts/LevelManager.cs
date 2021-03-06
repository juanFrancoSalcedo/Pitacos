﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textVelocity;

    public void Replay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadSavedSceneZoom()
    {
        Time.timeScale = 1;
        Transition.Instance.Zoom();
        StartCoroutine(WaitLoadScene(DataSystem.LoadLevel()));
        AudioController.Instance.TransitionAudio();
    }

    public void LoadSpecificSceneZoom(int indexScene)
    {
        Time.timeScale = 1;
        Transition.Instance.Zoom();
        StartCoroutine(WaitLoadScene(indexScene));
        AudioController.Instance.TransitionAudio();
    }

    private IEnumerator WaitLoadScene(int scene)
    {
        yield return new WaitForSeconds(1f);
        AudioController.Instance.SetNormal();
        LoadSpecificScene(scene);
    }

    public void LoadSpecificScene(int indexScene)
    {
        DataSystem.SaveLevel(indexScene);
        SceneManager.LoadScene(indexScene);   
    }

    public void Pause(Canvas canvasPause)
    {
        AudioController.Instance.PauseAudio();
        Time.timeScale = (Time.timeScale == 0) ? 1 : 0;
        canvasPause.enabled = (canvasPause.enabled) ? false : true;
    }

    public void SwitchSpeedsHigh()
    {
        Time.timeScale = (Time.timeScale == 1) ? 2 : 1;
        if (textVelocity != null)
        {
            textVelocity.text = (Time.timeScale == 1) ? "Velocidad x1" : "Velocidad x2";
        }
    }

    public void SetTimeScaleSpeed(int _speed)
    {
        Time.timeScale = _speed;
    }
}
