using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] Transform targetEnemy;
    [SerializeField] float shootingRange = 3f;
    [SerializeField] ParticleSystem projectileParticle;

    void PanTowardsEnemy()
    {
        objectToPan.transform.LookAt(targetEnemy);
    }

    void ShootEnemy()
    {
        float distanceTowardsEnemy = 100f;
        print("Initializing distance towards enemy " + distanceTowardsEnemy);
        distanceTowardsEnemy = Vector3.Distance(gameObject.transform.position, targetEnemy.position);
        if (distanceTowardsEnemy <= shootingRange)
        {
            print("Should Shoot");
            Shoot(true);
        }
        else
        {
            print("Should stop shooting");
            Shoot(false);
        }
    }

    void Shoot(bool isActive)
    {
        EmissionModule emission = projectileParticle.emission;
        emission.enabled = isActive;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetEnemy)
        {
            PanTowardsEnemy();
            ShootEnemy();
        } else
        {
            Shoot(false);
        }
    }
}
