using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enermy : MonoBehaviour {

    private GameObject player;
    public Rigidbody2D bullet;
    public float speed = 2f;
    public int shootingRange = 5;
    private float timeLastFire = 0;
    private bool faceLeft = true;
    public int created_site;
	public bool dead = false;
	public float timeLeft = 1f;
	public bool spawned = false;
	public GameObject NPCZombie;

	private Animator anim;
    private Spawner spawner;

    public void create_site(int site) {
        created_site = site;
    }

	public void spawn_update() {//to spawn a new enemy if needed

		if (dead) {
			spawner.Spawn (created_site);
			//Debug.Log ("blablabla");
		}
		//InvokeRepeating("Spawn", spawnDelay, spawnTime);
        Debug.Log("destroy site:" + created_site.ToString());
    }

    
	// Use this for initialization
	void Start () {
        GameObject spawn = GameObject.FindWithTag("spawner");
        spawner = spawn.GetComponent<Spawner>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectsWithTag("player")[0];

    }

	void Update()
	{
		if (dead && !spawned) {
			GetComponent<AudioSource> ().Play ();
			spawner.Spawn (created_site);
			spawned = true;
			Invoke ("SpawnNPCZombie", 0.7f);
			Debug.Log ("I did it");
		}
		/*if (spawned) {
			timeLeft = 1f;
			spawned = tr;
		}*/
	}
	void SpawnNPCZombie () 
	{
		Instantiate (NPCZombie, transform.position, transform.rotation);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector2 playerPos = player.transform.position;

        if(Mathf.Abs(playerPos.y - this.transform.position.y) <= 0.25f && Mathf.Abs(playerPos.x - this.transform.position.x) <= 20)
        {
            Debug.Log("in if");
            moveToPlayer();
        }
        else
        {
            //Debug.Log("set false");
            anim.SetBool("shoot", false);
        }

	}

    void moveToPlayer()
    {
        float distance = player.transform.position.x - this.transform.position.x;
        if (distance > 0 && faceLeft)
        {
            Vector3 enermyScale = transform.localScale;
            enermyScale.x *= -1;
            transform.localScale = enermyScale;
            faceLeft = false;
        }
        else if (distance < 0 && !faceLeft)
        {
            Vector3 enermyScale = transform.localScale;
            enermyScale.x *= -1;
            transform.localScale = enermyScale;
            faceLeft = true;
        }
        if (distance >= shootingRange || distance <= -shootingRange)
        {
            anim.SetBool("shoot", false);
            if (faceLeft)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime,
                    transform.position.y, transform.position.z);
            } 
            else
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime,
                    transform.position.y, transform.position.z);
            }
        }
        else
        {
            anim.SetBool("shoot", true);
            timeLastFire += Time.deltaTime;
            if (timeLastFire > 0.5)
            {

                Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
                if (faceLeft)
                {
                    bulletInstance.velocity = new Vector2(-15f, 0);
                }
                else
                {
                    bulletInstance.velocity = new Vector2(15f, 0);
                }
                timeLastFire = 0;
            }
        }
    }
}
