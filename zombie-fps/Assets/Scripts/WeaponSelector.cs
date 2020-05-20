using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelector : MonoBehaviour
{
    [SerializeField] int currentWeapon = 0;

    void Start() {
        SetActiveWeapon();
    }

    void Update()
    {
        int previousWeapon = currentWeapon;

        ProcessKeyInput();
        ProcessScrollWheel();

        if(previousWeapon != currentWeapon) {
            if(previousWeapon == 0) {
                FindObjectOfType<WeaponZoom>().DisableZoomIn();
            }
            SetActiveWeapon();
        }
    }

    void SetActiveWeapon() {
        int weaponIndex = 0;
        foreach (Transform weapon in transform) {
            if(weaponIndex == currentWeapon) {
                weapon.gameObject.SetActive(true);
            } else {
                weapon.gameObject.SetActive(false);
            }
            weaponIndex++;
        }
    }

    void ProcessKeyInput() {
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            currentWeapon = 0;
        } else if(Input.GetKeyDown(KeyCode.Alpha2)) {
            currentWeapon = 1;
        } else if(Input.GetKeyDown(KeyCode.Alpha3)) {
            currentWeapon = 2;
        }
    }

    void ProcessScrollWheel() {
        if(Input.GetAxis("Mouse ScrollWheel") < 0) {
            Debug.Log("childCount: " + transform.childCount);
            if(currentWeapon >= transform.childCount - 1){
                currentWeapon = 0;
            } else { 
                currentWeapon++;
            }
        }
        if(Input.GetAxis("Mouse ScrollWheel") > 0) {
            Debug.Log("childCount2: " + transform.childCount);
            if(currentWeapon == 0) {
                currentWeapon = transform.childCount - 1;
            } else {
                currentWeapon--;
            }
        }
    }
}
