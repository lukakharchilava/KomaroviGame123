using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float FireCharge = 15f;

    private float nextTimeToShoot = 0f;

    [Header("Rifle Ammo and shoot")]
    public int MaxAmmo = 32;
    public int mag = 10;
    private int CurrentAmmo;
    public float ReloadTime = 1.3f;
    private bool SetReloading = false;

    public Camera cam;
    


    [Header("Rifle Effects")]
    public ParticleSystem muzzleSpark;
    public GameObject GoreEffect;


    private void Awake()
    {
        CurrentAmmo = MaxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (SetReloading)
            return;
        if(CurrentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }


        if (Input.GetButton("Fire1") && Time.time >= nextTimeToShoot)
        {
            nextTimeToShoot = Time.time + 1f/FireCharge;
            Shoot();
        }
    }


    void Shoot()
    {
        if(mag == 0)
        {

            return;
        }

        CurrentAmmo--;

        if(CurrentAmmo == 0)
        {
            mag--;
        }

        AmmoCount.Instance.UpdateAmmoText(CurrentAmmo);
        AmmoCount.Instance.UpdateMagText(mag);


        muzzleSpark.Play();
        RaycastHit hitInfo;

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, range))
        {
            Target target = hitInfo.transform.GetComponent<Target>();
            ZombieMovement zombie1 = hitInfo.transform.GetComponent<ZombieMovement>();

            if (target != null)
            {
                target.TakeDamage(damage);

            }

            else if( zombie1 != null) 
            {
                zombie1.zombieHitDamage(damage);
                GameObject impactGo = Instantiate(GoreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(impactGo, 1f);
                
            }
        }
    }
    IEnumerator Reload()
    {
        SetReloading = true;
        //play reload sound
        yield return new WaitForSeconds(ReloadTime);
        CurrentAmmo = MaxAmmo;
        SetReloading = false;
    }

}
