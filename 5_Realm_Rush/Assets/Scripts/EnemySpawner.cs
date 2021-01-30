using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.1f,120f)][SerializeField] float waitBeforeSpawn = 2f;
    [SerializeField] EnemyMovement objectToSpawn;
    [SerializeField] Text scoreText;
    [SerializeField] AudioClip enemySpawnSFX;
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
            AddScore();
            GetComponent<AudioSource>().PlayOneShot(enemySpawnSFX);
            EnemyMovement enemy = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
            enemy.transform.parent = this.transform;
            yield return new WaitForSeconds(waitBeforeSpawn);
        }
    }

    private void AddScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
