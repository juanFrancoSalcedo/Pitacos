using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
   
    public AudioMixer mixer;
    public static AudioController Instance;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
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
}
