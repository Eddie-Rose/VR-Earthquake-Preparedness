using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Controller script controlling the specific sound and resetting logic 
 * of the hospital bed object
 */ 
[RequireComponent(typeof(GameObject))]
public class HospitalBedController : MonoBehaviour
{

    private AudioSource bedCollisionSound;

    private List<Collider> hasCollidedWith;

    // Use this for initialization
    void Start()
    {
        hasCollidedWith = new List<Collider>();
        var audioSource = gameObject.GetComponent<AudioSource>();
        bedCollisionSound = audioSource;
    }

    //Starts up collision sound on hit
    private void OnCollisionEnter(Collision collision)
    {
        if (!checkHasAlreadyCollided(collision))
        {
            if (!(collision.collider.gameObject.name == "Pillow"))
            {
                hasCollidedWith.Add(collision.collider);
                bedCollisionSound.Play();
            }
        }
    }

    //Reset method
    public void EarthquakeReset()
    {
        hasCollidedWith.Clear();
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
