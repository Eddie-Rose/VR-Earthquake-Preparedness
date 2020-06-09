using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This is the model that contains all the user data that requires to be saved in the json file. 
 */
[Serializable]
public class UserData {

    //To do: Might need parser for saving to json
    public List<Scenario> scenarios = new List<Scenario>();
    

    public UserData()
    {
        CreateScenarios();
    }
    public void AddScenario(Scenario scenario)
    {
        scenarios.Add(scenario);
    }

    private void CreateScenarios()
    {
        scenarios.Add(new Scenario("Office"));
        scenarios.Add(new Scenario("Living Room"));
        scenarios.Add(new Scenario("Hospital"));
    }

    public Scenario GetScenario(string name)
    {
        Scenario scenario = null;
        switch (name)
        {
            case "Office":
                scenario = scenarios[0];
                break;
            case "LivingRoom":
                scenario = scenarios[1];
                break;
            case "Hospital":
                scenario = scenarios[2];
                break;
            default:
                break;
        }
        return scenario; 
    }

    
}

public enum ScenaioName{
    OFFICE,LIVING_ROOM,HOSPITAL
}
