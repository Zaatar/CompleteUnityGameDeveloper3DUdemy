using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.1f,120f)][SerializeField] float waitBeforeSpawn = 2f;
    [SerializeField] EnemyMovement objectToSpawn;
    [SerializeField] Text scoreText;
    int score = 0;

    private void Start()
    {
        scoreText.text = score.ToString();
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            EnemyMovement enemy = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
            enemy.transform.parent = this.transform;
            score++;
            scoreText.text = score.ToString();
            yield return new WaitForSeconds(waitBeforeSpawn);
        }
    }
}
