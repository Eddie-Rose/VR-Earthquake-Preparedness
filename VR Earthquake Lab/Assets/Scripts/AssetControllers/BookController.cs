using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Bookshelf controller script controlls the sound and resetting ability  
 * of the object, specifically it controlls what specific sound should be 
 * playing at the moment of collisison
 */

[RequireComponent(typeof(GameObject))]
public class BookController : MonoBehaviour
{

    private AudioSource coatCollisionSoundWall;

    private List<Collider> hasCollidedWith;

    public GameObject floor;
    public GameObject desk;

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

            //Plays different sounds depending on the material
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

    private void PlaySource(ObjectType objectType)
    {
        switch (objectType)
        {
            case ObjectType.FLOOR:
                coatCollisionSoundWall.Play();
                break;
            case ObjectType.DESK:
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
