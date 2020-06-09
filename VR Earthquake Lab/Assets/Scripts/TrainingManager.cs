using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * This is a singleton class for handling the training mode including showing what stage the user is on and 
 * whether the user has finished the training for each scenario. 
 */
public class TrainingManager : MonoBehaviour {
    public int trainingStage = 0;
    public string[] objectives;
    public Text objective;

    private static TrainingManager _instance;
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    // Use this for initialization
	void Start () {
        if (!GameSettingManager.Instance.isTrainingMode)
        {
            Destroy(gameObject);
            return;
        }
        RecordStage();
        if (!GameSettingManager.Instance.isTrainingMode)
        {
            gameObject.SetActive(false);
            gameObject.GetComponent<TrainingManager>().enabled = false;
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void NextStage()
    {
        
        if (trainingStage >= objectives.Length)
        {
            return;
        }
        trainingStage++;
        UpdateText();
        RecordStage();
    }

    private void SetObjective(string text)
    {
        objective.text = text;
    }

    private void UpdateText()
    {
        int stage = trainingStage;
        if (stage < objectives.Length)
            SetObjective(objectives[stage]);
    }

    private void RecordStage()
    {
        Stage stage = new Stage(trainingStage, objectives[trainingStage]);
        Scenario scenario = GameSettingManager.Instance.UserData.GetScenario(GameSettingManager.Instance.ScenarioName);
        scenario.AddStage(stage);
    }

    public bool IsTrainingFinished()
    {
        if (trainingStage == objectives.Length-1)
        {
            GameSettingManager.Instance.UpdateScenarioCompletion();
            Destroy(gameObject);
            return true;
        }
       
        return false;
        
    }

    public void DestroyTrainingManager()
    {
        if(GameSettingManager.Instance.isTrainingMode)
            Destroy(gameObject);
    }
    public static TrainingManager Instance { get { return _instance; } }
}
