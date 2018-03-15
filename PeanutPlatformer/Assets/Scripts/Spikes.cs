using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		other.GetComponent<SimplePlatformController>().SetHitPoint(other.GetComponent<SimplePlatformController>().GetHitPoint() - 25);
		Destroy (gameObject);
	}
}
