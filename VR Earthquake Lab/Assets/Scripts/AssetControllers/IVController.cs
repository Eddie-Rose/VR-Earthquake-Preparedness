using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * IV controller script which controls the sound and resetting logic 
 * relating to the IV during an earthquake simulation
 */ 
[RequireComponent(typeof(GameObject))]
public class IVController : MonoBehaviour
{

    private AudioSource IVCollisionSound;

    private List<Collider> hasCollidedWith;

    // Use this for initialization
    void Start()
    {
        hasCollidedWith = new List<Collider>();
        var audioSource = gameObject.GetComponent<AudioSource>();
        IVCollisionSound = audioSource;
    }

    //Starts up collision sound on hit
    private void OnCollisionEnter(Collision collision)
    {
        //Plays different sound depending on the material
        if (!checkHasAlreadyCollided(collision))
        {
            hasCollidedWith.Add(collision.collider);
            IVCollisionSound.Play();
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
