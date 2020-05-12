using System.Collections;
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
    public GameObject shotObj;

    [Header("~~~~~Clips~~~~~~~")]

    public AudioClip winSound;
    public AudioClip loseSound;

    [Header("~~~~~~~UI States ~~~~~~")]

    [SerializeField] Button soundButton;
    [SerializeField] AnimationUIController animationSprite;
    [SerializeField] Sprite spriteSoundEnabled;
    [SerializeField] Sprite spriteSoundDisabled;

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

        Mute(DataSystem.LoadSoundState());
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
            DataSystem.SaveSoundState(true);
        }
        else
        {
            mixer.SetFloat("Master", -80);
            DataSystem.SaveSoundState(false);
        }
    }

    public void Mute(bool stateSound)
    {
        if (stateSound)
        {
            mixer.SetFloat("Master", 0);
            soundButton.GetComponent<Image>().sprite = spriteSoundEnabled;
            animationSprite.spriteToShift = spriteSoundDisabled;
        }
        else
        {
            mixer.SetFloat("Master", -80);
            soundButton.GetComponent<Image>().sprite = spriteSoundDisabled;
            animationSprite.spriteToShift = spriteSoundEnabled;
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

    //[ButtonMethod]
    private void SerachButtonsSounds()
    {
        Button[] but = FindObjectsOfType<Button>();

        foreach (Button bi in but)
        {
            if (bi.GetComponents<ButtonSound>().Length > 0)
            {
                DestroyImmediate(bi.GetComponents<ButtonSound>()[0]);
            }

            if (!bi.GetComponent<ButtonSound>())
            {
                bi.gameObject.AddComponent<ButtonSound>();
            }

            bi.GetComponent<ButtonSound>().shot = shotObj;

        }
    }
}
