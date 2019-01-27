using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour 
{
    BoxCollider2D m_RoomCollider;
    public GameObject m_NotVisibleRoom;

    public int m_pointsPerSpook = 25;
    public float m_juicePerSpook = 15;

	// Use this for initialization
	void Start () 
    {
        m_RoomCollider = this.gameObject.GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    public void SetRoomVisible(bool bIsVisible)
    {
        m_NotVisibleRoom.SetActive(bIsVisible);
    }

    public void SpookCivilians()
    {
        int numColliders = 20;
        Collider2D[] colliders = new Collider2D[numColliders];
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.layerMask = Physics2D.GetLayerCollisionMask(LayerMask.NameToLayer("AI"));
        contactFilter.useLayerMask =true;
        contactFilter.useTriggers = true;
        m_RoomCollider.OverlapCollider(contactFilter, colliders);


        GameObject player = GameObject.Find("Player");
        int spookedCount = 0;
        foreach (Collider2D collided in colliders)
        {
            if (collided != null && collided.tag == "Civilian") 
            {
                spookedCount++;
                collided.GetComponent<Civilian>().GetSpooked();
            }
        }

        player.GetComponent<PlayerStats>().AddToSpookJuice(spookedCount*m_juicePerSpook);
        player.GetComponent<PlayerStats>().AddToScore(spookedCount*m_pointsPerSpook);


    }
}
