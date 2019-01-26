using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class AI : MonoBehaviour {

    public Transform entrance;
    GameObject player;

    NavMeshAgent agent;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        SetNewDestination(player.transform);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void SetNewDestination(Transform destination)
    {
        agent.destination = destination.position;
    }
}
