using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parentSpawned;
    [SerializeField] int scorePerHit = 12;

    ScoreBoard scoreBoard;

    bool isDead; // False by Default

	// Use this for initialization
	void Start () {
        AddBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
	}

    private void AddBoxCollider()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    void OnParticleCollision(GameObject other)
    {

        if (!isDead)
        {
            isDead = true;
            scoreBoard.ScoreHit(scorePerHit);
            print("Hit: " + gameObject.name);
            GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
            fx.transform.parent = parentSpawned;
            Destroy(gameObject);
        }
    }
}
