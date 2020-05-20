using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] Tower tower;
    [SerializeField] int towerLimit;
    [SerializeField] Transform towerParent;

    Queue<Tower> towerQueue = new Queue<Tower>();
    public void AddTower(Block block) {
        if(towerQueue.Count < towerLimit) {
            Tower newTower = Instantiate(tower, block.transform.position, Quaternion.identity);
            block.isOccupiedByTower = true;
            newTower.block = block;
            newTower.transform.parent = towerParent;
            towerQueue.Enqueue(newTower);
        } else {
            Tower moveTower = towerQueue.Dequeue();
            moveTower.transform.position = block.transform.position;
            moveTower.block.isOccupiedByTower = false;
            block.isOccupiedByTower = true;            
            moveTower.block = block;
            towerQueue.Enqueue(moveTower);
        }
    }
}
