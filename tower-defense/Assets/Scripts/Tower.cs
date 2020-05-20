using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] float attackRange = 10f;
    [SerializeField] AudioClip fireSfx;

    public Block block;

    // Update is called once per frame
    void Update() {
        Transform targetEnemy = FindTarget();
        if(targetEnemy) {
            objectToPan.LookAt(targetEnemy.GetComponentInChildren<Renderer>().bounds.center);
            particleSystem.transform.LookAt(targetEnemy);
            float dist = Vector3.Distance(gameObject.transform.position, targetEnemy.transform.position);
            Fire(dist < attackRange);
        } else {
            Fire(false);
        }

    }

    Transform FindTarget() {
        Enemy[] sceneEnemies = FindObjectsOfType<Enemy>();
        if(sceneEnemies.Length == 0) return null;
        Transform closest = sceneEnemies[0].transform;
        foreach (Enemy sceneEnemy in sceneEnemies) {
            closest = ClosestEnemy(closest, sceneEnemy.transform);
        }
        return closest;
    }

    Transform ClosestEnemy(Transform a, Transform b) {
        float aDist = Vector3.Distance(gameObject.transform.position, a.position);
        float bDist = Vector3.Distance(gameObject.transform.position, b.position);
        if(aDist < bDist) return a;
        return b;
    }

    void Fire(bool shouldFire) {
        //if(shouldFire) GetComponent<AudioSource>().PlayOneShot(fireSfx);
        var em = particleSystem.emission;
        em.enabled = shouldFire;
    }
}
