using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class TutorialAssistant : MonoBehaviour
{
    [SerializeField] private float delayCompleted = 0.6f; 
    [SerializeField] private TutorialController control;
    [SerializeField] private DoAnimationController animatorControl;
    public TypeAnimation animationType;
    [SerializeField] private TypingAnimator typingObject;
    [SerializeField] private TMP_InputField fieldTextMision;
    [SerializeField] private UnityEngine.UI.Button buttonMision;
    [SerializeField] private Transform targetTransform;
    [TextArea(1,3)]
    [SerializeField] private string textToShow;
    [SerializeField] private string textEquivalent;
    [SerializeField] private bool misionCompleted;
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private Vector3 targetScale;

    public UnityEvent OnCompletedMission;

    public TypeMisionTutorial misionTuto;

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
          //  MisionFinished();
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
        control.NextMision();
        gameObject.SetActive(false);
    }
}
