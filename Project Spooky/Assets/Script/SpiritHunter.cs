using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritHunter : AI {

    Location playerLocation;
    // Use this for initialization
    void Start () {
        base.Start();
        playerLocation = m_player.GetComponent<Location>();
    }
	
	// Update is called once per frame
	void Update () {
        if (m_location.CurrentRoom != null && playerLocation.CurrentRoom != null && m_location.CurrentRoom.GetInstanceID() == playerLocation.CurrentRoom.GetInstanceID())
        {
            DrainJuice();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collided");
        Debug.Log(other.name);
        if (other.tag == "Room")
        {
            Debug.Log("room found");
        }
    }

    private void DrainJuice()
    {

    }

}
