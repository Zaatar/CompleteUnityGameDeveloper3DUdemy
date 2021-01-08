using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    [SerializeField] Waypoint start;
    [SerializeField] Waypoint end;
    Queue<Waypoint> waypointQueue = new Queue<Waypoint>();
    bool isPathFinderRunning = true;

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
        PathFind();
    }

    private void ColorCoreWaypoints()
    {
        start.SetTopColor(Color.green);
        end.SetTopColor(Color.red);
    }

    private void PathFind()
    {
        waypointQueue.Enqueue(start);
        while(waypointQueue.Count > 0 && isPathFinderRunning)
        {
            Waypoint searchCenter = waypointQueue.Dequeue();
            searchCenter.isExplored = true;
            if(searchCenter == end)
            {
                isPathFinderRunning = false;
                Debug.Log("At end point stopping search");
            }
            ExploreNeighbours(searchCenter);
        }
    }

    private void ExploreNeighbours(Waypoint from)
    {
        if (isPathFinderRunning)
        {
            foreach (Vector2Int direction in directions)
            {
                QueueNewNeighbours(from, direction);
            }
        }
    }

    private void QueueNewNeighbours(Waypoint from, Vector2Int direction)
    {
        Vector2Int neighbor = from.GetGridPos() + direction;
        if (grid.TryGetValue(neighbor, out Waypoint neighborCell))
        {
            neighborCell.SetTopColor(Color.cyan);
            if (!neighborCell.isExplored && !waypointQueue.Contains(neighborCell))
            {
                waypointQueue.Enqueue(neighborCell);
                Debug.Log("Queueing waypoint at : " + neighborCell.name);
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

    void Update()
    {
//        PathFind();
    }
}
