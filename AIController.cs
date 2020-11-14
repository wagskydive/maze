using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIController : MonoBehaviour
{
    NavMeshAgent agent;

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        LevelController.OnNewMazeSize += LevelReset;
    }

    void LevelReset(int rows, int columns)
    {
        transform.position = new Vector3(0, 0, 0);
        Vector3 destination = new Vector3((columns-1) * 4, 1, (rows-1) * 4);
        target.transform.position = destination;
        agent.SetDestination(destination);
    }


}
