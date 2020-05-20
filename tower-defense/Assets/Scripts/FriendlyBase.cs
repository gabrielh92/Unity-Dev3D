using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendlyBase : MonoBehaviour
{
    [SerializeField][Range(1,100)] int health = 20;
    [SerializeField] Text healthText;
    [SerializeField] AudioClip hitSfx;

    string textName;
    void Start() {
        textName = healthText.text;
        healthText.text = textName + health.ToString();
    }

    private void OnTriggerEnter(Collider other) {
        GetComponent<AudioSource>().PlayOneShot(hitSfx);
        health--;
        healthText.text = textName + health.ToString();
    }
}
