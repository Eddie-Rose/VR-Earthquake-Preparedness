using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Clock controller script controlls when specific sounds should 
 * be played and controlls when the physics (gravity) should be 
 * enabled during the simulation of an earthquake
 */ 
[RequireComponent(typeof(GameObject))]
public class ClockController : MonoBehaviour {

    public float timeToFall;
    private float timeRecorded;
    private bool isCollided;
    private AudioSource clockCollisionSoundFloor;
    private AudioSource clockCollisionSoundTv;
    private AudioSource clockCollisionSoundDesk;

    private Vector3 originalPosition;
    private Vector3 originalRotation;

    private List<Collider> hasCollidedWith;

    public GameObject floor;
    public GameObject tv;
    public GameObject desk;

    void Awake()
    {
        originalPosition = this.gameObject.transform.localPosition;
        originalRotation = this.gameObject.transform.localEulerAngles;
    }

    // Use this for initialization
    void Start () {
        hasCollidedWith = new List<Collider>();
        var audioSources = gameObject.GetComponents<AudioSource>();
        clockCollisionSoundFloor = audioSources[0];
        clockCollisionSoundTv = audioSources[1];
        clockCollisionSoundDesk = audioSources[2];
	}
	
	// Update is called once per frame
	void Update () {
        if (!StateController.isSimulationStarted())
            timeRecorded = 0;

        //Manually control when clock falls
        if (timeRecorded > timeToFall)
            gameObject.GetComponent<Rigidbody>().useGravity = true;

        timeRecorded += Time.deltaTime;
	}

    public void resetPosition()
    {
        hasCollidedWith.Clear();
        StartCoroutine(Resetter());
    }

    IEnumerator Resetter()
    {
        yield return new WaitForSeconds(0.1f);

        this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        this.GetComponent<Rigidbody>().detectCollisions = false;
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.GetComponent<Rigidbody>().useGravity = false;

        this.gameObject.transform.localPosition = originalPosition;
        this.gameObject.transform.localEulerAngles = originalRotation;

        this.GetComponent<Rigidbody>().detectCollisions = true;
        this.GetComponent<Rigidbody>().isKinematic = false;
    }
    
    //Starts up collision sound on hit
    private void OnCollisionEnter(Collision collision)
    {
        if (!checkHasAlreadyCollided(collision))
        {

            if (collision.collider.Equals(floor.GetComponent<Collider>()))
            {
                hasCollidedWith.Add(collision.collider);
                PlaySource(ObjectType.FLOOR);
            }
            else if (collision.collider.Equals(tv.GetComponent<Collider>()))
            {
                hasCollidedWith.Add(collision.collider);
                PlaySource(ObjectType.TV);

            }
            else if (collision.collider.Equals(desk.GetComponent<Collider>()))
            {
                hasCollidedWith.Add(collision.collider);
                PlaySource(ObjectType.DESK);
            }

        }
    }
   
    //Plays sound depending on the object type the clock collides with
    private void PlaySource(ObjectType objectType)
    {
        switch (objectType)
        {
            case ObjectType.FLOOR:
                clockCollisionSoundFloor.Play();
                break;
            case ObjectType.TV:
                clockCollisionSoundTv.Play();
                break;
            case ObjectType.DESK:
                clockCollisionSoundDesk.Play();
                break;    
        }
    }

    //Limits the collision noise to only 1 time
    private bool checkHasAlreadyCollided(Collision collision)
    {
        foreach(Collider collider in hasCollidedWith)
        {
            if (collider.Equals(collision.collider))
                return true;
        }
        return false;
    }
}
