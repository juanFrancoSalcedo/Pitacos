using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputFieldController : MonoBehaviour
{
    public TMP_InputField xInputField;
    public TMP_InputField yInputField;
    [SerializeField] private Button startPathButton;
    public CharacterController currentChar;

    [SerializeField] private GridManger grid;

    void Start()
    {
        startPathButton.onClick.AddListener(StartPath);
        startPathButton.onClick.AddListener(InsertCoordinates);

        xInputField.onEndEdit.AddListener(InsertCoordinates);
        yInputField.onEndEdit.AddListener(InsertCoordinates);
    }

    private void InsertCoordinates()
    {
        int xCoordinate = 0;
        int yCoordinate = 0;

        xCoordinate = (xInputField.text.Equals("")) ? 0 : int.Parse(xInputField.text);
        yCoordinate = (yInputField.text.Equals("")) ? 0 : int.Parse(yInputField.text);
        
        currentChar.targetX = XLimitTargets(xCoordinate);
        currentChar.targetY = YLimitTargets(yCoordinate);
        
    }

    private void InsertCoordinates(string valueNull)
    {
        int xCoordinate = 0;
        int yCoordinate = 0;
        
        xCoordinate = (xInputField.text.Equals("")) ? 0 : int.Parse(xInputField.text);
        yCoordinate = (yInputField.text.Equals("")) ? 0 : int.Parse(yInputField.text);

        currentChar.targetX = XLimitTargets(xCoordinate);
        currentChar.targetY = YLimitTargets(yCoordinate);

    }

    public void StartPath()
    {
        currentChar.StartCoroutine(currentChar.Move());
    //    ActiveButtons(false);
    }

    //public void ActiveButtons(bool enabled)
    //{
    //    startPathButton.gameObject.SetActive(enabled);
    //    xInputField.gameObject.SetActive(enabled);
    //    yInputField.gameObject.SetActive(enabled);
    //}

    public int XLimitTargets(int coordinate)
    {
        if (coordinate <= currentChar.limits.lowerX + (int)grid.transform.position.x)
        {
            return currentChar.limits.lowerX + (int)grid.transform.position.x; ;
        }

        if (coordinate >= currentChar.limits.upperX + (int)grid.transform.position.x)
        {
            return currentChar.limits.upperX + (int)grid.transform.position.x;
        }
        return coordinate+ (int)grid.transform.position.x;
    }

    public int YLimitTargets(int coordinate)
    {
        if (coordinate<= currentChar.limits.lowerY + (int)grid.transform.position.y)
        {
            return currentChar.limits.lowerY + (int)grid.transform.position.y;
        }

        if (coordinate >= currentChar.limits.upperY+(int)grid.transform.position.y)
        {
            return currentChar.limits.upperY + (int)grid.transform.position.y ;
        }

        return coordinate + (int)grid.transform.position.y ;
    }

}
