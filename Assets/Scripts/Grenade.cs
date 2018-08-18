using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {


    public float delay = 3f;
    public float blastRadius = 5f;
    public float explosionForce = 700f;

    public GameObject explosionEffect;

    private float countdown;
    private bool _hasExploded = false;

	// Use this for initialization
	void Start () {
        countdown = delay;
	}
	
	// Update is called once per frame
	void Update () {

        countdown -= Time.deltaTime;

        if (countdown <= 0.05f && !_hasExploded || Input.GetMouseButtonDown(2))
        {
            Explode();
            _hasExploded = true;
        }

	}

    public void Explode()
    {
        Debug.Log("Boom!");
        GameObject explosionEffectclone = Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (Collider nearbyObjects in collidersToDestroy)
        {
            Target target = nearbyObjects.GetComponent<Target>();
            if (target)
            {
                target.Destroyed();
            }
        }

        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (Collider nearbyObjectsCracked in collidersToMove)
        {
            Rigidbody nearbyObjectRigidbody = nearbyObjectsCracked.GetComponent<Rigidbody>();

            if (nearbyObjectRigidbody)
            {
                nearbyObjectRigidbody.AddExplosionForce(explosionForce, transform.position, blastRadius);
            }
        }

        Destroy(gameObject);
        Destroy(explosionEffectclone, 3f);

    }
}
