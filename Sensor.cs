using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public GameObject wallMeshObject;

    GameObject wall;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            wall = other.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (wall==other.gameObject)
        {
            Vector3 p = transform.position;
            float d = 3 - Vector3.Distance(transform.position, wall.transform.position);
            transform.position = new Vector3(p.x, d, p.z);
        }
    }
}
