using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * The controller class to control the control panel in each scenario(office,living room or hospital).
 */
public class InGameUICController : MonoBehaviour {
    public Text rootItemText;
    public GameObject canvas;

	public void StartSimulation()
    {
        StateController.Next();
    }

    public void OnExitSimulation()
    {
        TrainingManager.Instance.DestroyTrainingManager();

        SceneManager.LoadScene("SceneSelection");
    }


    public void ResetScene()
    {
        SceneManager.LoadScene(gameObject.scene.name);
    }

    public void SetButtonText(string text)
    {
        if(rootItemText!=null)
            rootItemText.text = text;
    }

    public void HideCanvas()
    {
        canvas.SetActive(false);
    }

    public void ShowCanvas()
    {
        canvas.SetActive(true);
    }

}
