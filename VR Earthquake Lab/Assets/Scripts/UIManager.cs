using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * A manager class in the sceneselection scene to control the UI transitions.
 */
public class UIManager : MonoBehaviour {

    public GameObject sceneSelectionMenu;
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject trainingGoal;

    GameObject selectedBtn;

	// Use this for initialization
	void Start () {
        StateController.ResetSim();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnSettingClicked()
    {

    }

    public void OnBackClicked()
    {
        sceneSelectionMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OnSettingBackClicked()
    {
        
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OnQuit()
    {
        GameSettingManager.Instance.SaveData();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
		    Application.Quit();
        #endif
    }

    public void SetClickedBtn(GameObject gb)
    {
        this.selectedBtn = gb;
    }

    public void OnSelectBtnClicked()
    {
        if(selectedBtn == null)
        {
            return;
        }

        string btn = selectedBtn.name;
        switch (btn)
        {
            case "Training":
                //Load the scene here
                Debug.Log("Training");
                //trainingGoal.SetActive(true);
                //mainMenu.SetActive(false);
                OnSeceneSelection();
                GameSettingManager.Instance.isTrainingMode = true;
                break;
            case "Sandbox":
                //Activate the scene selection scene 
                Debug.Log("Sandbox");
                OnSeceneSelection();
                GameSettingManager.Instance.isTrainingMode = false;
                break;
            case "Settings":
                //Open Settings Menu
                Debug.Log("Settings");
                mainMenu.SetActive(false);
                settingsMenu.SetActive(true);
                GameObject panel = settingsMenu.transform.Find("Panel").gameObject;
                GameObject magDown = panel.transform.Find("Magnitude Dropdown").gameObject;
                if (magDown != null)
                {
                    CarouselController carouselController = (CarouselController)magDown.GetComponent(typeof(CarouselController));
                    carouselController.OnSettingPanelEnabled();
                }
                GameObject volDown = panel.transform.Find("Volume Dropdown").gameObject;
                if (magDown != null)
                {
                   // Debug.Log("Cara " + GameSettingManager.Instance.GameSettings.enableJumpScare);

                    CarouselController carouselController = (CarouselController)volDown.GetComponent(typeof(CarouselController));
                    carouselController.OnSettingPanelEnabled();
                }
                break;
            default:
                break;
        }
    }

    public void OnSettingValueChanged(GameObject gameObject)
    {
        string value = gameObject.GetComponentInChildren<Text>().text;
        if (gameObject.name.Equals("Volume Dropdown"))
        {
            GameSettingManager.Instance.GameSettings.Volume = value;
        }
        if(gameObject.name.Equals("Magnitude Dropdown"))
        {
            GameSettingManager.Instance.GameSettings.Magnitude = value;
        }

        Debug.Log("Magnitude: "+GameSettingManager.Instance.GameSettings.Magnitude);
        Debug.Log("Volume: " + GameSettingManager.Instance.GameSettings.Volume);
    }

    public void OnSeceneSelection()
    {
        mainMenu.SetActive(false);
        sceneSelectionMenu.SetActive(true);
        trainingGoal.SetActive(false);
    }
}

