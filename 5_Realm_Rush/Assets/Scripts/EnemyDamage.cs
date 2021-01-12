using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    int scorePerHit = 1;
    int hits = 3;
    // Start is called before the first frame update
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
        print("Dead");
        ProcessHit();
    }

    void ProcessHit()
    {
        print("Entering Process hit");
        hits -= scorePerHit;
        if (hits <= 0)
        {
            KillEnemy();
        }
    }

    void KillEnemy()
    {
        print("Entering kill enemy");
        Destroy(gameObject);
    }
}
