using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;

public class AnimationTextController : DoAnimationController
{
    private RectTransform rectTransform;
    private TextMeshProUGUI textComponent;
    

    private new void OnEnable()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
        originPosition = rectTransform.anchoredPosition;

        base.OnEnable();
    }

    public override void ActiveAnimation()
    {
        OnStartedCallBack?.Invoke();
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

            case TypeAnimation.FadeOut:
                textComponent.DOFade(1,0);
                sequence.AppendInterval(coldTime);
                sequence.Append(textComponent.DOFade(0,timeAnimation).SetEase(animationCurve).SetDelay(delay).OnComplete(CallBacks));
                break;
        }
    }
    
}
