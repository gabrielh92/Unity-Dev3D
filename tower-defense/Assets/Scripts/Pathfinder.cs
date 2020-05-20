using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Block startBlock, endBlock;

    Dictionary<Vector2Int, Block> grid = new Dictionary<Vector2Int, Block>();
    Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
    List<Block> path = new List<Block>();

    public List<Block> GetPath() {
        if(path.Count == 0) {
            InitGrid();
            BreadthFirstSearch();
        }
        return path;
    }

    void BreadthFirstSearch() {
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        queue.Enqueue(startBlock.GetGridPos());
        Vector2Int curr = new Vector2Int();
        while(queue.Count > 0) {
            curr = queue.Dequeue();
            if(curr == endBlock.GetGridPos()) {
                break;
            }
            grid[curr].isExplored = true;
            foreach (Vector2Int direction in directions) {
                Vector2Int neighbor = curr + direction;
                if(grid.ContainsKey(neighbor) && (!grid[neighbor].isExplored || queue.Contains(neighbor))) {
                    queue.Enqueue(neighbor);
                    grid[neighbor].exploredFrom = grid[curr];
                }
            }
        }

        // now we create the path
        Block currBlock = endBlock;
        while(currBlock != startBlock) {
            path.Add(currBlock);
            currBlock = currBlock.exploredFrom;
        }
        path.Add(startBlock);
        path.Reverse();
    }

    void InitGrid() {
        Block[] blocks = FindObjectsOfType<Block>();
        foreach (Block block in blocks) {
            if(grid.ContainsKey(block.GetGridPos())) {
                Debug.LogWarning("Block already exists " + block + ". Skip.");
            } else {
                if(block.isWalkable()) grid.Add(block.GetGridPos(), block);
            }
        }
    }
}
