using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A controller class for showing or hiding the teleportation points. 
 */
public class StandPointController : MonoBehaviour {

    public GameObject standPoint;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowStandPoint()
    {
        standPoint.SetActive(true);
    }

    public void HideStandPoint()
    {
        standPoint.SetActive(false);
    }
}
