using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/*
 * This class is provided by the previous project.  
 */
[RequireComponent(typeof(Rigidbody))]
public class GroundController : MonoBehaviour {
    public static GroundController instance;
    public Vector3 Amplitude;
    public GroundController.State VibrationDirection;
    public AnimationCurve[] Earthquake;
    private float startTime;
    private Rigidbody rb;
    private Vector3 startPosition;
    public enum State
    {
        All, xVibration, yVibration, zVibration
    }

    private void Start(){
        instance = this;
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        this.enabled = false;
        Amplitude = GameSettingManager.Instance.GameSettings.GetEarthquakeMagnitude();
    }
    private void FixedUpdate() {
 
        rb.MovePosition(calculateDisplacement() + startPosition);
  
    }
    private Vector3 calculateDisplacement(){
        Vector3 result = new Vector3();
        float time = Time.time - startTime;
        if (time > Utilities.AnimationCurveLength(Earthquake[0]))
            StateController.StopSim();
        result.x = Earthquake[0].Evaluate(time);
        result.y = Earthquake[1].Evaluate(time);
        result.z = Earthquake[2].Evaluate(time);
        result = Vector3.Cross(result, Amplitude);

        switch (VibrationDirection)
        {
            case GroundController.State.All:
                break;
            case GroundController.State.xVibration:
                result.y = 0f;
                result.z = 0f;
                break;
            case GroundController.State.yVibration:
                result.x = 0f;
                result.z = 0f;
                break;
            case GroundController.State.zVibration:
                result.x = 0f;
                result.y = 0f;
                break;
        }

        return result;
    }
    public void StartStop(bool isEnabled) {
        if (isEnabled)
            startTime = Time.time;
        this.enabled = isEnabled;
    }
    public void ResetPosition(){
        rb.MovePosition(Vector3.zero);
    }
}
