using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using MyBox;

public class TutorialAssistant : MonoBehaviour
{
    [SerializeField] private bool forSkip = false;
    [SerializeField] private float delayCompleted = 0.6f; 
    public TutorialController control;
    [SerializeField] private bool misionCompleted;
    

    [Separator]
    public TypeMisionTutorial misionTuto;
    
    [ConditionalField(nameof(misionTuto), false,TypeMisionTutorial.animationCompleted)] [SerializeField] private DoAnimationController animatorControl;
    [ConditionalField(nameof(misionTuto), false, TypeMisionTutorial.animationCompleted)] public TypeAnimation animationType;
    [ConditionalField(nameof(misionTuto), false, TypeMisionTutorial.animationCompleted)] [SerializeField] private bool overrideTimeAnimation;
    [ConditionalField(nameof(overrideTimeAnimation))] [SerializeField]  private float timeAnimation;
    [ConditionalField(nameof(misionTuto), false, TypeMisionTutorial.animationCompleted)] [SerializeField] private Transform targetTransform;
    [ConditionalField(nameof(misionTuto), false, TypeMisionTutorial.animationCompleted)] [SerializeField] private Vector3 targetPos;
    [ConditionalField(nameof(misionTuto), false, TypeMisionTutorial.animationCompleted)] [SerializeField] private Vector3 targetScale;
    [ConditionalField(nameof(misionTuto), false, TypeMisionTutorial.textCompleted)] [SerializeField] private TypingAnimator typingObject;
    [ConditionalField(nameof(misionTuto), false, TypeMisionTutorial.textCompleted)] [TextArea(1, 3)] [SerializeField] private string textToShow;
    [ConditionalField(nameof(misionTuto), false, TypeMisionTutorial.textEquals)] [SerializeField] private TMP_InputField fieldTextMision;
    [ConditionalField(nameof(misionTuto), false, TypeMisionTutorial.textEquals)] [SerializeField] private string textEquivalent;
    [ConditionalField(nameof(misionTuto), false, TypeMisionTutorial.buttonClicked)] [SerializeField] private UnityEngine.UI.Button buttonMision;
    

    public UnityEvent OnCompletedMission;


    public enum TypeMisionTutorial
    {
        none,
        textEquals,
        textCompleted,
        animationCompleted,
        buttonClicked
    }

    private void OnEnable()
    {
        switch (misionTuto)
        {
            case TypeMisionTutorial.animationCompleted:
                animatorControl.animationType = this.animationType;
                animatorControl.targetPosition = targetPos;
                animatorControl.worldPoint = targetTransform;
                animatorControl.targetScale = targetScale;
                animatorControl.OnCompleted += MisionFinished;

                if (overrideTimeAnimation)
                {
                    animatorControl.timeAnimation = timeAnimation;
                }

                break;

            case TypeMisionTutorial.textCompleted:
                 typingObject.OnComplitedText += MisionFinished;
                 typingObject.textCompo.text = textToShow;
                 typingObject.EraseAndSaveText();
                break;

            case TypeMisionTutorial.buttonClicked:
                buttonMision.onClick.AddListener(MisionFinished);
                break;
        }
    }

    private void Update()
    {
        switch (misionTuto)
        {
            case TypeMisionTutorial.textEquals:

                if (fieldTextMision.text.Equals(textEquivalent) && !misionCompleted)
                {
                    MisionFinished();
                    misionCompleted = true;
                }
                break;
        }
    }

    public void StartMision()
    {
        if (misionCompleted)
        {
           MisionFinished();
        }
        else
        {
            switch (misionTuto)
            {
                case TypeMisionTutorial.animationCompleted:
                    animatorControl.ActiveAnimation();
                    break;

                case TypeMisionTutorial.textCompleted:
                    typingObject.StartAnimation();
                    break;
            }
        }
    }

    public void MisionFinished()
    {
        Invoke("SendReport",delayCompleted);

        switch (misionTuto)
        {
            case TypeMisionTutorial.animationCompleted:
                animatorControl.OnCompleted -= MisionFinished;
                break;

            case TypeMisionTutorial.textCompleted:
                typingObject.OnComplitedText -= MisionFinished;
                break;

            case TypeMisionTutorial.buttonClicked:
                buttonMision.onClick.RemoveListener(MisionFinished);
                break;
        }
    }

    public void SendReport()
    {
        OnCompletedMission?.Invoke();

        if (forSkip)
        {
            control.SkipMission();
        }
        else
        {
            control.NextMision();
        }
        gameObject.SetActive(false);
    }
}
