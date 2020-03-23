using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class AnimationController : DoAnimationController
{
    private new void OnEnable()
    {
        originPosition = transform.position;

        base.OnEnable();
    }
    
    public override void ActiveAnimation()
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
}
