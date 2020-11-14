using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MeshRenderer))]
public class TileInteraction : MonoBehaviour
{
    public event Action<int[]> OnButtonClick;

    MeshRenderer meshRenderer;

    int[] gridPosition;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetColor(Color color)
    {
        meshRenderer.material.color = color;
    }
    public void AddColor(Color color)
    {
        meshRenderer.material.color += color;
    }

    public void SetGridPosition(int[] pos)
    {
        gridPosition = pos;
    }

    public void ButtonPress()
    {
        OnButtonClick?.Invoke(gridPosition);
    }

}
