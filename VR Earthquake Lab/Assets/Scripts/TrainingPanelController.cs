using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * The controller class for showing whether the training has been completed or not. 
 */
public class TrainingPanelController : MonoBehaviour {

    public Text text;
    public Button button;
    public GameObject backgroundImage;
    public GameObject backgroundTextImage;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        text.enabled = GameSettingManager.Instance.GameSettings.isExihibitonMode;
        button.enabled = !GameSettingManager.Instance.GameSettings.isExihibitonMode;
        UpdateImageBackground();

    }

    void UpdateImageBackground()
    {
        if (GameSettingManager.Instance.GameSettings.isExihibitonMode)
        {
            backgroundImage.SetActive(true);
            backgroundTextImage.SetActive(true);
            gameObject.GetComponent<GvrPointerGraphicRaycaster>().enabled = false;
        }
        else
        {
            backgroundImage.SetActive(false);
            backgroundTextImage.SetActive(false);
            gameObject.GetComponent<GvrPointerGraphicRaycaster>().enabled = true;
        }
    }
}
