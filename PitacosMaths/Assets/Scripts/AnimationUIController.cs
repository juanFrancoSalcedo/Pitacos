﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class AnimationUIController : MonoBehaviour
{
    private RectTransform rectTransform;
    public Vector3 targetPosition;
    public Vector3 targetScale;
    private Vector3 originScale;
    private Vector3 originPosition;
    public float timeAnimation;
    public float delay;
    public float coldTime;
    public Ease animationCurve;
    public bool playOnAwake;
    public TypeAnimation animationType;


    public UnityEvent OnCompletedCallBack;
    public event System.Action OnCompleted;
    

    private void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        originPosition = rectTransform.anchoredPosition;
        originScale = rectTransform.localScale;
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
                rectTransform.DOAnchorPos(targetPosition, timeAnimation, false).SetEase(animationCurve).SetDelay(delay).OnComplete(CallBacks);
                break;

            case TypeAnimation.MoveBack:
                rectTransform.DOAnchorPos(originPosition, timeAnimation,false).SetEase(animationCurve).SetDelay(delay).OnComplete(CallBacks);
                break;

            case TypeAnimation.MoveReturnOrigin:
                sequence.Append(rectTransform.DOAnchorPos(targetPosition, timeAnimation,false).SetEase(animationCurve).SetDelay(delay));
                sequence.AppendInterval(coldTime);
                sequence.Append(rectTransform.DOAnchorPos(originPosition,timeAnimation,false).SetEase(animationCurve));
                break;

            case TypeAnimation.Scale:
                rectTransform.DOScale(targetScale, timeAnimation).SetEase(animationCurve).SetDelay(delay);
                break;


            case TypeAnimation.ScaleReturnOriginScale:
                sequence.Append(rectTransform.DOScale(targetScale, timeAnimation).SetEase(animationCurve).SetDelay(delay));
                sequence.AppendInterval(coldTime);
                sequence.Append(rectTransform.DOScale(originScale, timeAnimation).SetEase(animationCurve));
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

public enum TypeAnimation
{
    Move,
    MoveReturnOrigin,
    MoveFadeOut,
    MoveBack,
    Scale,
    ScaleReturnOriginScale
}
