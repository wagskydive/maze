using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(LineRenderer))]
public class PathRenderer : MonoBehaviour
{
    NavMeshAgent agent;

    LineRenderer lineRenderer;
    LevelController levelController;
    List<int[]> tiles = new List<int[]>();


    private void Start()
    {
        levelController = FindObjectOfType<LevelController>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        lineRenderer = GetComponent<LineRenderer>();
        LevelController.OnNewMazeSize += CreatePath;
    }

    void CreatePath(int rows, int columns)
    {
        transform.position = Vector3.zero;
        agent.speed = 0;
        agent.SetDestination(new Vector3((columns - 1) * 4, 1, (rows - 1) * 4));
        tiles = new List<int[]>();
        Invoke("HighlightDelayed",2);
        
    }

    void HighlightDelayed()
    {
        HighlightPathTiles(agent.path.corners);
    }

    void HighlightPathTiles(Vector3[] pathVectors)
    {
        int[] lastRc = new int[2];


        for (int i = 0; i < pathVectors.Length; i++)
        {
            int[] rc = new int[2];

            rc[0] = (int)(pathVectors[i].z+2) / 4;
            if(rc[0] - lastRc[0] > 1)
            {
                for (int j = 0; j < rc[0] - lastRc[0]; j++)
                {
                    int[] rcR = new int[2];
                    rcR[0] = lastRc[0] + j;
                    rcR[1] = lastRc[1];
                    tiles.Add(rcR);
                }
            }
            if (rc[0] - lastRc[0] < -1)
            {
                for (int j = 0; j < Mathf.Abs(rc[0] - lastRc[0]); j++)
                {
                    int[] rcR = new int[2];
                    rcR[0] = lastRc[0] - j;
                    rcR[1] = lastRc[1];
                    tiles.Add(rcR);
                }
            }

            rc[1] = (int)(pathVectors[i].x+2) / 4;
            if (rc[1] - lastRc[1] > 1)
            {
                for (int j = 0; j < rc[1] - lastRc[1]; j++)
                {
                    int[] rcR = new int[2];
                    rcR[0] = lastRc[0];
                    rcR[1] = lastRc[1] + j;
                    tiles.Add(rcR);
                }
            }

            if (rc[1] - lastRc[1] < -1)
            {
                for (int j = 0; j < Mathf.Abs( rc[1] - lastRc[1]); j++)
                {
                    int[] rcR = new int[2];
                    rcR[0] = lastRc[0];
                    rcR[1] = lastRc[1] - j;
                    tiles.Add(rcR);
                }
            }


            tiles.Add(rc);
            lastRc = rc;
        }

        //AddMissingTiles(tiles);
        //levelController.SetColors(tiles, Color.cyan);
    }

    private void AddMissingTiles(List<int[]> tiles)
    {
        int[] previousTile = new int[2];
        previousTile[0] = 0;
        previousTile[1] = 0;
        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i][0] - previousTile[0] > 1)
            {
                int[] missingTile = previousTile;
                previousTile[0] += 1;
                tiles.Add(missingTile);
            }
            if (tiles[i][1] - previousTile[1] > 1)
            {
                int[] missingTile = previousTile;
                previousTile[1] += 1;
                tiles.Add(missingTile);
            }
            previousTile = tiles[i];
        }
    }

    private void Update()
    {
        if(agent.path.corners.Length > 1)
        {
            lineRenderer.positionCount = agent.path.corners.Length;
            lineRenderer.SetPositions(agent.path.corners);
        }
    }
}
