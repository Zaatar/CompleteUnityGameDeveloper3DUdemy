using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] int towerLimit = 5;
    int towerCounter = 0;
    public void AddTower(Waypoint baseWaypoint)
    {
        if(towerCounter < towerLimit)
        {
            Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);
            baseWaypoint.isPlaceable = false;
            towerCounter++;
        }
        else
        {
            print("Tower maximum limit reached");
        }
    }
}
