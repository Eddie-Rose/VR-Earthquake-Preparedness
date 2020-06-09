using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Provided by the previous project. 
 */
public class UIScaler : MonoBehaviour {
    public float ScaleAt10m = 0.002f;
    public bool Scale = true;
    private Transform mainCamera;
	// Use this for initialization
	void Start () {
        mainCamera = Camera.main.transform;
        transform.localScale = Vector3.one * ScaleAt10m;
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(2*transform.position-mainCamera.position);//Look Away From. UI are flipped.
        if (Scale){
            float scale = ScaleAt10m / 10.0f * Vector3.Distance(transform.position, mainCamera.position);
            transform.localScale = Vector3.one * scale;
        }
	}
}
