using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private AnimationUIController shadow;
    public MisionTutorialAnswer[] misionAnimations;

    int misionIndex =0;

    public void NextMision()
    {
        misionIndex++;
        misionAnimations[misionIndex].animator.ActiveAnimation();
    }
    
}

[System.Serializable]
public struct MisionTutorialAnswer
{
    public DoAnimationController animator;
    public TypeAnimation animationType;
    public float delay;
    public Vector3 scale;
    public Vector3 position;
}
