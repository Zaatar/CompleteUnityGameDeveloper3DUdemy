using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructor : MonoBehaviour
{
    [SerializeField] float destroyTimer = 2f;
    void Start()
    {
        Destroy(this.gameObject, destroyTimer);   
    }
}
