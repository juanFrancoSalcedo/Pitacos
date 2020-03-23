using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialAssistant : MonoBehaviour
{
    [SerializeField] private TutorialController control;
    //[SerializeField] private  
    [SerializeField] private TMP_InputField fieldTextMision;
    [SerializeField] private string textEquivalent;

    public TypeMisionTutorial misionTuto;

    public enum TypeMisionTutorial
    {
        text
    }
    
    private void Update()
    {

        switch (misionTuto)
        {

            case TypeMisionTutorial.text:

                if (fieldTextMision.text.Equals(textEquivalent))
                {
                    print("Listo");
                }
                break;
                
        }

    }
}
