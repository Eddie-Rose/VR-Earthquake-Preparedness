using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeManager : MonoBehaviour {
	public GameObject CubePrefab;
	private static GameObject cubePrefab;
    private bool realtime = true;
	private static List<GameObject> instances = new List<GameObject>();
	void Start(){
		cubePrefab = CubePrefab;
	}
	private static Vector3 position(int index){
		int n = index+1;
		int depth = (int)Mathf.Floor(-0.5f + Mathf.Sqrt (-1.75f + 2 * n));
		int left = n-(depth) * (depth + 1) / 2-1;
		return new Vector3(left*2-depth, 0.5f, depth*2);
	}
	public static void OnNumberUpdate(int Count){
		while (instances.Count > Count) {
			remove ();
		}
		while (instances.Count < Count) {
			add ();
		}
	}
	private static void add(){
		GameObject instance = Instantiate (cubePrefab, position (instances.Count),Quaternion.identity);
        instance.name = "Cube" + instances.Count;
        instances.Add (instance);
	}
	private static void remove(){
		Destroy (instances[instances.Count - 1]);
		instances.RemoveAt (instances.Count - 1);
	}
	public static void SetCubesIsKinematic(bool freezed){
		CubeControler.publish ();
        foreach (GameObject instance in instances){
            if (!instance.GetComponent<CSVReplayer>().enabled) { 
            instance.GetComponent<Rigidbody>().isKinematic = freezed;
            }
            else if (!freezed){
                instance.GetComponent<CSVReplayer>().Replay();
            }
		}
	}
    public void SetRealTime(Toggle toggle){
        realtime = toggle.isOn;
        int count = instances.Count;
		foreach (GameObject instance in instances){
			instance.GetComponent<CSVReplayer>().enabled = !realtime;
			#if UNITY_EDITOR
			instance.GetComponent<CSVLogger>().enabled = realtime;
			#else 
			instance.GetComponent<CSVLogger>().enabled = false;
			#endif
        }
        cubePrefab.GetComponent<CSVReplayer>().enabled = !realtime;
		#if UNITY_EDITOR
		cubePrefab.GetComponent<CSVLogger>().enabled = realtime;
		#else 
		cubePrefab.GetComponent<CSVLogger>().enabled = false;
		#endif
    }
}
