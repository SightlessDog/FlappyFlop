using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    private int posX; 
    private int posY; 
    
    // Saves a reference to the game object that gets placed on this cell
    public GameObject objectOnThisGridSpace = null;

    public bool isOccupied = false;

    public void SetPosition(int x, int y)
    {
        posX = x;
        posY = y;
    }
    
    // Get the position of this gridSpace on the grid
    public Vector2Int GetPosition()
    {
        return new Vector2Int(posX, posY);
    }
}
