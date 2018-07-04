using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class playerControl : MonoBehaviour {

    public bool facingRight = false;
    public float speed = 2.0f;
    public float moveForce = 0f;
    public float maxSpeed = 2f;
    public bool jump = false;
    private bool grounded = false;
    public AudioClip[] jumpClips;
    public float jumpForce = 100f;
    private Transform groundCheck;
    private int MAXhealth = 50;
    private int health = 50;
    public GameObject targetCheck;
    public SimpleHealthBar healthBar;
    public Text record;
    public int totalScore = 0;

    public Text gameover;
    public Text back_to_menu;
    private Button back;
    private Animator anim;
   
	// Use this for initialization
	void Start () {
        groundCheck = transform.Find("groundCheck");
        anim = GetComponent<Animator>();
        gameover.text = "";
        back_to_menu.text = "";
        back = back_to_menu.GetComponent<Button>();
        back.interactable = false;
    }
	
    public void addScore()
    {
        totalScore++;
        record.text = "Transmission: " + totalScore.ToString();
    }

    void death()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        Destroy(gameObject,0.7f);
        anim.SetTrigger("death");
        back.interactable = true;
        gameover.text = "Game Over!";
        back_to_menu.text = "Back To Menu";

    }
    public void takeDamage()
    {
        health--;
        healthBar.UpdateBar(health, MAXhealth);
        if (health <= 0)
        {
            death();
			Debug.Log ("blablabla");
        }
    }
	// Update is called once per frame
	void Update () {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        // Debug.Log(grounded);
        // If the jump button is pressed and the player is grounded then the player should jump.
        if (Input.GetButtonDown("Jump") && grounded)
            jump = true;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
            // ... add a force to the player.
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);

        // If the player's horizontal velocity is greater than the maxSpeed...
        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
            // ... set the player's velocity to the maxSpeed in the x axis.
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if (h > 0 && !facingRight)
            // ... flip the player.
            Flip();
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (h < 0 && facingRight)
            // ... flip the player.
            Flip();

        if (jump)
        {
            // Set the Jump animator trigger parameter.
            // anim.SetTrigger("Jump");

            // Play a random jump audio clip.
            int i = Random.Range(0, jumpClips.Length);
            // AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

            // Add a vertical force to the player.
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));

            // Make sure the player can't jump again until the jump conditions from Update are satisfied.
            jump = false;
        }
    }



    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
