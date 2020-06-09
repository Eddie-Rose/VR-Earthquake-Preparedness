using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * This is the class for controlling the start button in different modes.   
 */
public class StartButtonController : MonoBehaviour {
    public Button trainingBtn;
    public Button sandBoxBtn;


    // Use this for initialization
    void Start () {
        if (GameSettingManager.Instance.isTrainingMode)
        {
            trainingBtn.gameObject.SetActive(true);
            sandBoxBtn.gameObject.SetActive(false);
        }
        else
        {
            trainingBtn.gameObject.SetActive(false);
            sandBoxBtn.gameObject.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
