using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera fpsCamera;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] float range = 100f;
    [SerializeField] float attackPower = 1f;
    [SerializeField] ParticleSystem muzzleFlashFx;
    [SerializeField] GameObject hitFx;
    [SerializeField] float shotDelay = 2f;
    [SerializeField] TextMeshProUGUI ammoText;

    bool canShoot = true;

    void OnEnable() {
        canShoot = true;        
    }

    void Update()
    {
        DisplayAmmo();
        if(Input.GetButtonDown("Fire1") && canShoot) {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot() {
        canShoot = false;
        if(ammoSlot.GetAmmoAmount(ammoType) > 0) {
            muzzleFlashFx.Play();
            RaycastHit hit;
            if(Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range)) {
                GameObject impact = Instantiate(hitFx, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 0.1f);
                EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
                if(target) {
                    target.TakeDamage(attackPower);
                }
            }
            ammoSlot.ReduceAmmoAmount(ammoType);
        }
        yield return new WaitForSeconds(shotDelay);
        canShoot = true;
    }

    void DisplayAmmo() {
        string gunType = "";
        if(ammoType == AmmoType.Bullets) gunType = "pistol";
        else if(ammoType == AmmoType.Rockets) gunType = "carbine";
        else if(ammoType == AmmoType.Shells) gunType = "shotgun";
        int currentAmmo = ammoSlot.GetAmmoAmount(ammoType);
        ammoText.text = gunType + " " + currentAmmo.ToString();
    }
}
