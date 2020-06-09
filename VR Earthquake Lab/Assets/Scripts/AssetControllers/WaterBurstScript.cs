using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script meant to be used for the water pipe in the hospital scene
 * controller for the sound and animation 
 */ 
[RequireComponent(typeof(AudioSource))]
public class WaterBurstScript : MonoBehaviour
{
    //fixed time to play after earthquake starts
    private float timeToFall = 15f;
    private float timeRecorded;

    private ParticleSystem waterBurst;
    private AudioSource waterBurstSoundStart;
    private AudioSource waterBurstSoundLoop;
    private bool playedOnce = true;
    private int playedCounter = 0;


    // Use this for initialization
    void Start()
    {
        //Initialises objects
        waterBurst = gameObject.GetComponent<ParticleSystem>();
        var audioSources = gameObject.GetComponents<AudioSource>();
        waterBurstSoundStart = audioSources[0];
        waterBurstSoundLoop = audioSources[1];
    }

    // Update is called once per frame
    void Update()
    {
        //reset the effect timer if the simulation has not started
        if (!StateController.isSimulationStarted())
        {
            timeRecorded = 0;
            waterBurst.Stop();
            waterBurstSoundLoop.Stop();
            playedOnce = true;
        }
        else
            timeRecorded += Time.deltaTime;

        //Water burst effect plays once every 2 simulations
        if ((timeRecorded > timeToFall) && (playedOnce))
        {
            playedOnce = false;
            if (playedCounter % 2 == 0)
            {
                StartCoroutine(playWaterBurstSound());
            }
            playedCounter++;
        }
       
        
    }

    //Starts the coroutine for the water burst effects 
    IEnumerator playWaterBurstSound()
    {
        waterBurstSoundStart.Play();
        yield return new WaitForSeconds(4.5f);
        waterBurst.Play();
        yield return new WaitForSeconds(1.5f);
        waterBurstSoundLoop.Play();
    }
}
