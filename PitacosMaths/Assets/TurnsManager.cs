using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnsManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textTimer;
    public Timer timer;
    [SerializeField] private InputFieldController inputContol;
    public CharacterController player1;
    public  CharacterController player2;
    [SerializeField] private AnimationController shadowElement;

    public event System.Action<CharacterController> OnPlayerSelected;
    private float bufferTime;

    private void Start()
    {
        bufferTime = timer.initTime;
        inputContol.currentChar = player1;
        OnPlayerSelected?.Invoke(inputContol.currentChar);
    }

    private void OnEnable()
    {
        player1.OnArrived += SwitchPlayer;
        player2.OnArrived += SwitchPlayer;
        timer.OnCalculatedTimeString += ShowTime;
    }

    private void SwitchPlayer(CharacterController _character)
    {
        inputContol.currentChar.gameObject.SetActive(false);

        if (ReferenceEquals(inputContol.currentChar, player1))
        {
            inputContol.currentChar = player2;
        }
        else
        {
            inputContol.currentChar = player1;
        }

        HideHalfPosition();
        inputContol.currentChar.gameObject.SetActive(true);
        inputContol.xInputField.text = "0";
        inputContol.yInputField.text = "0";
        OnPlayerSelected?.Invoke(inputContol.currentChar);
    }

    private void ShowTime(string timerString, float timeArg)
    {
        textTimer.text = timerString;

        if (timeArg <0)
        {
            timer.stopTimer = true ;
        }
    }

    private void HideHalfPosition( )
    {
        Vector3 futurePos =   shadowElement.transform.position;
        futurePos.x = -futurePos.x;
        shadowElement.targetPosition = futurePos ;
        shadowElement.ActiveAnimation();
    }
}

