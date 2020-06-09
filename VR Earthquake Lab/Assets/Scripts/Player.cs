using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The player controller class to control the player's behaviour in the earthquake.
 */
public class Player : MonoBehaviour {
    public Transform LeftHand;//Not Used
    public Transform RightHand;
    public AnimationCurve[] Earthquake;

    private Placeable placeable;
    private static Player instance;
    private static int height = 2;

    private Vector3 Amplitude;
    private float startTime;
    private Vector3 startPosition;
    private Vector3 currentPosition;
    private bool isEnabled;

    public static Player GetInstance(){
        return instance;
    }
    private void Awake(){
        instance = this;
        Amplitude = new Vector3(2, 2, 2);
        startPosition = transform.position;
        currentPosition = transform.position;
        isEnabled = false;
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (isEnabled)
        {
            gameObject.transform.Translate((calculateDisplacement()) - (currentPosition - startPosition));
            currentPosition = transform.position;
        }
    }
    private Vector3 calculateDisplacement()
    {
        Vector3 result = new Vector3();
        float time = Time.time - startTime;
        if (time > Utilities.AnimationCurveLength(Earthquake[0]))
            StateController.StopSim();

        result.x = Earthquake[0].Evaluate(time - 0.2f);
        result.y = Earthquake[1].Evaluate(time);
        result.z = Earthquake[2].Evaluate(time - 0.2f);
        result = Vector3.Cross(result, Amplitude);

        result.x = result.x * 0.8f;
        result.z = result.z * 0.7f;

        return result;
    }
    public Placeable GetHeldObject() {
        return placeable;
    }
    public bool SetHeldObject(Placeable heldObject){
        if (placeable != null && heldObject != null)
            return false; // if an item is being picked up while alredy holding obj.
        placeable = heldObject;
        return true;
    }

    public void SetPlayerPosition(Vector3 position)
    {
        float h = this.gameObject.transform.position.y;
        position.y = h;
        this.gameObject.transform.position = position;
        //currentPosition
    }
    public void StartStop(bool editorIsEnabled)
    {
        if (editorIsEnabled)
            currentPosition = gameObject.transform.position;
            startPosition = gameObject.transform.position;
            startTime = Time.time;
        isEnabled = editorIsEnabled;

    }
    public void ResetPosition()
    {
        transform.Translate(-(currentPosition - startPosition));
    }
}
