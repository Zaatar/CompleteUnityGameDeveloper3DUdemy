using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int baseHealth = 5;
    [SerializeField] Text healthText;

    private void Start()
    {
        healthText.text = baseHealth.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        baseHealth--;
        healthText.text = baseHealth.ToString();
    }
}
