using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
	
	public int y;

	// Update is called once per frame
	void Update () 
	{
		transform.Rotate (new Vector3 (0, y, 0) * Time.deltaTime);
	}
}
