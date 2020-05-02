﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using MyBox;

public class AudioController : MonoBehaviour
{
   
    public AudioMixer mixer;
    public static AudioController Instance;
    private AudioSource audioSourceComp;

    [Header("~~~~~Clips~~~~~~~")]

    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip buttonound;
    public AudioMixerGroup group;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSourceComp = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(this);
        }
    }

    public void PauseAudio()
    {
        if (Time.timeScale==1)
        {
            mixer.FindSnapshot("Paused").TransitionTo(0.01f);}
        else
        {
            mixer.FindSnapshot("Unpaused").TransitionTo(0.01f);
        }
    }

    public void TransitionAudio()
    {
        mixer.FindSnapshot("InTransition").TransitionTo(0.01f);
    }

    public void SetNormal()
    {
        mixer.FindSnapshot("Unpaused").TransitionTo(0.01f);
    }

    public void Mute()
    {
        float volume;

        mixer.GetFloat("Master",out volume);

        if (volume == -80)
        {
            mixer.SetFloat("Master", 0);
        }
        else
        {
            mixer.SetFloat("Master", -80);
        }
    }

    public void OnLosePlay(bool areYouLoser)
    {
        if (areYouLoser)
        {
            audioSourceComp.clip = loseSound;
            audioSourceComp.Stop();
            audioSourceComp.Play();
        }
        else
        {

            audioSourceComp.clip = winSound;
            audioSourceComp.Stop();
            audioSourceComp.Play();
        }
    }

    [ButtonMethod]
    private void SerachButtonsSounds()
    {
        Button[] but = FindObjectsOfType<Button>();

        foreach (Button bi in but)
        {
            bi.gameObject.AddComponent<ButtonSound>();
            bi.gameObject.GetComponent<AudioSource>().clip = buttonound;
            bi.gameObject.GetComponent<AudioSource>().outputAudioMixerGroup = group;
            bi.gameObject.GetComponent<AudioSource>().playOnAwake = false;
        }
    }
}
