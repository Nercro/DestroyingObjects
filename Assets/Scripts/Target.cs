using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public float health = 50f;
    public GameObject destroyedObject;
    public Transform destroyedCratesParent;

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0.05f)
        {
            Destroyed();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void Destroyed()
    {
        Instantiate(destroyedObject, transform.position, transform.rotation, destroyedCratesParent);

        Destroy(gameObject);
    }
    
}
