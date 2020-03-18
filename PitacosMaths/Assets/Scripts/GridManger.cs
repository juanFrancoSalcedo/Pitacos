using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManger : MonoBehaviour
{


    //TODO singleton this;

    [SerializeField] private Quad prefabTile;
    Quad[,] quads;
    [SerializeField]  private int columns = 11;
    [SerializeField]  private int rows = 10;

    private int colOffset;
    private int rowOffset;

    void Start()
    {
        colOffset = -(columns / 2);
        rowOffset = -(rows / 2);

        quads = new Quad[columns, rows];

        for (int col = 0; col < columns; col++)
        {
            for (int row = 0; row < rows; row++)
            {
                quads[col, row] = Instantiate(prefabTile, new Vector2(transform.position.x + col + colOffset, transform.position.y+ row + rowOffset),Quaternion.identity,transform);
                quads[col, row].xPos = (int)(quads[col, row].transform.position.x + transform.position.x);
                quads[col, row].yPos = (int)(quads[col, row].transform.position.y + transform.position.y);
            }
        }
    }
}
