using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] RigidbodyFirstPersonController rigidbodyFirstPersonController;
    [Range(10, 60)][SerializeField] float zoomedIn = 20;
    [Range(60, 90)][SerializeField] float zoomedOut = 60;
    [SerializeField] float zoomOutMouseSensitivity = 2f;
    [SerializeField] float zoomInMouseSensitivity = 0.5f;
    
    bool isZoomedIn = false;
    void Update() {
        if(Input.GetButtonDown("Fire2")) {
            ChangeFOV();
        }
    }

    void ChangeFOV() {
        float currFOV = playerCamera.fieldOfView;
        if(!isZoomedIn) {
            rigidbodyFirstPersonController.AdjustMouseSensitivity(zoomInMouseSensitivity);
            playerCamera.fieldOfView = zoomedIn;
            isZoomedIn = true;
        } else {
            rigidbodyFirstPersonController.AdjustMouseSensitivity(zoomOutMouseSensitivity);
            playerCamera.fieldOfView = zoomedOut;
            isZoomedIn = false;
        }
    }

    public void DisableZoomIn() {
        isZoomedIn = true; // this will force zoom status to off
        ChangeFOV();
    }
}
