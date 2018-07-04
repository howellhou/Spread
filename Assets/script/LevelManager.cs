using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void load(string level) {
        Time.timeScale = 1.0f;
        Application.LoadLevel(level);
    }

    public void quit() {
        Application.Quit();
    }

}
