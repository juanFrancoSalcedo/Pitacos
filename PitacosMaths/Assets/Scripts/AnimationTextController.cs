using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;

public class AnimationTextController : MonoBehaviour
{
    private RectTransform rectTransform;
    private TextMeshProUGUI textComponent;
    public Vector3 targetPosition;
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
        textComponent = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
        originPosition = rectTransform.anchoredPosition;
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
                rectTransform.DOMove(targetPosition, timeAnimation,false).SetEase(animationCurve).SetDelay(delay).OnComplete(CallBacks);
                break;

            case TypeAnimation.MoveReturnOrigin:
                sequence.Append(rectTransform.DOAnchorPos(targetPosition, timeAnimation,false).SetEase(animationCurve).SetDelay(delay));
                sequence.AppendInterval(coldTime);
                sequence.Append(rectTransform.DOAnchorPos(originPosition,timeAnimation,false).SetEase(animationCurve).OnComplete(CallBacks));
                break;

            case TypeAnimation.MoveFadeOut:

                sequence.Append(textComponent.DOFade(1,0).SetDelay(delay));
                sequence.Append(rectTransform.DOAnchorPos(targetPosition, timeAnimation, false).SetEase(animationCurve));
                sequence.AppendInterval(coldTime);
                sequence.Append(textComponent.DOFade(0, timeAnimation).SetEase(animationCurve).OnComplete(CallBacks));
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
