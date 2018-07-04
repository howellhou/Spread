using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attact : MonoBehaviour {
    public playerControl player;
    private Animator anim;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            transform.parent.GetComponent<Animator>().SetTrigger("attack");
        }

    }
    
    void OnTriggerStay2D(Collider2D col)
    {
        // Debug.Log("contacting");
        if (Input.GetMouseButtonDown(0))
        {
            // Debug.Log("mouse click");
            if (col.gameObject.tag == "enermy")
            {
                // Debug.Log("hitting")
                col.GetComponent<enermy>().spawn_update();
                Destroy(col.gameObject);
                //anim = col.gameObject.GetComponent<Animator>();
                //anim.SetTrigger("death");
                player.addScore();
                
            }
        }
    }

    
}
