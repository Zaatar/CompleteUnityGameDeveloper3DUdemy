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
    Waypoint searchCenter;
    List<Waypoint> path = new List<Waypoint>();

    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    private void ColorCoreWaypoints()
    {
        start.SetTopColor(Color.green);
        end.SetTopColor(Color.red);
    }

    private void BreadthFirstSearch()
    {
        waypointQueue.Enqueue(start);
        while(waypointQueue.Count > 0 && isPathFinderRunning)
        {
            searchCenter = waypointQueue.Dequeue();
            searchCenter.isExplored = true;
            if(searchCenter == end)
            {
                isPathFinderRunning = false;
            }
            ExploreNeighbours();
        }
    }

    private void CreatePath()
    {
        SetAsPath(end);
        Waypoint previous = end.exploredFrom;
        while(previous != start)
        {
            SetAsPath(previous);
            previous = previous.exploredFrom;
        }
        SetAsPath(start);
        path.Reverse();
    }

    private void SetAsPath(Waypoint waypoint)
    {
        path.Add(waypoint);
        waypoint.isPlaceable = false;
    }

    private void ExploreNeighbours()
    {
        if (isPathFinderRunning)
        {
            foreach (Vector2Int direction in directions)
            {
                QueueNewNeighbours(direction);
            }
        }
    }

    private void QueueNewNeighbours(Vector2Int direction)
    {
        Vector2Int neighbor = searchCenter.GetGridPos() + direction;
        if (grid.TryGetValue(neighbor, out Waypoint neighborCell))
        {
            if (!neighborCell.isExplored && !waypointQueue.Contains(neighborCell))
            {
                waypointQueue.Enqueue(neighborCell);
                neighborCell.exploredFrom = searchCenter;
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

    public List<Waypoint> GetPath()
    {
        if(path.Count == 0)
        {
            CalculatePath();
        }
        return path;
    }

    private void CalculatePath()
    {
        LoadBlocks();
        ColorCoreWaypoints();
        BreadthFirstSearch();
        CreatePath();
    }

    void Update()
    {
//        PathFind();
    }
}
