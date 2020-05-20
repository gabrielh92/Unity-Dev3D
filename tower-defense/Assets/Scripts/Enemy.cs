using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 10;
    [SerializeField] ParticleSystem hitParticleSystem;
    [SerializeField] ParticleSystem deathParticleSystem;
    [SerializeField] ParticleSystem goalParticleSystem;
    [SerializeField] float movementPeriod = 0.5f;
    [SerializeField] AudioClip hitSfx;

    void Start()
    {
        List<Block> path = FindObjectOfType<Pathfinder>().GetPath();
        StartCoroutine(MoveToBlock(path));
    }

    IEnumerator MoveToBlock(List<Block> path) {
        foreach(Block block in path) {
            transform.position = block.transform.position;
            yield return new WaitForSeconds(movementPeriod);
        }
        ReachGoalSequence();
    }

    void ReachGoalSequence() {
        ParticleSystem goalFx = Instantiate(goalParticleSystem, transform.position, Quaternion.identity);
        goalFx.Play();
        Destroy(gameObject);
    }

    void OnParticleCollision(GameObject other) {
        health--;
        hitParticleSystem.Play();
        GetComponent<AudioSource>().PlayOneShot(hitSfx);
        if(health < 1) {
            ParticleSystem deathFx = Instantiate(deathParticleSystem, transform.position, Quaternion.identity);
            deathFx.Play();
            Destroy(gameObject);
        }
    }
}
