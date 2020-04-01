using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelAnswer : MonoBehaviour
{
    private int incorrectCount = -1;
    private int correctCount =-1;


    [SerializeField] GameObject[] chulos;
    [SerializeField] GameObject[] equis;

    public void SumCorrect()
    {
        correctCount++;
        chulos[correctCount].SetActive(true);
    }

    public void SumIncorrect()
    {
        incorrectCount++;
        equis[incorrectCount].SetActive(true);
    }
}
