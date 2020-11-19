using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MeshRenderer))]
public class TileInteraction : MonoBehaviour
{
    public event Action<int[]> OnButtonClick;
    
    public event Action <int[]> OnPlayerEnter;
    
    public event Action <int[]> OnPlayerExit;
    
    public bool isVisitedByPlayer { get; private set;} 
    
    void SetVisited()
    {
      isVisitedByPlayer = true;
    }
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
    
    void OnTriggerEnter(Collider other)
    {
        If(other.GetComponent<PlayerController>())
            {
              if(!isVisitedByPlayer)
              {
                SetVisited();
              }
              OnPlayerEnter?.Invoke(gridPosition);
            }
    } 
    
    void OnTriggerExit(Collider other)
    {
        If(other.GetComponent<PlayerController>())
            {
              OnPlayerExit?.Invoke(gridPosition);
            }
    } 

}
