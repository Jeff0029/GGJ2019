using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour {

	public void SpookTime()
    {
        SceneManager.LoadScene("Mansion");
    }

    public void NotSpookTime()
    {
        SceneManager.LoadScene("Main");
    }
}
