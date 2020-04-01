using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypingAnimator : MonoBehaviour
{
    public TextMeshProUGUI textCompo { get; set; }
    [SerializeField] private float speedText= 0.4f;
    private string fullString;
    private string currentString;

    public event System.Action OnComplitedText;

    private void OnEnable()
    {
        textCompo = GetComponent<TextMeshProUGUI>();
    }

    public void EraseAndSaveText()
    {
        textCompo = GetComponent<TextMeshProUGUI>();
        fullString = textCompo.text;
        textCompo.text = "";
    }

    public void SaveText()
    {
        fullString = textCompo.text;
    }

    public void StartAnimation()
    {
        StartCoroutine(ShowPartial( ));
    }

    private IEnumerator ShowPartial()
    {
        textCompo = GetComponent<TextMeshProUGUI>();

        textCompo.text = "";

        for (int i = 0; i < fullString.Length; i++)
        {
            currentString = fullString.Substring(0,i);
            textCompo.text = currentString;
            yield return new WaitForSeconds(speedText);
        }

        textCompo.text = fullString;
        OnComplitedText?.Invoke();
    }



}
