using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float healthPoints = 100f;
    [SerializeField] Canvas damageCanvas;

    void Start() {
        damageCanvas.enabled = false;
    }

    public void TakeDamage(float damage) {
        Debug.Log("Taking damage " + damage);
        healthPoints -= damage;
        damageCanvas.enabled = true;
        Invoke("DisableDamageCanvas", 1.5f);
        if(healthPoints < 1) {
            GetComponent<DeathHandler>().Death();
        }
    }

    void DisableDamageCanvas() {
        damageCanvas.enabled = false;
    }
}
