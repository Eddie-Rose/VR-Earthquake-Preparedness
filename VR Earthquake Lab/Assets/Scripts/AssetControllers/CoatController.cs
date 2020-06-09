using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Coat controller script controls the sound and resetting logic of the 
 * coat object
 */ 
public class CoatController : MonoBehaviour {

    AudioSource standHitSound;
    bool isCollided;
    public GameObject wall;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        standHitSound = gameObject.GetComponent<AudioSource>();
	}

    //Starts up collision sound on hit
    void OnCollisionEnter(Collision collision)
    {
        if (wall != null)
        {
            //Plays different sound depending on the material
            if (collision.collider.Equals(wall.GetComponent<Collider>()))
            {
                if (!isCollided)
                {
                    PlaySource();
                    isCollided = true;
                }
               
            }
        }

    }

    //Reset method
    public void doReset()
    {
        isCollided = false;
    }

    void PlaySource()
    {
        standHitSound.Play();
    }
}
