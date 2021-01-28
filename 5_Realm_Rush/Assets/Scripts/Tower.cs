using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Tower : MonoBehaviour
{
    //Parameters
    [SerializeField] Transform objectToPan;
    [SerializeField] float shootingRange = 3f;
    [SerializeField] ParticleSystem projectileParticle;


    //State
    Transform targetEnemy;
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
        SetTargetEnemy();
        if (targetEnemy)
        {
            PanTowardsEnemy();
            ShootEnemy();
        } else
        {
            Shoot(false);
        }
    }

    private void SetTargetEnemy()
    {
        EnemyDamage[] enemyList = FindObjectsOfType<EnemyDamage>();
        if(enemyList.Length == 0) { return; }
        Transform closestEnemy = enemyList[0].transform;
        foreach (EnemyDamage enemy in enemyList)
        {
            closestEnemy = GetClosest(closestEnemy, enemy.transform);
        }
        targetEnemy = closestEnemy;
    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        float distToA = Vector3.Distance(transform.position, transformA.position);
        float distToB = Vector3.Distance(transform.position, transformB.position);
        if(distToA > distToB)
        {
            return transformB;
        }
        return transformA;
    }
}
