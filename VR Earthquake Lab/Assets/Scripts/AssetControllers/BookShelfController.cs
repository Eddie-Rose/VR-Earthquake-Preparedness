using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Bookshelf controller script controlls the sound and resetting ability 
 * of the object, specifically it controlls what specific sound should be 
 * playing at the moment of collisison
 */ 
[RequireComponent(typeof(GameObject))]
public class BookShelfController : MonoBehaviour
{

    private AudioSource bookShelfCollisionSound;

    private List<Collider> hasCollidedWith;

    public GameObject floor;
    public GameObject table;

    // Use this for initialization
    void Start()
    {
        hasCollidedWith = new List<Collider>();
        var audioSource = gameObject.GetComponent<AudioSource>();
        bookShelfCollisionSound = audioSource;
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
            else if (collision.collider.Equals(table.GetComponent<Collider>()))
            {
                hasCollidedWith.Add(collision.collider);
                PlaySource(ObjectType.TABLE);
            }

        }
    }

    private void PlaySource(ObjectType objectType)
    {
        switch (objectType)
        {
            case ObjectType.TABLE:
                bookShelfCollisionSound.Play();
                break;
            case ObjectType.FLOOR:
                bookShelfCollisionSound.Play();
                break;
        }
    }

    //Resets Earthquake
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
