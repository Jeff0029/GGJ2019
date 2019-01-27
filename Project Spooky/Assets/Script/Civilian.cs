using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civilian : AI {

    public Transform m_entrance;
    public float m_spookIncreaseSpeed = 3;
    bool m_isSpooked = false;
    // Use this for initialization
    void Start () {
        base.Start();
        m_entrance = GameObject.Find("Entrance").transform;

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
    }

    public void GetSpooked()
    {
        if (!m_isSpooked)
        {
            m_agent.speed += m_spookIncreaseSpeed;
            m_agent.SetDestination(m_entrance.position);
            m_isSpooked = true;
        }
    }
}
