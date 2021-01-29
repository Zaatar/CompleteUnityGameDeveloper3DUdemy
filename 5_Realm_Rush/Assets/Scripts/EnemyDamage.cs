using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] ParticleSystem deathParticlePrefab;
    int scorePerHit = 1;
    int hits = 3;
    void Start()
    {
        //AddNonTriggerBoxCollider();
        //AddKinematicRigidBody();
    }

    void AddNonTriggerBoxCollider()
    {
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;
    }

    void AddKinematicRigidBody()
    {
        Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.isKinematic = true;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    void ProcessHit()
    {
        hits -= scorePerHit;
        hitParticlePrefab.Play();
        if (hits <= 0)
        {
            KillEnemy();
        }
    }

    void KillEnemy()
    {
        ParticleSystem death = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        death.Play();
        Destroy(gameObject);
        Destroy(death);
    }
}
