using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    GameGrid gameGrid;
    [SerializeField] private LayerMask gridLayer;
    
    // Start is called before the first frame update
    void Start()
    {
        gameGrid = FindObjectOfType<GameGrid>();
    }

    // Update is called once per frame
    void Update()
    {
        GridCell cellMouseIsOver = isMouseOverAGridSpace();

        if (cellMouseIsOver != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                cellMouseIsOver.GetComponent<MeshRenderer>().material.color = Color.blue;
            } 
        }
    }
    
    // Returns the grid cell if the mouse is over
    GridCell isMouseOverAGridSpace()
    {
        // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //
        // if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, gridLayer))
        // {
        //     return hitInfo.transform.GetComponent<GridCell>();
        // }

        return null;
    }
}
