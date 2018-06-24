using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;

    bool isDead; // False by Default

	// Use this for initialization
	void Start () {
        AddNonTriggerBoxCollider();
	}

    private void AddNonTriggerBoxCollider()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    void OnParticleCollision(GameObject other)
    {

        if (!isDead)
        {
            isDead = true;
            print("Hit: " + gameObject.name);
            GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
            fx.transform.parent = parent;
            Destroy(gameObject);
        }
    }
}
