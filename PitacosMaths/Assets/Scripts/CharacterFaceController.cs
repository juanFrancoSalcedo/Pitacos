using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterFaceController : MonoBehaviour
{
    public Emotion myEmotions;
    [SerializeField] private MisionController misionController;
    [SerializeField] private CharacterController characterOwner;
    private Image image;
    private Animator animationControl;

    void Start()
    {
        animationControl = GetComponent<Animator>();
        image = GetComponent<Image>();
        misionController.OnPlayerMisionFinished += ChangeEmotion;
    }

    public void ChangeEmotion(TypeEmotion emo, CharacterController character)
    {
        if (!ReferenceEquals(character, characterOwner))
        {
            return;
        }

        switch (emo)
        {
            case TypeEmotion.Basic:
                image.sprite = myEmotions.basicImage;
                break;

            case TypeEmotion.Sad:
                image.sprite = myEmotions.sadImage;
                animationControl.SetTrigger("Sad");
                Invoke("RestoreBasicImage", 1.5f);
                
                break;

            case TypeEmotion.Happy:
                image.sprite = myEmotions.happyImage;
                animationControl.SetTrigger("Happy");
                Invoke("RestoreBasicImage", 1.5f);

                break;
        }
    }

    private void RestoreBasicImage()
    {
        ChangeEmotion(TypeEmotion.Basic,characterOwner);
    }

}

[System.Serializable]
public struct Emotion
{
    public TypeEmotion emotionType;
    public Sprite basicImage;
    public Sprite sadImage;
    public Sprite happyImage;
}

public enum TypeEmotion
{
    Basic,
    Sad,
    Happy
}
