using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    private int height = 10;
    private int width = 10; 
    private float GridSize = 5f;

    private GameObject[,] gameGrid;
    [SerializeField] private GameObject gridCellPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateGrid());
    }

    private IEnumerator CreateGrid()
    {
        gameGrid = new GameObject[height, width];
        
        if (gridCellPrefab == null)
        {
            Debug.LogWarning("ERROR: Grid cell is null");
            yield return null;
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Create a new GridSpace object for each cell
                gameGrid[x, y] = Instantiate(gridCellPrefab, new Vector3(x * GridSize, y * GridSize),
                    Quaternion.identity);
                gameGrid[x, y].GetComponent<GridCell>().SetPosition(x, y);
                gameGrid[x,y].transform.parent = transform;
                gameGrid[x, y].gameObject.name = "Grid Space ( X: " + x + ", Y: " + y + ")";

                yield return new WaitForSeconds(0.025f);
            }
        }
    }
    
    // Gets the grid position from world position
    Vector2Int GetGridPositionFromWorld(Vector3 worldPos) 
    {
        int x = Mathf.FloorToInt(worldPos.x / GridSize);
        int y = Mathf.FloorToInt(worldPos.z / GridSize);

        x = Mathf.Clamp(x, 0, width);
        y = Mathf.Clamp(x, 0, height);

        return new Vector2Int(x, y); 
    }
    
    // Gets the world positon from grid 
    public Vector3 GetWorldPosFromGridPos(Vector2Int gridPos)
    {
        float x = gridPos.x * GridSize;
        float y = gridPos.y * GridSize;

        return new Vector3(x, 0, y);
    }
}
