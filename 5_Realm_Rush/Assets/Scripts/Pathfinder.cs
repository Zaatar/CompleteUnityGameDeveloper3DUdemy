using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    [SerializeField] Waypoint start;
    [SerializeField] Waypoint end;
    Waypoint currentLocation;
    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    void Start()
    {
        LoadBlocks();
        ColorCoreWaypoints();
        currentLocation = start;
    }

    private void ColorCoreWaypoints()
    {
        start.SetTopColor(Color.green);
        end.SetTopColor(Color.red);
    }

    private void ExploreNeighbours()
    {
        foreach(Vector2Int direction in directions)
        {
            Waypoint neighborCell;
            Vector2Int neighbor = currentLocation.GetGridPos() + direction;
            if (grid.TryGetValue(neighbor, out neighborCell))
            {
                neighborCell.SetTopColor(Color.cyan);
            }
        }
    }

    private void LoadBlocks()
    {
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();
        foreach(Waypoint waypoint in waypoints)
        {
            if (grid.ContainsKey(waypoint.GetGridPos()))
            {
                Debug.LogWarning("Overlapping block at " + waypoint.GetGridPos());
            }
            grid.Add(waypoint.GetGridPos(), waypoint);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ExploreNeighbours();
    }
}
