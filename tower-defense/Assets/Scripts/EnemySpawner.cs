using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Range(1f, 120f)][Tooltip("s")][SerializeField] float spawnDelay = 2f;
    [SerializeField] Enemy enemy;
    [SerializeField] Text scoreText;
    [SerializeField] AudioClip spawnSfx; 

    int enemyCount = 0;
    string textName;
    void Start()
    {
        textName = scoreText.text;
        scoreText.text = textName + enemyCount.ToString();
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn() {
        while(true) {
            GetComponent<AudioSource>().PlayOneShot(spawnSfx);
            var newEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
            newEnemy.transform.parent = gameObject.transform;
            enemyCount++;
            scoreText.text = textName + enemyCount.ToString();
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
