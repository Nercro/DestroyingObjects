using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    [Header("Gun Properties")]
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 10f;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Camera fpsCam;

    [Header("Grenade Properties")]
    public GameObject grenadePrefab;
    public float greandeThrowForce = 50f;
    public float camOffset = 2f;


    
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if (Input.GetMouseButtonDown(1))
        {
            ThrowGrenade();
        }
	}

    public void Shoot()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO =  Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }

    private void ThrowGrenade()
    {
        GameObject grenadePrefabClone = Instantiate(grenadePrefab, new Vector3(fpsCam.transform.position.x, fpsCam.transform.position.y - camOffset, fpsCam.transform.position.z), fpsCam.transform.rotation);

        Rigidbody grenadePrefabCloneRigidbody = grenadePrefabClone.GetComponent<Rigidbody>();
        grenadePrefabCloneRigidbody.AddForce(fpsCam.transform.forward * greandeThrowForce, ForceMode.VelocityChange);
    }
}
