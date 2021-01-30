using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] ParticleSystem deathParticlePrefab;
    [SerializeField] AudioClip enemyHitSFX;
    [SerializeField] AudioClip enemyDeathSFX;
    int scorePerHit = 1;
    int hits = 3;

    AudioSource myAudioSource;

    void Start()
    {
        //AddNonTriggerBoxCollider();
        //AddKinematicRigidBody();
        myAudioSource = GetComponent<AudioSource>();
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
        myAudioSource.PlayOneShot(enemyHitSFX);
        hits -= scorePerHit;
        hitParticlePrefab.Play();
        if (hits <= 0)
        {
            KillEnemy();
        }
    }

    void KillEnemy()
    {
        PlayDeathParticleEffect();
        AudioSource.PlayClipAtPoint(enemyDeathSFX, Camera.main.transform.position);
        Destroy(gameObject);
    }

    private void PlayDeathParticleEffect()
    {
        ParticleSystem death = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        death.Play();
        float deathTimer = death.main.duration;
        Destroy(death.gameObject, deathTimer);
    }
}
