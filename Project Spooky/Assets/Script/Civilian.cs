using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(AudioSource))]
public class Civilian : AI {

    public Transform m_entrance;
    AudioSource civSource;
    public AudioClip[] screams;
    public float m_spookIncreaseSpeed = 3;
    bool m_isSpooked = false;
    bool bLeftHouse = false;

    private Animator myAnim;

    // Use this for initialization
    private new void Start () {
        base.Start();
        m_entrance = GameObject.Find("Entrance").transform;
        civSource = GetComponent<AudioSource>();
        myAnim = GetComponent<Animator>();

    }

    protected override Vector3 Spawn()
    {
        int index = Random.Range(0, m_roamingLocations.Count - 1);
        return m_roamingLocations[index].position;
    }

    // Update is called once per frame
    override protected void Update () {
        if (!m_isSpooked)
        {
            base.Update();
        }
        else
        {
            m_agent.SetDestination(m_entrance.position);
            if (Vector3.Distance(m_entrance.position, transform.position) <= m_distanceToWaypointComplete)
            {
                GameObject.Find("AIFactory").GetComponent<AISpawning>().Respawn(gameObject);
            }
        }
    }

    public void GetSpooked()
    {
        if (!m_isSpooked)
        {
            myAnim.SetTrigger("spooked");

            civSource.clip = screams[Random.Range(0, screams.Length - 1)];
            civSource.Play();
            m_agent.speed += m_spookIncreaseSpeed;
            m_isSpooked = true;
        }
    }
}
