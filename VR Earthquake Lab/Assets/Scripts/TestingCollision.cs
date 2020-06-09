using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * A test script for testing on the collision of the object. 
 */
public class TestingCollision : MonoBehaviour {

    public Text text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        text.text = collision.relativeVelocity.ToString();
    }
}
