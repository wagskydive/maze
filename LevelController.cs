using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static event Action<int, int> OnNewMazeSize;

    public static event Action<MazeNode[,]> OnNewMazeNodes;

    public static event Action<MazeSpawner> OnNewMazeSpawner;

    public static event Action<int[]> OnTileClicked;
    
    public static event Action <int[]> OnPlayerOnNewTile;

    public static event Action OnSetDestinationButtonPress;

    public static event Action OnStartFpsButtonPress;



    public GameObject MazePrefab;

    public GameObject TopCam;

    public GameObject FpsCam;

    GameObject currentMaze;
    MazeSpawner spawner;

    public MazeNode[,] mazeNodes;

    public int currentRows = 10;
    public int currentColumns = 10;

    private void HandleGridCreation(MazeNode node, TileInteraction tileInteraction)
    {
        mazeNodes[node.Row, node.Column] = node;
        int[] pos = { node.Row, node.Column };
        tileInteraction.SetGridPosition(pos);
        tileInteraction.OnButtonClick += OnTileClicked;
        tileInteraction.OnPlayerEnter += AssignCurrentPlayerTile;
    }

    public void CreateMazeButtonPress()
    {
        mazeNodes = new MazeNode[currentRows, currentColumns];
        CreateMaze(currentRows, currentColumns);
        OnNewMazeNodes?.Invoke(mazeNodes);
        
    }

    public void SetDestinationButtonPress()
    {
        OnSetDestinationButtonPress?.Invoke();
    }

    public void StartFpsButtonPress()
    {
        TopCam.SetActive(false);
        FpsCam.SetActive(true);
        OnStartFpsButtonPress?.Invoke();
    }
    
    void AssignCurrentPlayerTile(int[] tile)
    {
        OnPlayerOnNewTile?.Invoke(tile);
    }


    void CreateMaze(int rows, int columns)
    {
        if(currentMaze != null)
        {
            Destroy(currentMaze);
        }
        mazeNodes = new MazeNode[rows, columns];
        currentMaze = Instantiate(MazePrefab);
        spawner = currentMaze.GetComponent<MazeSpawner>();
        spawner.OnMazeCellCreated += HandleGridCreation;

        spawner.Rows = rows;
        spawner.Columns = columns;
        spawner.RandomSeed = UnityEngine.Random.Range(0, 999999);
        spawner.SpawnMaze();
        OnNewMazeSize?.Invoke(rows, columns);
        OnNewMazeSpawner?.Invoke(spawner);
    }

}
