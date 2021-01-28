using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.1f,120f)][SerializeField] float waitBeforeSpawn = 2f;
    [SerializeField] EnemyMovement objectToSpawn;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Instantiate(objectToSpawn, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(waitBeforeSpawn);
        }
    }
}
