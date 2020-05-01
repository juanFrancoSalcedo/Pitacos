﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(AudioSource))]
public class ButtonSound : MonoBehaviour
{
    Button butto;
    AudioSource audioSour;

    void Start()
    {
        audioSour = GetComponent<AudioSource>();
        butto = GetComponent<Button>();
        butto.onClick.AddListener(PlaySound);
    }
    private void PlaySound()
    {
        audioSour.Play();
    }
}