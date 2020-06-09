using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Upon the start of the unity application, this script analyses the
 * animation curve and attaches the movement of the earthquake with the 
 * player with some minor adjustments to add realism.
 */
public class PlayerEarthquakeMovement : MonoBehaviour {
    public static PlayerEarthquakeMovement instance;
    public AnimationCurve[] Earthquake;
    private Vector3 Amplitude;
    private float startTime;
    private Vector3 startPosition;
    private Vector3 currentPosition;

	// Use this for initialization
	private void Awake () {
        instance = this;
        Amplitude = new Vector3(1, 1, 1);
        startPosition = transform.position;
        currentPosition = transform.position;
        this.enabled = false;
	}

    // Update is called once per frame
    private void FixedUpdate () {
        gameObject.transform.Translate((calculateDisplacement()) - (currentPosition - startPosition));
        currentPosition = transform.position;
	}

    private Vector3 calculateDisplacement()
    {
        Vector3 result = new Vector3();
        float time = Time.time - startTime;
        if (time > Utilities.AnimationCurveLength(Earthquake[0]))
            StateController.StopSim();
     
        result.x = Earthquake[0].Evaluate(time-0.2f);
        result.z = Earthquake[2].Evaluate(time);
        result.y = Earthquake[1].Evaluate(time-0.2f);
        result = Vector3.Cross(result, Amplitude);

        result.x = result.x * 0.5f;
        result.z = result.z * 0.4f;

        return result;
    }

    public void StartStop(bool isEnabled)
    {
        if (isEnabled)
            startTime = Time.time;
        this.enabled = isEnabled;
    }
    public void ResetPosition()
    {
        transform.Translate(-(currentPosition - startPosition));
    }
}
