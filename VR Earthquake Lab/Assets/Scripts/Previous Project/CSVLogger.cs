using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.ComponentModel;
using System.IO;
using UnityEngine;

/*
 * This class is provided by the previous project. 
 */
public class CSVLogger : MonoBehaviour {
	public string OutputFolder;
    private string path;
	private string log = "";
	private float time = 0;
	private float start = 0;
    private BackgroundWorker worker;
    void Start(){
        #if !UNITY_EDITOR
            this.enabled = false;
        #endif
        try
        {
        	Directory.CreateDirectory(OutputFolder);
		}
		catch{
			Debug.Log ("Creating external output folder failed.");
		}
        char sep = Path.DirectorySeparatorChar;
		//Directory.CreateDirectory(Application.streamingAssetsPath + sep + "CSVs" + sep);
		path = Application.streamingAssetsPath + sep + "Cube-CSVs" + sep + transform.name + ".csv";
    }
    // Write the content of log to a CSV file
    void writeToCSV()
    {
        //Stub. CSV output method goes here.
        Debug.Log("Writing to:" + path);
		try{
			File.WriteAllText(path, log);
            Debug.Log("Write Complete");
        }
		catch(System.Exception exception){
			Debug.Log (exception.Message);
		}
        log = "";
        time = 0;
        worker = null;
    }
    // Write the content of log to a CSV file
    void writeToExternalCSV(){
		//Stub. CSV output method goes here.
		string file = "\\"+System.DateTime.Now.ToString()+".csv";
		file = file.Replace ("/", "-").Replace(":","-");
		Debug.Log("Writing to:"+ OutputFolder+file);
		File.WriteAllText(OutputFolder+file, log);
		log = "";
		time = 0;
        worker = null;
        Debug.Log("Write Complete");
	}
	// Update is called once per frame
	void FixedUpdate () {
		if (ExperimentController.running) {
			float angle = transform.eulerAngles.z;
			if (angle > 180)
				angle -= 360;
			log += time.ToString()+","+ vectorString(transform.eulerAngles, true) +","+ vectorString(transform.position,false) + "\n";
			time += Time.realtimeSinceStartup-start;
		} else if (worker == null && !log.Equals("")) {
			worker = new BackgroundWorker ();
            worker.DoWork += new DoWorkEventHandler(delegate (object o, DoWorkEventArgs args){
                writeToCSV();
            });
            worker.RunWorkerAsync();
		}
		start = Time.realtimeSinceStartup;
	}
    private string vectorString(Vector3 vector,bool isangle){
        if (isangle){
            return Utilities.clamp(vector.x) + "," + Utilities.clamp(vector.y) + "," + Utilities.clamp(vector.z);
        }
        return vector.x+","+ vector.y + ","+vector.z;
    }
}
