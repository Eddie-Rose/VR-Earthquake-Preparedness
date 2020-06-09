using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A model class for recording the history of a camera. 
 */
[Serializable]
public class CameraHistory {

    public Vector3 position;
    public Quaternion rotation;
    public Vector3 viewDirection; 

    //constructor
    public CameraHistory(Vector3 position, Quaternion rotation, Vector3 viewDirection)
    {
        this.position = position;
        this.rotation = rotation;
        this.viewDirection = viewDirection;
    }
}
