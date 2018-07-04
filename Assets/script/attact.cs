using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attact : MonoBehaviour
{
	public playerControl player;
	private Animator anim;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (Input.GetMouseButtonDown (0)) {
			transform.parent.GetComponent<Animator> ().SetTrigger ("attack");
		}

	}

	void OnTriggerStay2D (Collider2D col)
	{
		// Debug.Log("contacting");
		if (Input.GetMouseButtonDown (0)) {
			// Debug.Log("mouse click");
			if (col.gameObject.tag == "enermy") {
				// Debug.Log("hitting")
				col.gameObject.GetComponent<Rigidbody2D>().constraints = 
					RigidbodyConstraints2D.FreezePositionX|RigidbodyConstraints2D.FreezePositionY;
				col.isTrigger = true;
				Destroy (col.gameObject, 1f);
				//float timeLeft = 1f;
				//too many enemies are created!
				//col.GetComponent<enermy> ().spawn_update ();
				if (!col.gameObject.GetComponent<enermy> ().dead) {
					GetComponent<AudioSource> ().Play ();
					player.addScore ();
				}
				col.GetComponent<enermy> ().dead = true;

				//InvokeRepeating("Spawn", spawnDelay, spawnTime);
				anim = col.gameObject.GetComponent<Animator> ();
				anim.SetTrigger ("death");
				Debug.Log ("the enermy is " + col.gameObject.GetComponent<enermy> ().dead);

			}
		}
	}

    
}
