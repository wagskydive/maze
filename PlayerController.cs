using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController FirstPersonController;

    public event Action OnFireButtonPress;

    Rigidbody rbody;
    
    int[] currentTile;
    
    List<int[]> currentRoute;

    private void Start()
    {
        rbody = GetComponent<Rigidbody>();
        LevelController.OnNewMazeSize += ResetPlayer;
        
    }
    
    void SetCurrentRoute(List<int[]> route)
    {
        currentRoute = route;
    }
    
    void AssignCurrentTile(int[] tile)
    {
        currentTile = tile;
        //if(currentRoute)
    }

    void ResetPlayer(int rows, int columns)
    {
        transform.position = new Vector3(0, 10, 0);
        FirstPersonController.enabled = true;
        rbody.isKinematic = false;
        rbody.velocity = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && FirstPersonController.enabled)
        {
            
            FirstPersonController.enabled = false;
            
            rbody.isKinematic = true;
            rbody.velocity = Vector3.zero;
        }

        if (Input.GetMouseButtonDown(1) && !FirstPersonController.enabled)
        {
            FirstPersonController.enabled = true;
            FirstPersonController.mouseLook.lockCursor = true;
            rbody.isKinematic = false;
            rbody.velocity = Vector3.zero;
        }


        if (Input.GetMouseButtonDown(0) && FirstPersonController.enabled)
        {
            OnFireButtonPress?.Invoke();
        }

    }



}
