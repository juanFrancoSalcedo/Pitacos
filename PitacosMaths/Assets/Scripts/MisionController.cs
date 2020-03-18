﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisionController : MonoBehaviour
{
    public TurnsManager turnsManager;
    [SerializeField] private GameObject mision;
    [SerializeField] private Transform gridPosition;
    public event System.Action<TypeEmotion,CharacterController> OnPlayerMisionFinished;

    [Header("~~~~~~~~~~ Animation Outputs ~~~~~~~~~~~~")]
    [SerializeField] private AnimationUIController popUpWin;
    [SerializeField] private AnimationTextController failText;
    
    private void OnEnable()
    {
        turnsManager.OnPlayerSelected += DistributeMision;
        turnsManager.player1.OnArrived += CompareArrive;
        turnsManager.player2.OnArrived += CompareArrive;
    }
    
    private void DistributeMision( CharacterController playerArg)
    {
        Vector3 misionPos = playerArg.transform.position ;

        misionPos.y = Random.Range(playerArg.limits.lowerY, playerArg.limits.upperY) + gridPosition.position.y;
        misionPos.x = Random.Range(playerArg.limits.lowerX, playerArg.limits.upperX) + gridPosition.position.x;
        mision.transform.position = misionPos;
    }

    private void CompareArrive(CharacterController playerPosit)
    {
        if (mision.transform.position == playerPosit.transform.position)
        {
            popUpWin.ActiveAnimation();
            OnPlayerMisionFinished?.Invoke(TypeEmotion.Happy,playerPosit);
            turnsManager.timer.initTime += 5;
        }
        else
        {
            failText.ActiveAnimation();
            OnPlayerMisionFinished?.Invoke(TypeEmotion.Sad, playerPosit);
            turnsManager.timer.initTime -= 5;
        }
    }
}

