using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * This is the controller class to allow user to choose on the object and mark their selection correct or wrong based on the task. 
 */
public class HazardSelection : MonoBehaviour {

    public bool userChoice;
    public List<bool> correctAnswers;
    public List<bool> shouldDisplay;
    public GameObject rootItem;
    public List<Sprite> sprites;
    
	// Use this for initialization
	void Start () {
		if(GameSettingManager.Instance != null)
        {
            if (!GameSettingManager.Instance.isTrainingMode)
            {
                //this.gameObject.SetActive(false);
                Destroy(this.gameObject);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!StateController.isSimulationStarted())
        {
            ShowSelectionMenu();
            CheckDisplay();
        }
        
	}

    public void CheckAnswer()
    {
        if (!shouldDisplay[TrainingManager.Instance.trainingStage])
            return;
        bool isCorrect;
        bool correctAnswer = correctAnswers[TrainingManager.Instance.trainingStage];
        if(correctAnswer == userChoice)
        {
            isCorrect = true;
            rootItem.GetComponent<Image>().sprite = sprites[2];
        }
        else
        {
            isCorrect = false;
            rootItem.GetComponent<Image>().sprite = sprites[3];
        }

        UpdateAnswer(isCorrect);


    }

    private void UpdateAnswer(bool isCorrect)
    {
        string str = gameObject.transform.parent.gameObject.name;
        string parentName = !string.IsNullOrEmpty(str) ? str : "No Parent";
        Question question = new Question(parentName);
        question.IsCorrect = isCorrect;
        Scenario scenario = GameSettingManager.Instance.UserData.GetScenario(GameSettingManager.Instance.ScenarioName);
        int stage = TrainingManager.Instance.trainingStage;
        scenario.UpdateStage(stage, question);
    }
    public void SetUserChoice(bool choice)
    {
        this.userChoice = choice;
        if (choice)
        {
            rootItem.GetComponent <Image>().sprite = sprites[1];
        }
        else
        {
            rootItem.GetComponent<Image>().sprite = sprites[0];
        }
    }

    private void ShowSelectionMenu()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void HideSelectionMenu()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void CheckDisplay()
    {
        int num = TrainingManager.Instance.trainingStage;
        gameObject.SetActive(shouldDisplay[num]);
    }

    public void Reset()
    {
        userChoice = false;
        rootItem.GetComponent<Image>().sprite = sprites[0];
    }


}
