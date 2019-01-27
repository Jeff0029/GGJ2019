using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawning : MonoBehaviour {

    public int m_initialCivilianSpawn = 4;
    public int m_initialSpiritHunterSpawn = 1;
    public int m_numberOfSpookedToIncreaseSpiritHunter = 6;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i <= m_initialCivilianSpawn; i++)
        {
            Instantiate(Resources.Load("Prefab/Civilian"));
        }

        for (int i = 0; i <= m_initialSpiritHunterSpawn; i++)
        {
            Instantiate(Resources.Load("Prefab/SpiritHunter"));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
