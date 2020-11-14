using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorManager : MonoBehaviour
{
    MazeSpawner spawner;


    private void Start()
    {
        LevelController.OnNewMazeSpawner += AssignSpawner;
        PathFinderTree.OnRouteCreated += ShowRoute;
    }

    private void ShowRoute(List<int[]> obj)
    {
        SetColors(obj, Color.green);
    }

    void AssignSpawner(MazeSpawner sp)
    {
        spawner = sp;
    }

    public void SetAllColors(Color col)
    {
        for (int i = 0; i < spawner.Rows; i++)
        {
            for (int j = 0; j < spawner.Columns; j++)
            {
                SetFloorTileColor(i, j, col);
            }

        }
    }

    public void SetColors(List<int[]> tiles, Color col)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            SetFloorTileColor(tiles[i][0], tiles[i][1], col);
        }
    }

    public void SetFloorTileColor(int row, int column, Color color)
    {
        if (spawner)
        {
            spawner.floorTiles[row, column].GetComponent<TileInteraction>().SetColor(color);
        }
    }

    public void AddFloorTileColor(int row, int column, Color color)
    {
        if (spawner)
        {
            spawner.floorTiles[row, column].GetComponent<TileInteraction>().AddColor(color);
        }
    }


}
