using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * This class is used to control the behaviour of the Carousels component in the Settings panel. 
 */
public class CarouselController : MonoBehaviour {
    public List<string> options;
    public Text label;


    private int counter;
	// Use this for initialization
	void Start () {
        InitDropDowns();
    }

    private void InitDropDowns()
    {
        if (gameObject.name.Equals("Volume Dropdown"))
        {
            label.text = GameSettingManager.Instance.GameSettings.enableJumpScare == false ? "Off" : "On";
            BoolToNum(label.text);

        }
        if (gameObject.name.Equals("Magnitude Dropdown"))
        {
            label.text = GameSettingManager.Instance.GameSettings.isExihibitonMode == false ? "Off" : "On";
            BoolToNum(label.text);
        }
    }
	
	// Update is called once per frame
	void Update () {

    }

    private void BoolToNum(string value)
    {
        if (value.Equals("On"))
        {
            counter = 0;
        }
        if (value.Equals("Off"))
        {
            counter = 1;
        }
    }

    private void ValueToNum(string value)
    {
        if (value.Equals("Low"))
        {
            counter = 0;
        }
        if (value.Equals("Medium"))
        {
            counter = 1;
        }
        if (value.Equals("High"))
        {
            counter = 2;
        }
    }

    public void OnRightBtnClicked()
    {
        if (counter < 1)
        {
            counter++;
        }
        label.text = options[counter];
    }

    public void OnLeftBtnClicked()
    {
        if (counter>0)
        {
            counter--;
        }
        label.text = options[counter];
    }

    public void OnSettingPanelEnabled()
    {
        InitDropDowns();
    }

    public void UpdateSettings(GameObject gameObject)
    {
        string value = gameObject.GetComponentInChildren<Text>().text;
        if (gameObject.name.Equals("Volume Dropdown"))
        {
            GameSettingManager.Instance.GameSettings.enableJumpScare = value.Equals("On") ? true : false;
        }
        if (gameObject.name.Equals("Magnitude Dropdown"))
        {
            GameSettingManager.Instance.GameSettings.isExihibitonMode = value.Equals("On") ? true : false;
        }
    }


}
