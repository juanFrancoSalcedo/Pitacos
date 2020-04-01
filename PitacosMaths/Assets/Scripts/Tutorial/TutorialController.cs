using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public TutorialAssistant[] misionAnimations;
    public TutorialAssistant[] skipeAnimations;
    private int misionIndex =-1;
    private int skipeMisionIndex = -1;

    private void Start()
    {
        if (TurnsManager.Instance != null)
        {
            TurnsManager.Instance.timer.stopTimer = true;
        }

        NextMision();
    }

    public void NextMision()
    {
        if (misionIndex + 1 >= misionAnimations.Length)
        {
            if (TurnsManager.Instance != null)
            {
                TurnsManager.Instance.timer.stopTimer = false;
            }

            return;
        }

        misionIndex++;
        misionAnimations[misionIndex].gameObject.SetActive(true);
        misionAnimations[misionIndex].StartMision();
    }

    public void SkipMission()
    {
        misionIndex = misionAnimations.Length-1;
        NextMision();


        if (skipeMisionIndex + 1 >= skipeAnimations.Length)
        {
            return;
        }
        skipeMisionIndex++;
        skipeAnimations[skipeMisionIndex].gameObject.SetActive(true);
        skipeAnimations[skipeMisionIndex].StartMision();

    }
}
