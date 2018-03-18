using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimplePlatformController : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = true;

	public float hitPoints = 50;
	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public Transform groundCheck;
	public Text countText;
	public Text timer;
	public Text hitPointsText;
	public GameObject gameOverText;
	public float timeLeft = 60;
    private string sceneName;

	private int count = 0;
	private bool grounded = false;
	private Animator anim;
	private Rigidbody2D rb2d;

    // Use this for initialization
    void Awake () 
	{
		SetCountText ();
		SetTimerText ();
		SetHitPointText ();

	    Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{

		//Check if linecast is casting to layer ground 
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		if (Input.GetButtonDown ("Jump") && grounded)
		{
			jump = true;
		}

		timeLeft -= Time.deltaTime;
		SetTimerText ();

		if (timeLeft <= 0) 
		{
			gameOverText.SetActive (true);

		}

		if (hitPoints <= 0) 
		{
			gameOverText.SetActive (true);
		}
	}

	void FixedUpdate()
	{
		float h = Input.GetAxis ("Horizontal");

		//The animation will move as we play, mathf abs so that the speed is never negative
		anim.SetFloat("Speed", Mathf.Abs(h));

		//If we are under the speedlimit you can speed up
		if (h * rb2d.velocity.x < maxSpeed && sceneName == "Level2") 
		{
			rb2d.AddForce (Vector2.left * h * moveForce);
		}

	    if (h * rb2d.velocity.x < maxSpeed && sceneName == "Main")
	    {
	        rb2d.AddForce(Vector2.right * h * moveForce);
	    }

        //If its over maxspeed set it to maxspeed.
        if (Mathf.Abs (rb2d.velocity.x) > maxSpeed) 
		{	
			//Mathf return 1 if its a positive number, -1 if its a negative number
			rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
		}

		//Flip
		if (h > 0 && !facingRight) {
			Flip ();
		} else if (h < 0 && facingRight) 
		{
			Flip ();
		}

		//Jump
		if (jump) 
		{
			anim.SetTrigger ("Jump");
			rb2d.AddForce(new Vector2(0f, jumpForce));

			//If you dont want to doublejump
			jump = false;
		}
	}

    void Flip ()
	{
		facingRight = !facingRight;

		//Set the scale to the local scale
		Vector3 theScale = transform.localScale;

		//flip the scale
		theScale.x *= -1;

		transform.localScale = theScale;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Pick Up")) 
		{
			count++;
			SetCountText ();
		}
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Spikes"))
        {
            anim.SetTrigger("Die");
            //hitPoints = hitPoints - 10;
        }
    }

	void SetCountText ()
	{
		countText.text = "Count: " + count.ToString ();

	}

	void SetTimerText()
	{
		timer.text = "Time: " + timeLeft.ToString();

		if (timeLeft <= 0) 
		{
			timer.text = "Time's up";
		}
	}

	void SetHitPointText ()
	{
		hitPointsText.text = "Hitpoints: " + hitPoints.ToString ();

		if (hitPoints <= 0) 
		{
			hitPointsText.text = "Dead";
		}
	}

	public void SetHitPoint(float h)
	{
		SetHitPointText ();
		hitPoints = h;
	}

	public float GetHitPoint()
	{
		return hitPoints;
	}

}