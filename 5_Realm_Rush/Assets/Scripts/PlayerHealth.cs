using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int baseHealth = 5;
    [SerializeField] Text healthText;
    [SerializeField] AudioClip playerDamageSFX;

    private void Start()
    {
        healthText.text = baseHealth.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        baseHealth -= 1;
        GetComponent<AudioSource>().PlayOneShot(playerDamageSFX);
        healthText.text = baseHealth.ToString();
    }
}
