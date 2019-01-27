using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour {

    GameObject m_currentRoom;

    public GameObject CurrentRoom
    {
        get
        {
            return m_currentRoom;
        }

        set
        {
            m_currentRoom = value;
        }
    }
}
