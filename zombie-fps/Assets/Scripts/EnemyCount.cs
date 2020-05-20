using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI enemyCountText;

    string enemiesText = "zombies ";
    void Start()
    {
        enemyCountText.text = enemiesText + transform.childCount.ToString();
    }

    void Update()
    {
        int aliveEnemyCount = 0;
        foreach(Transform child in transform) {
            if(!child.GetComponent<EnemyHealth>().isDead) aliveEnemyCount++;
        }
        enemyCountText.text = enemiesText + aliveEnemyCount.ToString();       
    }
}
