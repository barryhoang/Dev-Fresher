using System;
using System.Collections;
using System.Collections.Generic;
using Maps;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapElement : MonoBehaviour
{
     public  GridMap gridMap;
    
    private void Awake()
    {
        //gridMap = transform.parent.GetComponent<GridMap>();
    }
    
    private void Update()
    {
        PlaceObjOnGrid();
    }

    private void PlaceObjOnGrid()
    {
        Transform t = transform;
        Vector3 pos = t.position;
        int xPos = (int) pos.x;
        int yPos = (int) pos.y;
        /*gridMap.SetHero(this, xPos, yPos); 
        gridMap.SetEnemy(this, xPos, yPos);*/
        
    }
}
