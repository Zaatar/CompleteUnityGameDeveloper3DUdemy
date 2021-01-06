using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("Level load delay in seconds")][SerializeField] float loadLevelDelay = 2f;
    [Tooltip("Particle to play on player death")] [SerializeField] GameObject deathFX;

    private void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
    }

    private void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
        deathFX.SetActive(true);
        Invoke("ReloadLevel", loadLevelDelay);
    }

    private void ReloadLevel() //String referenced method
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
