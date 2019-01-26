using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour 
{
    BoxCollider2D m_RoomCollider;
    public GameObject m_NotVisibleRoom;

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
}
