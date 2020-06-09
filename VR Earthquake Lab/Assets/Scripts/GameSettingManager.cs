using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/*
 * This is a singleton class for handling load game setting data from a json file and save setting data to the json file 
 * and save user data to a separate json file every time the project application is closed. 
 */
public class GameSettingManager : MonoBehaviour {

    private static string directoryName = "GameSettings";
    private static string gameSettingsFileName = "gameSettings.json";
    private static GameSettingManager _instance;

    

    public string ScenarioName { get; set; }

    private string dpath = "";
    public Settings GameSettings { get; set; }

    public UserData UserData { get; set; }
   
    public bool isTrainingMode;
    public bool[] isTrainingCompleted = { false,false,false};

    void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            dpath = Application.persistentDataPath;
        }
        else
        {
            dpath = Application.streamingAssetsPath;
        }

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);

        CreateDataFolder();
        LoadGameData();
    }
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void LoadGameData()
    {
        // Path.Combine combines strings into a file path
        // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
        string filePath = Path.Combine(dpath + "/" +directoryName, gameSettingsFileName);

        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(filePath);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            GameSettings = JsonUtility.FromJson<Settings>(dataAsJson);
        }
        else
        {
            GameSettings = new Settings();
            //SaveData();
        }
        UserData = new UserData();
    }

    private void CreateDataFolder()
    {
        string directoryPath = Path.Combine(dpath, directoryName);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
    }

    public void SaveData()
    {
        //Debug.Log("userID: "+GameSettings.UserID);
        GameSettings.IncreaseID();
        string filePath = Path.Combine(dpath + "/" + directoryName, gameSettingsFileName);
        string json = JsonUtility.ToJson(GameSettings);
        File.WriteAllText(filePath, json);
        Debug.Log(filePath);

        //Need to implement the DataSave part here
        SaveUserData();
    }

    private void SaveUserData()
    {
        string userFilePath = "user" + GameSettings.UserID + "data.json";
        string filePath = Path.Combine(dpath + "/" + directoryName, userFilePath);

        Debug.Log(UserData.scenarios[0]);
        string json = JsonUtility.ToJson(UserData);  
        File.WriteAllText(filePath, json);
    }
    public void UpdateScenarioCompletion()
    {
        switch (ScenarioName)
        {
            case "Office":
                isTrainingCompleted[0] = true;
                break;
            case "LivingRoom":
                isTrainingCompleted[1] = true;
                break;
            case "Hospital":
                isTrainingCompleted[2] = true;
                break;
            default:
                
                break;
        }
    }
    public static GameSettingManager Instance { get { return _instance; } }
}
