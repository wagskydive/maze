using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinderTree : MonoBehaviour
{
    public static event Action<MazeNode> OnMazeNodeVisited;

    public static event Action<List<int[]>> OnRouteCreated;

    int rows;
    int columns;

    int totalNodesLeft;

    MazeNode[,] mazeNodes;

    List<int[]> RouteOptionsLeft = new List<int[]>();
    List<List<int[]>> allRoutes = new List<List<int[]>>();

    LevelController levelController;

    private void Start()
    {
        LevelController.OnNewMazeNodes += SetupNewMaze;
        LevelController.OnTileClicked += TileClickHandler;
        levelController = FindObjectOfType<LevelController>();
        
    }

    void TileClickHandler(int[] tile)
    {
        //levelController.SetAllColors(Color.blue);
        CreateRoute(tile);
    }

    void SetupNewMaze(MazeNode[,] nodes)
    {
        mazeNodes = nodes;
        rows = nodes.GetLength(0);
        columns = nodes.GetLength(1);
        RouteOptionsLeft = new List<int[]>();
        totalNodesLeft = rows * columns;
        TraverseMaze();

        //int[] pos = { rows-1,columns-1 };
        //HighlightRoute(pos);
    }


    
    void TraverseMaze()
    {
        mazeNodes[0, 0].isVisited = true;
        mazeNodes[0, 0].parentNode = new int[] { 0, 0 };
        RouteOptionsLeft = FindNeighbors(mazeNodes[0, 0]);
        totalNodesLeft--;

        while (totalNodesLeft>-10 && RouteOptionsLeft.Any())
        {
            CheckRouteOptions(RouteOptionsLeft[0][0], RouteOptionsLeft[0][1]);
            RouteOptionsLeft.RemoveAt(0);
            //Debug.Log("Nodes Left: " + totalNodesLeft);
        }
        

    }

    private void CheckRouteOptions(int row, int column)//List<int[]> routeStartOptions)
    {

        MazeNode nodeToVisit = mazeNodes[row, column];
        List<int[]> neighbors = FindNeighbors(nodeToVisit);
        mazeNodes[row,column].isVisited = true;

        totalNodesLeft--;
        if (neighbors.Any())
        {
            RouteOptionsLeft.AddRange(neighbors);
        }

    }

    public List<int[]>CreateRoute(int[] goal)
    {
        List<int[]> route = new List<int[]>();
        int[] currentStep = goal;

        int[] startPoint = { 0, 0 };
        //Color col = new Color(UnityEngine.Random.Range(0, .3f), UnityEngine.Random.Range(0, .3f), UnityEngine.Random.Range(0, .3f));
        for (int i = 0; i < 1000; i++)
        {
            if (currentStep[0] == 0 && currentStep[1] == 0)
            {
                break;
            }
            //levelController.AddFloorTileColor(currentStep[0], currentStep[1], col);
            currentStep = mazeNodes[currentStep[0], currentStep[1]].parentNode;
            route.Add(currentStep);


        }
        Debug.Log("Route takes " + route.Count + " steps.");

        OnRouteCreated?.Invoke(route);

        return route;
    }

    public List<int[]> FindNeighbors(MazeNode nodeToVisit)
    {
        //Debug.Log("Visiting Node: X(Column): " + nodeToVisit.Column + " Y(Row): " + nodeToVisit.Row);
        List<int[]> neighbors = new List<int[]>();

        if (!nodeToVisit.cellInfo.WallBack && nodeToVisit.Row - 1 >= 0)
        {
            MazeNode neighborCandidate = mazeNodes[nodeToVisit.Row - 1, nodeToVisit.Column];
            if (!neighborCandidate.isVisited)
            {
                int[] loc = new int[2];
                loc[0] = nodeToVisit.Row - 1;
                loc[1] = nodeToVisit.Column;
                
                mazeNodes[loc[0],loc[1]].parentNode = new int[] { nodeToVisit.Row, nodeToVisit.Column};
                //Debug.Log("Found Neigbor: Row: " + loc[0] + " Column: " + loc[1]);
                neighbors.Add(loc);
            }

        }
        if (!nodeToVisit.cellInfo.WallFront && nodeToVisit.Row + 1 < rows)
        {
            MazeNode neighborCandidate = mazeNodes[nodeToVisit.Row + 1, nodeToVisit.Column];
            if (!neighborCandidate.isVisited)
            {
                int[] loc = new int[2];
                loc[0] = nodeToVisit.Row + 1;
                loc[1] = nodeToVisit.Column;
                
                mazeNodes[loc[0], loc[1]].parentNode = new int[] { nodeToVisit.Row, nodeToVisit.Column };
                //Debug.Log("Found Neigbor: X: " + loc[0] + " Y: " + loc[1]);
                neighbors.Add(loc);
            }
        }
        if (!nodeToVisit.cellInfo.WallLeft && nodeToVisit.Column - 1 >= 0)
        {

            MazeNode neighborCandidate = mazeNodes[nodeToVisit.Row, nodeToVisit.Column - 1];
            if (!neighborCandidate.isVisited)
            {
                int[] loc = new int[2];
                loc[0] = nodeToVisit.Row;
                loc[1] = nodeToVisit.Column-1;
                
                mazeNodes[loc[0], loc[1]].parentNode = new int[] {nodeToVisit.Row, nodeToVisit.Column};
                //Debug.Log("Found Neigbor: X: " + loc[0] + " Y: " + loc[1]);
                neighbors.Add(loc);
            }
        }
        if (!nodeToVisit.cellInfo.WallRight && nodeToVisit.Column + 1 < columns)
        {
            //Debug.Log("Found Candidate Neigbor: X: " + (nodeToVisit.Column + 1 )+ " Y: " + nodeToVisit.Row);


            MazeNode neighborCandidate = mazeNodes[nodeToVisit.Row, nodeToVisit.Column + 1];
            if (!neighborCandidate.isVisited)
            {
                int[] loc = new int[2];
                loc[0] = nodeToVisit.Row;
                loc[1] = nodeToVisit.Column + 1;
                
                mazeNodes[loc[0], loc[1]].parentNode = new int[] { nodeToVisit.Row, nodeToVisit.Column};
                //Debug.Log("Found Neigbor: X: " + loc[0] + " Y: " + loc[1]);
                neighbors.Add(loc);
            }
        }

        //levelController.SetFloorTileColor(nodeToVisit.Row, nodeToVisit.Column, Color.blue);

        int[] thisLoc = new int[2];
        thisLoc[0] = nodeToVisit.Row;
        thisLoc[1] = nodeToVisit.Column;
        
        OnMazeNodeVisited?.Invoke(nodeToVisit);
        return neighbors;
    }

}
