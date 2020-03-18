using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerManager : MonoBehaviour
{
    public static AnswerManager Instance;

    [Header("~~~~~~~ UI Elements ~~~~~~~")]
    [SerializeField] private Canvas loseCanvas;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    public void Lose()
    {
        loseCanvas.enabled = true;
    }
}
