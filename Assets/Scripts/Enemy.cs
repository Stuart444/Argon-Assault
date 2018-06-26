using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parentSpawned;
    [SerializeField] int scorePerHit = 12;
    [SerializeField] int maxHits = 3;

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
        ProcessHit();
        if (maxHits <= 0)
        {
            KillEnemy();
        }
    }

    private void ProcessHit()
    {
        scoreBoard.ScoreHit(scorePerHit);
        maxHits--; // same as saying maxHits = maxHits - 1
    }

    private void KillEnemy()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parentSpawned;
        Destroy(gameObject);
    }
}
