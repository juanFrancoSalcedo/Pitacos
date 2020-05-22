﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class MisionController : MonoBehaviour
{
    public TurnsManager turnsManager;
    public GameObject mision;
    [SerializeField] private Transform gridPosition;
    public event System.Action<TypeEmotion, CharacterController> OnPlayerMisionFinished;
    [Tooltip("[0]Sum, [1]Pi")]
    public Sprite[] imagesMision;
    [Header("~~~~~~~~~~ Animation Outputs ~~~~~~~~~~~~")]
    public AnimationUIController popUpWin;
    public AnimationUIController failText;

    [ConditionalField(nameof(useSpecificArray), true)] public bool useSpecificPos;
    [ConditionalField(nameof(useSpecificPos))] public SpecificPos specificPos;
    [ConditionalField(nameof(useSpecificPos), true)] public bool useSpecificArray;
    public bool usePools;
    [ConditionalField(nameof(useSpecificArray),true)] [SerializeField] private List<SpecificPos> arraySpecificPos = new List<SpecificPos>();
    [ConditionalField(nameof(usePools), true)] [SerializeField] private List<SpecificPos> poolA = new List<SpecificPos>();
    [ConditionalField(nameof(usePools), true)] [SerializeField] private List<SpecificPos> poolB = new List<SpecificPos>();
    [ConditionalField(nameof(usePools), true)] [SerializeField] private List<SpecificPos> poolC = new List<SpecificPos>();
    int countArraySpecific =0;


    private void OnEnable()
    {
        turnsManager.OnPlayerSelected += DistributeMision;
        turnsManager.player1.OnArrived += CompareArrive;
        turnsManager.player2.OnArrived += CompareArrive;
        tag = "MisionController";
        if (usePools)
        {
            ChoosePool();
        }
    }

    private void DistributeMision(CharacterController playerArg)
    {
        if(AnswerManager.Instance.questionList.Count < AnswerManager.Instance.indexQuestion)
        {
            if (AnswerManager.Instance.questionList[AnswerManager.Instance.indexQuestion] == TypeAnswer.Arrive)
            {
                mision.GetComponent<SpriteRenderer>().sprite = imagesMision[1];
            }
            else
            {
                mision.GetComponent<SpriteRenderer>().sprite = imagesMision[0];
            }
        }

        if (useSpecificArray && countArraySpecific < arraySpecificPos.Count)
        {
            mision.transform.position = new Vector3(arraySpecificPos[countArraySpecific]._X, arraySpecificPos[countArraySpecific]._Y, playerArg.transform.position.z);
            countArraySpecific++;
            return;
        }

        if (useSpecificPos)
        {
            mision.transform.position = new Vector3(specificPos._X, specificPos._Y, playerArg.transform.position.z);
            useSpecificPos = false;
            return;
        }
        
        int count = 0;

        Vector3 misionPos = playerArg.transform.position;
        Vector3 bufPlayerPos = playerArg.transform.position;

        while (misionPos == playerArg.transform.position && misionPos.y == bufPlayerPos.y)
        {
            misionPos.y = Random.Range(playerArg.limits.lowerY, playerArg.limits.upperY) + gridPosition.position.y;
            misionPos.x = Random.Range(playerArg.limits.lowerX, playerArg.limits.upperX) + gridPosition.position.x;
            count++;
        }

        if (misionPos == playerArg.transform.position)
        {
            Debug.LogError("ESTE JUEGO VA EXPLOTAR 3...2...1... DATO De vital IMPORTANTCIA(" + count + ")");
        }

        mision.transform.position = misionPos;
    }

    private void ChoosePool()
    {
        int cre = Random.Range(1,4);
        arraySpecificPos.Clear();
        switch (cre)
        {
            case 1:
                arraySpecificPos = poolA;
                break;

            case 2:
                arraySpecificPos = poolB;
                break;

            case 3:
                arraySpecificPos = poolC;
                break;
        }
        useSpecificPos = false;
        useSpecificArray = true;
    }

    private void CompareArrive(CharacterController playerPosit)
    {
        if (mision.transform.position == playerPosit.transform.position)
        {
            popUpWin.ActiveAnimation();
            OnPlayerMisionFinished?.Invoke(TypeEmotion.Happy, playerPosit);
            turnsManager.timer.initTime += 5;
            turnsManager.ShowScore(true);
        }
        else
        {
            failText.ActiveAnimation();
            OnPlayerMisionFinished?.Invoke(TypeEmotion.Sad, playerPosit);
            turnsManager.timer.initTime -= 5;
            turnsManager.ShowScore(false);
        }
    }

    [System.Serializable]
    public struct SpecificPos
    {
        public int _X;
        public int _Y;
    }
}



