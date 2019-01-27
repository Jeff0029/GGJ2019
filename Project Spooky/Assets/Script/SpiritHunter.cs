using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritHunter : AI {

    private float drainCooldown = 0;

    public float drainCooldownTime = 3f; // Seconds
    public float drainDamage = 5f;

    Location playerLocation;

    // Use this for initialization
    private new void Start()
    {
        base.Start();
        m_agent.speed = 6;
        playerLocation = m_player.GetComponent<Location>();
    }

    protected override Vector3 Spawn()
    {
        return GameObject.Find("Entrance").transform.position;
    }


    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        if (drainCooldown > 0)
        {
            drainCooldown -= Time.deltaTime;
        }
        else if (m_location.CurrentRoom != null && playerLocation.CurrentRoom != null && m_location.CurrentRoom.GetInstanceID() == playerLocation.CurrentRoom.GetInstanceID())
        {
            drainCooldown = drainCooldownTime;
            DrainJuice();
        }

    }

    private void DrainJuice()
    {
        Debug.Log("Draining");
        m_player.GetComponent<PlayerStats>().RemoveFromSpookJuice(drainDamage);
    }

}
