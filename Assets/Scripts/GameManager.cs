using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public GameObject npcPrefab;
    [SerializeField] private int numberOfNPCs = 10;

    private void Start()
    {
        GraphConstraint.Instance.OnWalkableNodesUpdated += StartSpawning;
    }

    private void OnDestroy()
    {
        GraphConstraint.Instance.OnWalkableNodesUpdated -= StartSpawning;
    }

    private void StartSpawning()
    {
        for (int i = 0; i < numberOfNPCs; i++)
        {
            SpawnRandomNPC();
        }
    }

    private void SpawnRandomNPC()
    {
        var walkableNodes = GraphConstraint.Instance.walkableNodes;
        if (walkableNodes.Count == 0) return; // No place to spawn

        // Get a random walkable position
        int randomIndex = Random.Range(0, walkableNodes.Count);
        Vector3 spawnPosition = (Vector3) walkableNodes[randomIndex];

        // Spawn the NPC
        Instantiate(npcPrefab, spawnPosition, Quaternion.identity);
    }
}