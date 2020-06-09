using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * This is the class for showing the obejective of each scene. 
 */
public class ObjectiveCanvasController : MonoBehaviour {
    public string[] objectives;
    public Text objective;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SetObjective(string text)
    {
        objective.text = text;
    }

    public void UpdateText()
    {
        int stage = TrainingManager.Instance.trainingStage;
        if(stage+1<objectives.Length)
            SetObjective(objectives[stage+1]);
    }
}
