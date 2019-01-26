using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Location))]
public class AI : MonoBehaviour {

    public Transform m_entrance;
    protected GameObject m_player;

    protected Location m_location;
    NavMeshAgent m_agent;

	// Use this for initialization
	protected void Start () {
        m_agent = GetComponent<NavMeshAgent>();
        m_location = GetComponent<Location>();
        m_player = GameObject.Find("Player");
        SetNewDestination(m_player.transform);
    }
	
	// Update is called once per frame
	void Update () {

	}

    void SetNewDestination(Transform destination)
    {
        //Go to a random room
        m_agent.destination = destination.position;
    }

}
