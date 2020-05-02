using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerManager : MonoBehaviour
{
    public static AnswerManager Instance;
    private MisionController misionController;
    [SerializeField] private float correctAnswer;

    public List<TypeAnswer> questionList = new List<TypeAnswer>();
    public int indexQuestion { get; set; } = 0;

    [SerializeField] private int limitMistakes =2;

    [Header("~~~~~~~ UI Elements ~~~~~~~")]
    [SerializeField] private Canvas loseCanvas;
    [SerializeField] private Canvas winCanvas;

    private TMPro.TextMeshProUGUI textQuestion;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        TurnsManager.Instance.OnPlayerSelected += PoseChallenge;

    }

    private void PoseChallenge(CharacterController character)
    {
        print("Cambio Posicion");

        if (limitMistakes <= TurnsManager.Instance.mistakes)
        {
            Invoke("Lose",2);
        }

        if (indexQuestion >= questionList.Count)
        {
            InputFieldController.Instance.ActiveButtons(false);
            TurnsManager.Instance.timer.stopTimer = true;
            Invoke("Win",2);
            return;
        }
        
        switch (questionList[indexQuestion])
        {
            case TypeAnswer.QuestionTest:
                print("Supocicion Input Fields");
                InputFieldController.Instance.ActiveInputsfields(false);
                AskEquation(character.transform.position);
                break;
            case TypeAnswer.Arrive:
                InputFieldController.Instance.ActiveInputsfields(true);
                SolveArrive();
                break;
        }
    }

    public void Lose()
    {
        loseCanvas.enabled = true;
        AudioController.Instance.OnLosePlay(true);
    }

    public void Win()
    {
          winCanvas.enabled = true;
        AudioController.Instance.OnLosePlay(false);
    }

    public void AskEquation(Vector3 charPos)
    {
        if (misionController == null)
        {
            misionController = GameObject.FindGameObjectWithTag("MisionController").GetComponent<MisionController>();
        }

        textQuestion = GameObject.FindGameObjectWithTag("QuestionPopUp").transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
        textQuestion.transform.parent.GetComponent<AnimationUIController>().animationType = TypeAnimation.Move;
        textQuestion.transform.parent.GetComponent<AnimationUIController>().ActiveAnimation();
        textQuestion.text = "¿Cuál es la distancia entre (" + misionController.mision.transform.position.x + ',' + misionController.mision.transform.position.y + ')'
             + " y (" + charPos.x+','+ charPos.y+") ?";
        
        float sum1 = (float)(charPos.x - misionController.mision.transform.position.x);
        float sum2 = (float)(charPos.y - misionController.mision.transform.position.y);
        float squareResult = Mathf.Pow(sum1, 2) + Mathf.Pow(sum2,2);
        correctAnswer = Mathf.Sqrt(squareResult);
        string corregido = correctAnswer.ToString("0.0");
        correctAnswer = float.Parse(corregido);
    }

    public void SolveEquation(string result)
    {
        
        float resultFloat = float.Parse(ChangeChar(result));
        if (resultFloat == correctAnswer)
        {
            InputFieldController.Instance.currentChar.targetX =(int)misionController.mision.transform.position.x;
            InputFieldController.Instance.currentChar.targetY = (int)misionController.mision.transform.position.y;
            InputFieldController.Instance.currentChar.StartCoroutine(InputFieldController.Instance.currentChar.Move());
            //InputFieldController.Instance.currentChar.MoveSnaping(); 
        }
        else
        {
            InputFieldController.Instance.currentChar.targetX =(int)InputFieldController.Instance.currentChar.transform.position.x;
            InputFieldController.Instance.currentChar.targetY =(int)InputFieldController.Instance.currentChar.transform.position.y;
            InputFieldController.Instance.currentChar.StartCoroutine(InputFieldController.Instance.currentChar.Move());
            //InputFieldController.Instance.currentChar.MoveSnaping(); 

        }

        if (indexQuestion >= questionList.Count)
        {
            textQuestion.transform.parent.GetComponent<AnimationUIController>().animationType = TypeAnimation.Scale;
            textQuestion.transform.parent.GetComponent<AnimationUIController>().targetScale = Vector3.zero;
            textQuestion.transform.parent.GetComponent<AnimationUIController>().ActiveAnimation();
            return;
        }

        if (questionList[indexQuestion] != TypeAnswer.QuestionTest)
        {
            textQuestion.transform.parent.GetComponent<AnimationUIController>().animationType = TypeAnimation.MoveBack;
            textQuestion.transform.parent.GetComponent<AnimationUIController>().ActiveAnimation();
        }
        else
        {
            textQuestion.transform.parent.GetComponent<AnimationUIController>().animationType = TypeAnimation.ScaleReturnOriginScale;
            textQuestion.transform.parent.GetComponent<AnimationUIController>().ActiveAnimation();
        }
    }

    public void SolveArrive()
    {
        if (textQuestion != null)
        {
            textQuestion.transform.parent.GetComponent<AnimationUIController>().animationType = TypeAnimation.MoveBack;
            textQuestion.transform.parent.GetComponent<AnimationUIController>().ActiveAnimation();
        }
    }

    private string ChangeChar(string str)
    {
        string newStr = str.Replace('.',',');

        return newStr;
    }

    public void SumQuestion()
    {
        indexQuestion++;
    }

}

public enum TypeAnswer
{
    None,
    Arrive,
    QuestionTest
}
