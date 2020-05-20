using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFx;
    [SerializeField] Transform instParent;
    [SerializeField] int scoreValue = 15;
    [SerializeField] int health = 10;

    Scoreboard scoreboard;

    void Start()
    {
        scoreboard = FindObjectOfType<Scoreboard>();
        Collider collider = gameObject.AddComponent<BoxCollider>();
        collider.isTrigger = false;
    }

    void OnParticleCollision(GameObject other) {
        scoreboard.ScoreHit(scoreValue);
        health--;
        if(health < 1) {
            GameObject instFx = Instantiate(deathFx, transform.position, Quaternion.identity);
            instFx.transform.parent = instParent;
            Destroy(gameObject);
        }
    }
}
