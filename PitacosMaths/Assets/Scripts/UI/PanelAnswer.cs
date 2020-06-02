using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelAnswer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textScore;
    private int score = 0;
    public void SumCorrect()
    {
        score += 100;
        textScore.text =""+score;
    }

    public void SumIncorrect()
    {
        score -= 50;
        textScore.text = "" + score;
    }
}
