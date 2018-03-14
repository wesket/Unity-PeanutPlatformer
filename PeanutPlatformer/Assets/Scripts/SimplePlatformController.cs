﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlatformController : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = true;

	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public Transform groundCheck;

	private bool grounded = false;
	private Animator anim;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Awake () 
	{
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
	}

	void FixedUpdate()
	{
		float h = Input.GetAxis ("Horizontal");

		//The animation will move as we play, mathf abs so that the speed is never negative
		anim.SetFloat("Speed", Mathf.Abs(h));

		//If we are under the speedlimit you can speed up
		if (h * rb2d.velocity.x < maxSpeed) 
		{
			rb2d.AddForce (Vector2.right * h * moveForce);
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
}