using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class AnimationUIController : MonoBehaviour
{
    private RectTransform rectTransform;
    private Image image;
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
    public UnityEvent OnStartedCallBack;
    public event System.Action OnCompleted;
    

    private void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
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
        OnStartedCallBack?.Invoke();
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

            case TypeAnimation.MoveFadeOut:

                sequence.Append(image.DOFade(1, 0).SetDelay(delay));
                sequence.Append(rectTransform.DOAnchorPos(targetPosition, timeAnimation, false).SetEase(animationCurve));
                sequence.AppendInterval(coldTime);
                sequence.Append(image.DOFade(0, timeAnimation).SetEase(animationCurve).OnComplete(CallBacks));
                break;

            case TypeAnimation.Scale:
                rectTransform.DOScale(targetScale, timeAnimation).SetEase(animationCurve).SetDelay(delay);
                break;


            case TypeAnimation.ScaleReturnOriginScale:
                sequence.Append(rectTransform.DOScale(targetScale, timeAnimation).SetEase(animationCurve).SetDelay(delay));
                sequence.AppendInterval(coldTime);
                sequence.Append(rectTransform.DOScale(originScale, timeAnimation).SetEase(animationCurve).OnComplete(CallBacks));
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
    ScaleReturnOriginScale,
    FadeOut
    //InfiniyLoopScaleReturnOriginScale
}
