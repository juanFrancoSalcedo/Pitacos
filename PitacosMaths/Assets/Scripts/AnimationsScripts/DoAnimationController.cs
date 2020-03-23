using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;


public abstract class DoAnimationController : MonoBehaviour
{
    public Vector3 targetPosition;
    protected Vector3 originPosition;
    public float timeAnimation;
    public float delay;
    public float coldTime;
    public Ease animationCurve;
    public bool playOnAwake;
    public TypeAnimation animationType;

    public UnityEvent OnCompletedCallBack;
    public UnityEvent OnStartedCallBack;
    public event System.Action OnCompleted;

    public abstract void ActiveAnimation();
   

    protected void OnEnable()
    {
        OnCompleted += CallBacks;

        if (playOnAwake)
        {
            ActiveAnimation();
        }

    }

    protected void CallBacks()
    {
         OnCompletedCallBack?.Invoke();
    }

    public void SetColdTime(float _time)
    {
        coldTime = _time;
    }


}
