using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * This is the class to control the description text of the start button. 
 */
public class StartButtonTextController : MonoBehaviour {

    public Text text;
	public void SetButtonText()
    {
        if (GameSettingManager.Instance.isTrainingMode)
        {
            text.text = "Next";
        }
        else
        {
            text.text = "Reset";
        }
    }
}
