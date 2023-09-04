using System;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GraphConstraint : MonoBehaviour
{
    public static GraphConstraint Instance;
    public List<Vector3> walkableNodes;

    public float leftBound = -10f;
    public float rightBound = 10f;
    public float topBound = 10f;
    public float bottomBound = -10f;
    
    public event Action OnWalkableNodesUpdated;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }

    private void Init()
    {
        AstarPath.active.AddWorkItem(new AstarWorkItem(ctx => {
            var gg = AstarPath.active.data.gridGraph;
            gg.GetNodes(node => {
                Vector3 worldPoint = (Vector3)node.position;
                if (worldPoint.x < leftBound || worldPoint.x > rightBound ||
                    worldPoint.y < bottomBound || worldPoint.y > topBound) {
                    node.Walkable = false;
                }
                else
                {
                    walkableNodes.Add(worldPoint);
                }
            });

            // Recalculate all grid connections
            // This is required because we have updated the walkability of some nodes
            // Note: In a future update, you'll want to change this to gg.RecalculateAllConnections, for performance.
            gg.GetNodes(node => gg.CalculateConnections((GridNodeBase)node));
            OnWalkableNodesUpdated?.Invoke();
        }));
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Draw rectangle borders
        Vector3 bottomLeft = new Vector3(leftBound, bottomBound);
        Vector3 topLeft = new Vector3(leftBound, topBound);
        Vector3 topRight = new Vector3(rightBound, topBound);
        Vector3 bottomRight = new Vector3(rightBound, bottomBound);

        Gizmos.DrawLine(bottomLeft, topLeft);
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
    }
}