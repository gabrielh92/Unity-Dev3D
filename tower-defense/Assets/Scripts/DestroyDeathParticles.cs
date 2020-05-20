using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDeathParticles : MonoBehaviour
{
    [SerializeField] AudioClip deathSfx;

    void Start()
    {
        GetComponent<AudioSource>().PlayOneShot(deathSfx);
        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration); 
    }
}
