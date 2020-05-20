using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Block))]
public class CubeEditor : MonoBehaviour
{
    Block block;
    void Awake() {
        block = GetComponent<Block>();
    }

    void Update()
    {
        SnapToGrid();
        //UpdateLabel();
    }

    void SnapToGrid() {
        transform.position = new Vector3(
            block.GetGridPos().x * block.GetGridSize(), 0f, block.GetGridPos().y * block.GetGridSize());
    }

    void UpdateLabel() {
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        string labelText = block.GetGridPos().x.ToString() + "," + block.GetGridPos().y.ToString();
        textMesh.text = labelText;
        gameObject.name = labelText;
    }
}
