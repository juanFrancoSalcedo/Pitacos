using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class AnimationController : MonoBehaviour
{
    public Vector3 targetPosition;
    private Vector3 originPosition;
    public float timeAnimation;
    public float delay;
    public float coldTime;
    public AnimationCurve animationCurve;
    public bool playOnAwake;
    public TypeAnimation animationType;

  
    public UnityEvent OnCompletedCallBack;
    public event System.Action OnCompleted;
    

    private void OnEnable()
    {
        originPosition = transform.position;
        OnCompleted += CallBacks;
        
        if (playOnAwake)
        {
            ActiveAnimation();
        }
    }
    
    public void ActiveAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        
        switch (animationType)
        {
            case TypeAnimation.Move:
                transform.DOMove(targetPosition, timeAnimation, false).SetEase(animationCurve).SetDelay(delay).OnComplete(CallBacks);
                break;

            case TypeAnimation.MoveReturnOrigin:

                sequence.Append(transform.DOMove(targetPosition, timeAnimation, false).SetEase(animationCurve).SetDelay(delay));
                sequence.AppendInterval(coldTime);
                sequence.Append(transform.DOMove(targetPosition, timeAnimation, false).SetEase(animationCurve));

                break;
        }
    }


    private void CallBacks()
    {
        OnCompletedCallBack?.Invoke();
    }
    

    public void SetColdTime(float _time)
    {
        coldTime = _time;
    }
}
