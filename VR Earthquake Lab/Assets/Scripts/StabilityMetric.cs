using System;
using System.Collections.Generic;
using System.IO;
using Unity.Collections;
using UnityEngine;


//Time for the object to fall
//The distance an object has moved from its original place(magnitude of the distance centre has moved?)
//Vibrations of an object during an Earthquake
/*
 * A script attached to the target obejct for measuring the behaviour of the object
 */
public class StabilityMetric : MonoBehaviour {

    private Vector3 initialPosition;
    private Vector3 initialDistance;
    private bool recording;
    [ReadOnly] public bool hasFallen;
    private float time;
    private float timeToFall;
    private float displacementOfObject;
    private List<ObjectRecord> records = new List<ObjectRecord>();

    public GameObject floor; 

    //if spherical cannot determine when it has fallen over
    public bool isSpherical;

    // Use this for initialization
    void Start () {
        initialPosition = gameObject.transform.rotation.eulerAngles;
        initialDistance = gameObject.transform.position - floor.transform.position;
        recording = false;
        hasFallen = false;
        time = 0f;

        
        //no need to record if the object is spherical
        if (isSpherical)
        {
            hasFallen = true;
        }

	}
	
	// Update is called once per frame
	void Update () {
		if (recording)
        {
            time += Time.deltaTime;

            if (!hasFallen)
            {
                timeToFall = time;

                //Record data of vibrations (record both x and z axis of vibrations so record displacement until object has fallen)
                float xDisplacement = NormaliseAngle(gameObject.transform.localEulerAngles.x - initialPosition.x);
                float yDisplacement = NormaliseAngle(gameObject.transform.localEulerAngles.y - initialPosition.y);
                float zDisplacement = NormaliseAngle(gameObject.transform.localEulerAngles.z - initialPosition.z);


                Vector3 currentDistance = gameObject.transform.position - floor.transform.position;
                records.Add(new ObjectRecord(time, xDisplacement, yDisplacement, zDisplacement, currentDistance - initialDistance));
            }

        }
	}
    void OnEnable()
    {
        StateController.SimulationStarted += RecordStability;
        StateController.SimulationStopped += StopRecordStability;
        records.Clear();
    }
    void OnDisable()
    {
        StateController.SimulationStarted -= RecordStability;
        StateController.SimulationStopped -= StopRecordStability;
    }

    private void RecordStability()
    {
        Debug.Log("Start Recording");
        recording = true;
    }

    private float NormaliseAngle(float angle)
    {
        if (angle > 180)
        {
            return angle - 360f;
        }
        else if (angle < -180)
        {
            return angle + 360f;
        }
        else
        {
            return angle;
        }
    }


    private void StopRecordStability()
    {
        Debug.Log("Stop Recording");
        recording = false;

    #if UNITY_EDITOR
        RecordMetrics();
    #endif
    }

    //Used to display or write to a file
    private void RecordMetrics()
    {
        System.IO.Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Records/" + gameObject.name);
        string filePath = Directory.GetCurrentDirectory()+"/Records/"+gameObject.name + "/"+ gameObject.name + ".csv";

        using (var writer = new StreamWriter(@filePath))
        {
            foreach (var pair in records)
            {
                writer.WriteLine("{0},{1},{2},{3},{4}", pair.TimeToFall, pair.XRotationalDisplacement, pair.YRotationalDisplacement, pair.ZRotationalDisplacement,pair.getDistance());
            }
        }
        String text;
        switch (hasFallen)
        {
            case true:
                text = "Time for object to fall is: " + timeToFall;
                break;

            case false:
                text = "Object did not fall over";
                break;

            default:
                text = "null";
                break;
        }

        System.IO.File.WriteAllText(Directory.GetCurrentDirectory() + "/Records/" + gameObject.name + "/" + gameObject.name + ".txt", text);
    }

   
}


//The object to indicate what needs to be written to the csv file
class ObjectRecord
{
    public float TimeToFall { get; set; }
    public float XRotationalDisplacement { get; set; }
    public float YRotationalDisplacement { get; set; }
    public float ZRotationalDisplacement { get; set; }
    public Vector3 displacement; 

    public ObjectRecord(float timeToFall, float xDisplacement, float yDisplacement, float zDisplacement,Vector3 displacementVector)
    {
        TimeToFall = timeToFall;
        XRotationalDisplacement = xDisplacement;
        YRotationalDisplacement = yDisplacement;
        ZRotationalDisplacement = zDisplacement;
        displacement = displacementVector;
    }

    public float getDistance()
    {
        return Mathf.Sqrt(displacement.x * displacement.x + displacement.y * displacement.y + displacement.z * displacement.z);
    }
}
