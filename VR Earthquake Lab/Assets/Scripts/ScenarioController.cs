using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * A controller class for enabling or disabling the object that appears in training/playground mode. 
 */
public class ScenarioController : MonoBehaviour {

    public Button button;
    public Text text;
    public int sceneIndex = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameSettingManager.Instance.isTrainingMode)
        {
            
            if (GameSettingManager.Instance.isTrainingCompleted[sceneIndex])
            {
                button.enabled = false;
                text.enabled = true;
                text.gameObject.SetActive(true);
                gameObject.GetComponent<GvrPointerGraphicRaycaster>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<GvrPointerGraphicRaycaster>().enabled = true;
                button.enabled = true;
                text.enabled = false;
                text.gameObject.SetActive(false);
            }
        }
        else
        {
            gameObject.GetComponent<GvrPointerGraphicRaycaster>().enabled = true;
            button.enabled = true;
            text.enabled = false;
            text.gameObject.SetActive(false);
        }
	}
}
