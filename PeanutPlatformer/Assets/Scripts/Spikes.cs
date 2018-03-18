﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
	    if (other.GetComponent<SimplePlatformController>() != null)
	    {
	        other.GetComponent<SimplePlatformController>().SetHitPoint(other.GetComponent<SimplePlatformController>().GetHitPoint() - 10);
	        Destroy(gameObject);
        }

	}
}
