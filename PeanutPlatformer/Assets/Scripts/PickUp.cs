using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

    public AudioClip pickUpSound;
    public float pickUpVolume = 1f;
    public Renderer rend;
    public CircleCollider2D circleCol;

    private AudioSource source;

	// Use this for initialization
	void Awake () {
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

    }

	//Destroy the gameobject you collide with
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
            source.PlayOneShot(pickUpSound, pickUpVolume);

            rend = GetComponent<MeshRenderer>();
            rend.enabled = false;
            circleCol = GetComponent<CircleCollider2D>();
            circleCol.enabled = false;
            //Destroy (gameObject);
		}
	}
}
