using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputFieldController : MonoBehaviour
{

    public static InputFieldController Instance;
    public TMP_InputField xInputField;
    public TMP_InputField yInputField;
    [SerializeField] private Button startPathButton;
    public CharacterController currentChar;
    [SerializeField] private GridManger grid;
    public TMP_InputField auxiliarTest;

    public int xCoordinate { get; set; } = 0;
    public int yCoordinate { get; set; } = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        startPathButton.onClick.AddListener(ButtonAction);
        startPathButton.onClick.AddListener(InsertCoordinates);

        xInputField.onEndEdit.AddListener(InsertCoordinates);
        yInputField.onEndEdit.AddListener(InsertCoordinates);
    }

    private void InsertCoordinates()
    {
        xCoordinate = (xInputField.text.Equals("")) ? 0 : int.Parse(xInputField.text);
        yCoordinate = (yInputField.text.Equals("")) ? 0 : int.Parse(yInputField.text);
    }

    private void InsertCoordinates(string valueNull)
    {
        xCoordinate = (xInputField.text.Equals("")) ? 0 : int.Parse(xInputField.text);
        yCoordinate = (yInputField.text.Equals("")) ? 0 : int.Parse(yInputField.text);
    }

    private void ButtonAction()
    {
        switch (AnswerManager.Instance.questionList[AnswerManager.Instance.indexQuestion])
        {
            case TypeAnswer.Arrive:
                StartPath();
                break;

            case TypeAnswer.QuestionTest:
                SendAnswer();
                break;
        }
    }

    public void StartPath()
    {
        currentChar.targetX = XLimitTargets(xCoordinate);
        currentChar.targetY = YLimitTargets(yCoordinate);
        currentChar.StartCoroutine(currentChar.Move());
    //    ActiveButtons(false);
    }

    private void SendAnswer()
    {
        AnswerManager.Instance.SolveEquation(auxiliarTest.text);
        auxiliarTest.text = "";
    }

    public void ActiveButtons(bool enabled)
    {
        startPathButton.gameObject.SetActive(enabled);
        xInputField.gameObject.SetActive(enabled);
        yInputField.gameObject.SetActive(enabled);
    }

    public void ActiveInputsfields(bool enabled)
    {
        xInputField.gameObject.SetActive(enabled);
        yInputField.gameObject.SetActive(enabled);
    }

    public int XLimitTargets(int coordinate)
    {
        if (coordinate <= currentChar.limits.lowerX + (int)grid.transform.position.x)
        { return currentChar.limits.lowerX + (int)grid.transform.position.x; ;}

        if (coordinate >= currentChar.limits.upperX + (int)grid.transform.position.x)
        {return currentChar.limits.upperX + (int)grid.transform.position.x;}

        return coordinate+ (int)grid.transform.position.x;
    }

    public int YLimitTargets(int coordinate)
    {
        if (coordinate<= currentChar.limits.lowerY + (int)grid.transform.position.y)
        { return currentChar.limits.lowerY + (int)grid.transform.position.y;}

        if (coordinate >= currentChar.limits.upperY+(int)grid.transform.position.y)
        {return currentChar.limits.upperY + (int)grid.transform.position.y ;}

        return coordinate + (int)grid.transform.position.y ;
    }

}
