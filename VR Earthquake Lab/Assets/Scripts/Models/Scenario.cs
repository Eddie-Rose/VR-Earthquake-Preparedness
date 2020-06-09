using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A model class for recording the scenario that the user has played. 
 */
[Serializable]
public class Scenario {

    public string ScenarioName;
    public List<Stage> stages = new List<Stage>();
    public List<CameraHistory> cameraHistories = new List<CameraHistory>();
    public Scenario(string scenarioName)
    {
        this.ScenarioName = scenarioName;
    }
    public void AddStage(Stage stage)
    {
        stages.Add(stage);
    }
    public void AddHistory(CameraHistory cameraHistory)
    {
        cameraHistories.Add(cameraHistory);
    }
    public void UpdateStage(int index, Question question)
    {
        if (index > stages.Count)
        {
            Debug.Log("Out of boudary");
            return;
        }

        Stage stage = stages[index];
        stage.AddQuestion(question);

    }
}
