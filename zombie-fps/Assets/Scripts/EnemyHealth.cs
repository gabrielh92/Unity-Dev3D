using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 50f;
    public bool isDead = false;

    public void TakeDamage(float damage) {
        BroadcastMessage("OnDamageTaken");
        hitPoints -= damage;
        if(hitPoints < 1) {
            GetComponent<Animator>().SetTrigger("Dead");
            isDead = true;
            GetComponent<Collider>().enabled = false;
            //Destroy(gameObject);
        }
    }
}
