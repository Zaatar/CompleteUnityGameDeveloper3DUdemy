using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] Transform explosionParent;
    [SerializeField] int scorePerHit = 1;
    [SerializeField] int hits = 10;

    Scoreboard scoreBoard;
    private void Start()
    {
        AddNonTriggerBoxCollider();
        scoreBoard = FindObjectOfType<Scoreboard>();
    }

    private void OnParticleCollision(GameObject other)
    {
        //deathFX.SetActive(true);
        //todo consider adding sound effects on each hit
        ProcessHit();
    }

    private void ProcessHit()
    {
        scoreBoard.ScoreHit(scorePerHit);
        hits -= 1;
        if (hits <= 0)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = explosionParent;
        Destroy(gameObject);
    }

    private void AddNonTriggerBoxCollider()
    {
        BoxCollider boxCollider =  gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }
}
