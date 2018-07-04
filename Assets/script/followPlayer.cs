using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour {
    public float offset_x = 0.5f;
    public float offset_y = 0.5f;
    public GameObject player;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 pos = player.transform.position;
        int neg;
        if (player.GetComponent<playerControl>().facingRight)
        {
            neg = 1;
        }
        else
        {
            neg = -1;
        }
        pos.x = pos.x + neg * offset_x;
        pos.y = pos.y + offset_y;
        transform.position = pos;
	}
}
