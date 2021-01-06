using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{

    //todo work out why sometimes it's slow on first scene playthrough
    [Header("General")]
    [Tooltip("In m/s")][SerializeField] float xSpeed = 4f;
    [Tooltip("In m/s")][SerializeField] float ySpeed = 4f;
    [Tooltip("In m")] [SerializeField] float xRange = 6f;
    [Tooltip("In m")] [SerializeField] float yRange = 7f;
    [SerializeField] GameObject[] Guns;

    [Header("Screen-position based")]
    [SerializeField] float positionPitchFactor = -2.5f;
    [SerializeField] float positionYawFactor = 2.5f;

    [Header("Control-throw based")]
    [SerializeField] float controlPitchFactor = -5f;
    [SerializeField] float controlRollFactor = -10f;
    float xThrow, yThrow;
    bool isControlEnabled = true;
    bool isFiring = true;

    void Update()
    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }

    void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float yOffset = yThrow * ySpeed * Time.deltaTime;

        float rawNewXPos = transform.localPosition.x + xOffset;
        float rawNewYPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(rawNewXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(rawNewYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            ActivateGuns();
        } else
        {
            DeactivateGuns();
        }
    }

    void ActivateGuns()
    {
        foreach (GameObject gun in Guns)
        {
            gun.SetActive(true);
        }
    }

    void DeactivateGuns()
    {
        foreach(GameObject gun in Guns)
        {
            gun.SetActive(false);
        }
    }

    void OnPlayerDeath() //called by string reference
    {
        isControlEnabled = false;
    }
}
