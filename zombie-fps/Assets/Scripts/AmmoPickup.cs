using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int ammoAmount = 5;
    [SerializeField] AmmoType ammoType;

    // player has the only collider setup for this
    // an alternative is to use tags
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            FindObjectOfType<Ammo>().IncreaseAmmo(ammoType, ammoAmount);
            Destroy(gameObject);
        }   
    }
}
