using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{

    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [Range(0,1)] float movementFactor; //0 for not moved, 1 for moved
    [SerializeField] float period = 2f;
    Vector3 startingPosition;
    void Start()
    {
        startingPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float cycles; //grows continually from zero
        if(period <= Mathf.Epsilon) {
            cycles = 0f;
        } else {
            cycles = Time.time / period;
        }
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = rawSinWave /2f + 0.5f;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
