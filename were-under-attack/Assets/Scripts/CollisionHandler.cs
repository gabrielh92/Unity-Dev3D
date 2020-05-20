using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("s")][SerializeField] float levelLoadDelay = 1f;
    [Tooltip("Particle System")][SerializeField] GameObject deathFx;

    void OnTriggerEnter(Collider collider) {
        StartDeathSequence();
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void StartDeathSequence() {
        SendMessage("OnPlayerDeath");
        deathFx.SetActive(true);
    }

    void ReloadLevel() { // referenced by string
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
