using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterController : MonoBehaviour
{
    public Constraints limits;
    public SpriteRenderer characterSprite;
    public Sprite xSprite;
    public Sprite ySpriteUp;
    public Sprite ySpriteDown;

    public event Action<CharacterController> OnArrived;

    [SerializeField] private GridManger grid;

    public int originX= 0;
    public int originY = 0;

    public int targetX { get; set;}
    public int targetY { get; set;}

    void Start()
    {
        transform.position = new Vector3(grid.transform.position.x + originX, grid.transform.position.y +originY, transform.position.z);
    }

    public IEnumerator Move()
    {
        TurnsManager.Instance.timer.stopTimer = true;

        while (transform.position != new Vector3(targetX,targetY,0))
        {
            if (targetX != (int)transform.position.x)
            {
                int dif = (targetX > (int)transform.position.x) ? 1 : -1;

                characterSprite.sprite = xSprite;
                characterSprite.flipX = (dif > 0) ? false : true;
                transform.Translate(dif, 0, 0);
            }
            else if (targetY != (int)transform.position.y)
            {
                int dif = (targetY > (int)transform.position.y) ? 1 : -1;

                characterSprite.sprite = (dif >0) ? ySpriteUp: ySpriteDown;
                transform.Translate(0, dif, 0);
            }
            yield return new WaitForSeconds(0.3f);
        }
        OnArrived?.Invoke(this);
        TurnsManager.Instance.timer.stopTimer = false;
    }

    public void MoveSnaping()
    {
        transform.position = new Vector3(targetX, targetY, transform.position.z);
        OnArrived?.Invoke(this);
        //AnswerManager.Instance.SumQuestion();
    }
}

[System.Serializable]
public struct Constraints
{
    public int lowerX;
    public int upperX;
    public int lowerY;
    public int upperY;

}
