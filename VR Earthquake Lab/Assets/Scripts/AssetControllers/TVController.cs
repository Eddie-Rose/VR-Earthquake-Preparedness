using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Controller script for the TV, controls the logic for 
 * ressetting and playing the sound clips when it collides with
 * an object
 */
[RequireComponent(typeof(GameObject))]
public class TVController : MonoBehaviour
{

    private AudioSource tvCollisionSoundDesk;
    private AudioSource tvCollisionSoundFloor;

    private List<Collider> hasCollidedWith;

    public GameObject floor;
    public GameObject desk;

    // Use this for initialization
    void Start()
    {
        hasCollidedWith = new List<Collider>();
        var audioSource = gameObject.GetComponent<AudioSource>();
        tvCollisionSoundDesk = audioSource;
        tvCollisionSoundFloor = audioSource;
    }

    //Starts up collision sound on hit
    private void OnCollisionEnter(Collision collision)
    {
        if (!checkHasAlreadyCollided(collision))
        {
            //Plays different sound depending on the material
            if (collision.collider.Equals(floor.GetComponent<Collider>()))
            {
                hasCollidedWith.Add(collision.collider);
                PlaySource(ObjectType.FLOOR);
            }
            else if (collision.collider.Equals(desk.GetComponent<Collider>()))
            {
                hasCollidedWith.Add(collision.collider);
                PlaySource(ObjectType.DESK);
            }

        }
    }

    //Reset method
    public void EarthquakeReset()
    {
        hasCollidedWith.Clear();
    }

    private void PlaySource(ObjectType objectType)
    {
        switch (objectType)
        {
            case ObjectType.DESK:
                tvCollisionSoundDesk.Play();
                break;
        }
    }

    //Limits the collision noise to only 1 time
    private bool checkHasAlreadyCollided(Collision collision)
    {
        foreach (Collider collider in hasCollidedWith)
        {
            if (collider.Equals(collision.collider))
                return true;
        }
        return false;
    }
}
