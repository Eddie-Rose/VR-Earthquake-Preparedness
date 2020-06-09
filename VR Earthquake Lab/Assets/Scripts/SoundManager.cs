using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The controller class for controlling the sound effect
 */
public class SoundManager : MonoBehaviour {
    public AudioSource efxSource;                    //Drag a reference to the audio source which will play the sound effects.
    public static SoundManager instance = null;        //Allows other scripts to call functions from SoundManager.
    private bool isPlayingMusic;
    private float startTime;


    private static SoundManager _instance;

    public static SoundManager Instance { get { return _instance; } }
    public AnimationCurve[] Earthquake;

    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }

    public static SoundManager getInstance()
    {
        return instance;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (StateController.isSimulationStarted())
        {
            if (!isPlayingMusic)
            {
                
                float volume = GameSettingManager.Instance.GameSettings.GetVolumeValue();

                efxSource.volume = volume;
                efxSource.Play();
                isPlayingMusic = true;
                startTime = Time.time;
            }

            float time = Time.time - startTime;
            float volumeadjuster = 1f;
            if (time < Utilities.AnimationCurveLength(Earthquake[0]))
            {
                volumeadjuster = Mathf.Abs(Earthquake[0].Evaluate(time - 0.2f));
                if (volumeadjuster > 0.6f)
                    volumeadjuster = 0.6f;
                efxSource.volume = volumeadjuster + 0.9f;
            }
        }
        else
        {
            efxSource.Stop();
            isPlayingMusic = false;
        }
	}



}
