using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public bool isExplored = false;
    public Block exploredFrom;
    public bool isOccupiedByTower = false;

    Vector2Int gridPos;
    const int gridSize = 10;

    public bool isWalkable() {
        if(gameObject.transform.Find("Block_Friendly")) return true;
        if(gameObject.transform.Find("Block_Enemy")) return true;
        if(gameObject.transform.Find("Block_Neutral")) return false;
        Debug.Log("New type of block? Add to isWalkable check");
        return false; // wtf?
    }

    public int GetGridSize(){
        return gridSize;
    }

    public Vector2Int GetGridPos() {
        return new Vector2Int(Mathf.RoundToInt(transform.position.x/gridSize),
            Mathf.RoundToInt(transform.position.z/gridSize));
    }

    bool isPlaceable() {
        if(gameObject.transform.Find("Block_Friendly")) return false;
        if(gameObject.transform.Find("Block_Enemy")) return false;
        if(gameObject.transform.Find("Block_Neutral")) return true;
        Debug.Log("New type of block? Add to isPlaceable check");
        return false; // wtf?
    }

    void OnMouseOver() {
        if(Input.GetKey("mouse 0") && !isOccupiedByTower && isPlaceable()) {
            FindObjectOfType<TowerFactory>().AddTower(this);
        }
    }
}
