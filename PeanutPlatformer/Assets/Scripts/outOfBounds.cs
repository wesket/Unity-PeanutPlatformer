using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class outOfBounds : MonoBehaviour
{
    void onCollisionEnter(Collision col) {
        if (col.gameObject.name == "outOfBounds" || col.gameObject.tag == "outOfBounds") {
            this.transform.position = new Vector3(0, 2, 0);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("outOfBounds"))
        {
            this.transform.position = new Vector3(0, 2, 0);
        }
    }
}