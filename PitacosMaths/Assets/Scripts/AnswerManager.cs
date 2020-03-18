using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerManager : MonoBehaviour
{
    public static AnswerManager Instance;
    public TypeAnswer answerType;
    private MisionController misionController;
    public float correctAnswer;


    [Header("~~~~~~~ UI Elements ~~~~~~~")]
    [SerializeField] private Canvas loseCanvas;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    private void Start()
    {
        TurnsManager.Instance.OnPlayerSelected += PoseChallenge;
    }

    private void PoseChallenge(CharacterController character)
    {
        AskEquation(character.transform.position);
    }

    public void Lose()
    {
        loseCanvas.enabled = true;
    }
    
    public void AskEquation(Vector3 charPos)
    {
        if (misionController == null)
        {
            misionController = GameObject.FindGameObjectWithTag("MisionController").GetComponent<MisionController>();
        }

        TMPro.TextMeshProUGUI textQuestion = GameObject.FindGameObjectWithTag("QuestionPopUp").transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>(); ;
        textQuestion.transform.parent.GetComponent<AnimationUIController>().ActiveAnimation();
        textQuestion.text = "¿Cuál es la distancia entre (" + misionController.mision.transform.position.x + ',' + misionController.mision.transform.position.y + ')'
             + " y (" + charPos.x+','+ charPos.y+") ?";


        float sum1 = (float)(misionController.mision.transform.position.x+ charPos.x);
        float sum2 = (float)(misionController.mision.transform.position.y+ charPos.y);

        float squareResult = Mathf.Pow(sum1, 2) + Mathf.Pow(sum2,2);

        correctAnswer = Mathf.Sqrt(squareResult);
        string corregido = correctAnswer.ToString("0.0");
        correctAnswer = float.Parse(corregido);
    }



    public void SolveEquation( string result)
    {
        float resultFloat = float.Parse(result);

        if (resultFloat == correctAnswer)
        {
            print("Bien my pez");
        }
        else
        {

            print("Mal Mi Pez");
        }
    }
}

public enum TypeAnswer
{
    None,
    Arrive,
    QuestionTest
}
