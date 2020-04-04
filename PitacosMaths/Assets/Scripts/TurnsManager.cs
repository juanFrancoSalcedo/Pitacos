using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnsManager : MonoBehaviour
{
    [Header("~~~~~~~~~ UI Elements~~~~~~~~")]
    [SerializeField] private TextMeshProUGUI textTimer;
    [SerializeField] private PanelAnswer derquisPanel;
    [SerializeField] private PanelAnswer feryiPanel;
    [Header("~~~~~~~~~~~~~~~~~~")]
    public Timer timer;
    [SerializeField] private InputFieldController inputContol;
    public CharacterController player1;
    public CharacterController player2;
    [SerializeField] private AnimationController shadowElement;

    public static TurnsManager Instance;
    public event System.Action<CharacterController> OnPlayerSelected;
    public int successes { get; set; }
    public int mistakes { get; set; }
    private float bufferTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

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
        timer.OnTimerZero += TimeFinished;
    }

    private void SwitchPlayer(CharacterController _character)
    {

        if (ReferenceEquals(inputContol.currentChar, player1))
        {
            inputContol.currentChar = player2;
        }
        else
        {
            inputContol.currentChar = player1;
        }

        HideHalfPosition();
        inputContol.xInputField.text = "0";
        inputContol.yInputField.text = "0";
        AnswerManager.Instance.SumQuestion();
        OnPlayerSelected?.Invoke(inputContol.currentChar);
    }

    private void ShowTime(string timerString, float timeArg)
    {
        textTimer.text = timerString;

        if (timeArg <=0)
        {
            timer.stopTimer = true ;
        }
    }

    public void ShowScore(bool isGoodAnswer)
    {
        if (ReferenceEquals(inputContol.currentChar, player1))
        {
            if (isGoodAnswer)
            {
                derquisPanel.SumCorrect();
                successes++;
            }
            else
            {
                derquisPanel.SumIncorrect();
                mistakes++;
            }
        }

        if (ReferenceEquals(inputContol.currentChar, player2))
        {
            if (isGoodAnswer)
            {
                feryiPanel.SumCorrect();
                successes++;
            }
            else
            {
                feryiPanel.SumIncorrect();
                mistakes++;
            }
        }
    }

    private void HideHalfPosition( )
    {
        Vector3 futurePos =   shadowElement.transform.position;
        futurePos.x = -futurePos.x;
        shadowElement.targetPosition = futurePos ;
        shadowElement.ActiveAnimation();
    }

    public void TimeFinished()
    {
        Time.timeScale = 0;
        AnswerManager.Instance.Lose();
    }
}

