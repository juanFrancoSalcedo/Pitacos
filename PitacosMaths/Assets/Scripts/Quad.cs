using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quad : MonoBehaviour
{
    [SerializeField] private bool showOnOver;

    public int xPos;
    public int yPos;
    private SpriteRenderer render;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (InputFieldController.Instance.currentChar.transform.position == transform.position && showOnOver)
        {
            render.color = Color.white;
        }
    }
}
