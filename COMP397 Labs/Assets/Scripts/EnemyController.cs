using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public NavMeshAgent navMeshAgent;

    public Transform playerT;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerT = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(playerT.position);
    }
}
