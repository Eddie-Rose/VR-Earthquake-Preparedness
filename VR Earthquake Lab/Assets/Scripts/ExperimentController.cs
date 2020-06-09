using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperimentController : MonoBehaviour {

	public static bool running = false;
	public Button startButton;
	private Text buttonText;
	private int duration = -1;
	private int timeLeft = -1;
	void Start(){
		buttonText = startButton.GetComponentInChildren<Text> ();
	}

	void FixedUpdate () {
		if (timeLeft == 0) {
			StopExperiment();
		}
		if (running) {
			timeLeft--;
		}
	}
	void StopExperiment(){
		running = false;
		buttonText.text = "Start";
		CubeManager.SetCubesIsKinematic (true);
	}
	public void SetDuration(InputField input){
		duration = int.Parse(input.text);
	}
	public void ParseAndSetAngle(InputField input){
		float angle = float.Parse (input.text);
		angle = Mathf.Clamp (angle, -90, 90);
		input.text = angle.ToString();
		CubeControler.SetAngle (angle);
	}
	public void ParseAndSetAspect(InputField input){
		float aspect = float.Parse (input.text);
		if (aspect <= 0) {
			input.text = 1.0f.ToString();
			return;
		}
		CubeControler.SetAspect (aspect);
	}
    public void ToggleExperiment() {
		if (!running) {
			timeLeft = duration;
			running = true;
			buttonText.text = "Stop";
			CubeManager.SetCubesIsKinematic (false);
		} 
		else {
			StopExperiment ();
		}
    }
}
