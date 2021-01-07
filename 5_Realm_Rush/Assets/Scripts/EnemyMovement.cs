using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Waypoint> path;
    void Start()
    {
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        Debug.Log("Starting Patrol");
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            Debug.Log("Visiting Block " + waypoint.name);
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("Ending Patrol");
    }

    void Update()
    {

    }
}
