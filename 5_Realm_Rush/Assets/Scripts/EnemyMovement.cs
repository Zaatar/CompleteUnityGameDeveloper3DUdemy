using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    List<Waypoint> path;
    [SerializeField] float movementPeriod = 0.5f;
    [SerializeField] ParticleSystem explosionParticle;

    void Start()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        path = pathfinder.GetPath();
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(movementPeriod);
        }
        SelfDestruct();
    }

    void SelfDestruct()
    {
        PlayExplosionParticleSystem();
        Destroy(gameObject);
    }

    private void PlayExplosionParticleSystem()
    {
        ParticleSystem explosion = Instantiate(explosionParticle, transform.position, Quaternion.identity);
        explosion.Play();
        float deathTimer = explosion.main.duration;
        Destroy(explosion.gameObject, deathTimer);
    }
}