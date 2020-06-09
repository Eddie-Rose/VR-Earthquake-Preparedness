using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This is the script to record the camera's rotation and postions. These data are not used in this project, but might be used as a future work.
 */
public class CameraHistoryRecorder : MonoBehaviour {

    private bool isRecording;
    // Use this for initialization
    void Start () {
        InvokeRepeating("RecordCamera", 0.0f, 0.1f);
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    private void RecordCamera()
    {
        if (isRecording)
        {
            CameraHistory history = new CameraHistory(gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.forward);
            GameSettingManager.Instance.UserData.GetScenario(GameSettingManager.Instance.ScenarioName).AddHistory(history);
        }
        
    }

    public void StartRecord()
    {
        isRecording = true;
    }

    public void StopRecord()
    {
        isRecording = false;
    }
}
