using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeNode
{
    public MazeCell cellInfo { get; private set; }

    public int Column { get; private set; }
    public int Row{ get; private set; }

    public int[] parentNode;

    public bool isVisited = false;

    public MazeNode(MazeCell nodeCellInfo, int row, int column)
    {
        cellInfo = nodeCellInfo;
        Column = column;
        Row = row;
    }



}
