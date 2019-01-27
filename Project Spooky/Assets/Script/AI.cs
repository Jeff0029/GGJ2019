using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Location))]
public class AI : MonoBehaviour
{
    public float m_distanceToWaypointComplete = 0.3f;
    public float m_mintimeIdleInObjective = 1;
    public float m_maxtimeIdleInObjective = 5;

    private bool m_isMoving;

    protected GameObject m_player;
    protected Location m_location;
    protected Transform m_spawnLocation;
    protected NavMeshAgent m_agent;

    protected List<Transform> m_roamingLocations = new List<Transform>();

    private bool IsMoving
    {
        get { return m_isMoving; }
        set {
                m_agent.isStopped = !value;
                m_isMoving = value;
            }

    }

    private void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_location = GetComponent<Location>();
    }

    // Use this for initialization
    protected void Start()
    {

        m_player = GameObject.Find("Player");

        m_distanceToWaypointComplete += m_agent.radius;

        foreach (GameObject room in GameObject.FindGameObjectsWithTag("Room"))
        {
            m_roamingLocations.Add(room.transform.Find("RoomWayPoint"));
        }

        transform.position = Spawn();
        StartCoroutine("Roam");
    }

    protected virtual Vector3 Spawn()
    {
        return m_spawnLocation.position;
    }


    private IEnumerator Roam()
    {
        int locationIndex = Random.Range(0, m_roamingLocations.Count - 1);
        SetNewDestination(m_roamingLocations[locationIndex]);
        IsMoving = false;
        yield return new WaitForSeconds(Random.Range(m_mintimeIdleInObjective, m_maxtimeIdleInObjective));
        IsMoving = true;
        yield return null;
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        if (Vector3.Distance(m_agent.destination, transform.position) <= m_distanceToWaypointComplete)
        {
            StartCoroutine("Roam");
        }
    }

    void SetNewDestination(Transform destination)
    {
        //Go to a random room
        m_agent.destination = destination.position;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Room")
        {
            m_location.CurrentRoom = other.gameObject;
        }
    }

}
