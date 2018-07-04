using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamecontrol : MonoBehaviour {

    private AudioSource audio;
    private float normal_timescale;
    public GameObject panel;
    public Scrollbar scroll_music;
    private bool is_pause;
    // Use this for initialization
    void Awake() {
        panel.SetActive(false);
    }

	void Start () {
        normal_timescale = Time.timeScale;
        is_pause = false;
        audio = GetComponent<AudioSource>();
        scroll_music.value = audio.volume;
	}
	
	// Update is called once per frame
	void Update () {
        if(is_pause)audio.volume = scroll_music.value;
        if (Input.GetKeyDown(KeyCode.Escape)) pause_or_continue();
	}
    public void pause_or_continue() {
        if (!is_pause)
        {
            is_pause = true;
            Time.timeScale = 0;
            panel.SetActive(true);
        }
        else {
            is_pause = false;
            Time.timeScale = normal_timescale;
            panel.SetActive(false);
        }
    }
}
