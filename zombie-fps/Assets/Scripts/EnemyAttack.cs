using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float damage = 20f;
    PlayerHealth target;

    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();  
        Debug.Log("start enemy attack target: " + target);    
    }

    public void AttackHitEvent() {
        Debug.Log("Attacking target");
        if(target) {
            Debug.Log("found!");
            target.TakeDamage(damage);
        }
    }
}
