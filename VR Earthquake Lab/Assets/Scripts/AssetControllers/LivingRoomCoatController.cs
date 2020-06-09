using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Controller for the coat object, controls the sound and resetting 
 * logic of the object.
 */
[RequireComponent(typeof(GameObject))]
public class LivingRoomCoatController : MonoBehaviour
{

    private AudioSource coatCollisionSoundWall;

    private List<Collider> hasCollidedWith;

    public GameObject wall1;
    public GameObject wall2;
    public GameObject wall3;


    // Use this for initialization
    void Start()
    {
        hasCollidedWith = new List<Collider>();
        var audioSource = gameObject.GetComponent<AudioSource>();
        coatCollisionSoundWall = audioSource;
    }

    //Starts up collision sound on hit
    private void OnCollisionEnter(Collision collision)
    {
        if (!checkHasAlreadyCollided(collision))
        {
            //Plays different sound depending on the material
            if (collision.collider.Equals(wall1.GetComponent<Collider>()))
            {
                hasCollidedWith.Add(collision.collider);
                PlaySource(ObjectType.WALL);
            }
            else if (collision.collider.Equals(wall2.GetComponent<Collider>()))
            {
                hasCollidedWith.Add(collision.collider);
                PlaySource(ObjectType.WALL);
            }
            else if (collision.collider.Equals(wall3.GetComponent<Collider>()))
            {
                hasCollidedWith.Add(collision.collider);
                PlaySource(ObjectType.WALL);
            }

        }
    }

    private void PlaySource(ObjectType objectType)
    {
        switch (objectType)
        {
            case ObjectType.WALL:
                coatCollisionSoundWall.Play();
                break;
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
