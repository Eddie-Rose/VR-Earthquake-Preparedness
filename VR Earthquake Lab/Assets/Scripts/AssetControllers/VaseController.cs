using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Controller for the vase, controls the animation, colliders and visibility of the 
 * vase especially the breaking logic of the vase when it collides with the floor
 */
public class VaseController : MonoBehaviour {
    Vector3 initialPosition;
    float timeToFall;
    Quaternion initialRotation;
    bool touchedGround = false;
    AudioSource brokenGlassSound;

    public GameObject floor;


    // Use this for initialization
    void Start () {
        initialRotation = gameObject.transform.rotation;
        brokenGlassSound = gameObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

        //Reset timer if the simulation has not started
        if (!StateController.isSimulationStarted())
        {
            timeToFall = 0;
        }
        if (timeToFall > 20)
        {
            if(!touchedGround)
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
                
        }
        timeToFall += Time.deltaTime;
        
	}

    //On collision with the ground, change vase into a broken vase
    void OnCollisionEnter(Collision collision)
    {
        if (floor != null)
        {
            if (collision.collider.Equals(floor.GetComponent<Collider>()))
            {
                touchedGround = true;
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.GetComponent<BoxCollider>().enabled = true;
                gameObject.GetComponent<CapsuleCollider>().enabled = false;
                gameObject.transform.rotation = initialRotation;
                gameObject.GetComponent<Rigidbody>().freezeRotation = true;

                PlaySource();
            }
        }
       
    }

    void PlaySource()
    {
        brokenGlassSound.Play();
    }

    //Resets the mesh and colliders to its original non-broken state when reset called
    public void Reset()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<Rigidbody>().freezeRotation = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        touchedGround = false;

        if (gameObject.transform.childCount > 0)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
