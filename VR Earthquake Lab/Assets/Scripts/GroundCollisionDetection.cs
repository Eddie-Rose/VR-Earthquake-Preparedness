using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * This script is for the use of the experiment of finding the common themes
 * of objects under the influence of an earthquake. It will disable the recording object
 * when the object being recorded falls over
 */ 
public class GroundCollisionDetection : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            this.transform.parent.GetComponent<StabilityMetric>().hasFallen = true;
            Debug.Log(this.transform.parent.gameObject.name + " has fallen");
            gameObject.SetActive(false);
           
        }
    }
}
