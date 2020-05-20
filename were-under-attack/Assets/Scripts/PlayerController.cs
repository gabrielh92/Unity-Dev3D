using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("X-Axis Movement")]
    [Tooltip("m/s")][SerializeField] float xSpeed = 25f;
    [Tooltip("m")][SerializeField] float xLimit = 20f;

    [Header("Y-Axis Movement")]
    [Tooltip("m/s")][SerializeField] float ySpeed = 30f;
    [Tooltip("m")][SerializeField] float yLimit = 13f;

    [Header("Rotation")]
    [SerializeField] float posPitchFactor = -1.79f;
    [SerializeField] float throwPitchFactor = -20f;
    [SerializeField] float posYawFactor = 2.17f;
    [SerializeField] float posRollFactor = 0f;
    [SerializeField] float rollThrowFactor = -21.50f;

    [Header("Lasers")]
    [SerializeField] GameObject[] lasers;

    float xThrow, yThrow;
    bool isControlEnabled = true;

    void Update()
    {
        if(isControlEnabled) {
            Translate();
            Rotate();
            Fire();
        }
    }

    void Fire() {
        SetLasers(true);
        /*
        if(Input.GetButton("Fire")) {
            SetLasers(true);
        } else {
            SetLasers(false);
        }*/
    }

    void SetLasers(bool status) {
        foreach (GameObject laser in lasers) {
            var em = laser.GetComponent<ParticleSystem>().emission;
            em.enabled = status;
        }
    }

    void Rotate() {
        float pitch = (posPitchFactor * transform.localPosition.y) + (throwPitchFactor * yThrow);
        float yaw = (posYawFactor * transform.localPosition.x);
        float roll = (posRollFactor * transform.localPosition.z) + (rollThrowFactor * xThrow);
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void Translate() {
        xThrow = Input.GetAxis("Horizontal");
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float rawX = Mathf.Clamp(transform.localPosition.x + xOffset, -xLimit, xLimit);

        yThrow = Input.GetAxis("Vertical");
        float yOffset =  yThrow * ySpeed * Time.deltaTime;
        float rawY = Mathf.Clamp(transform.localPosition.y + yOffset, -yLimit, yLimit);
        transform.localPosition = new Vector3(rawX, rawY, transform.localPosition.z);
    }

    void OnPlayerDeath() { // string reference
        isControlEnabled = false;
    }
}
