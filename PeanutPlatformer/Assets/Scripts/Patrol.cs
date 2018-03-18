using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour {


	public float speed;
	public Transform groundDetection;
	public float distance = 2f;

	private bool movingRight = true;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Translate (Vector2.right * speed * Time.deltaTime);


		RaycastHit2D groundInfo = Physics2D.Raycast (groundDetection.position, Vector2.down, distance);

        //If the ray has not collided move to the other side
	    if (groundInfo.collider == false)  
		{

			if (movingRight == true) {
				//rotate charachter 180 degrees
				transform.eulerAngles = new Vector3 (0, -180, 0);
				movingRight = false;
			} else
            {
				transform.eulerAngles = new Vector3 (0, 0, 0);
				movingRight = true;
			}
		}
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<SimplePlatformController>() != null)
        {
            other.GetComponent<SimplePlatformController>().SetHitPoint(other.GetComponent<SimplePlatformController>().GetHitPoint() - 10);
            Destroy(gameObject);
        }

        /*if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }*/

    }
}
