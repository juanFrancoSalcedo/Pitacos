using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class ButtonSound : MonoBehaviour
{
    Button butto;
    AudioSource audioSour;
    public GameObject shot;

    void Start()
    {
        audioSour = GetComponent<AudioSource>();
        Destroy(audioSour);
        butto = GetComponent<Button>();
        butto.onClick.AddListener(PlaySound);
    }

    private void PlaySound()
    {
        Instantiate(shot);
    }
}
