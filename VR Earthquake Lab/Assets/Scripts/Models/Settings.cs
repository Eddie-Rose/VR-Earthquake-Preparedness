using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The model class that handles the settings for the VR application. 
 */
[Serializable]
public class Settings {

    public Settings()
    {
        this.Volume = "Low";
        this.Magnitude = "Low";
        this.isExihibitonMode = false;
        this.enableJumpScare = false;
        
    }

    public string Volume;
    public string Magnitude;
    public bool isExihibitonMode;
    public bool enableJumpScare;
    public int UserID;

    public void IncreaseID()
    {
        UserID++;
    }
    public float GetVolumeValue()
    {
        return 1f;
    }

    public Vector3 GetEarthquakeMagnitude()
    {
        return new Vector3(2f,2f,2f);
    }

}
