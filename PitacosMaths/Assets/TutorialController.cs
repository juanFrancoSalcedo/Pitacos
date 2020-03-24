using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private AnimationUIController shadow;
    public TutorialAssistant[] misionAnimations;

    [SerializeField] private int misionIndex =-1;

    private void Start()
    {

        TurnsManager.Instance.timer.stopTimer = true;
        NextMision();
    }

    public void NextMision()
    {

        if (misionIndex + 1 >= misionAnimations.Length)
        {
            TurnsManager.Instance.timer.stopTimer = false;
            return;
        }

        misionIndex++;
        misionAnimations[misionIndex].gameObject.SetActive(true);
        misionAnimations[misionIndex].StartMision();
    }
}
