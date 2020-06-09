using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script is for creating custom cylindrical colliders, purpose for testing,
//by using multiple box colliders
public class CubeControler : MonoBehaviour {
	private static float _angle = 0;
	private static float _aspect = 1;
	private static List<CubeControler> listeners = new List<CubeControler>();
	private Transform _startPosition;
	// Use this for initialization
	void Start(){
		_startPosition = new GameObject().transform;
		Utilities.CopyTransform (transform, _startPosition);
		listeners.Add (this);
		publish ();
	}
	private void place(){
		/*float x = Mathf.Pow (1.0f / _aspect, 1.0f / 3);
		float y = Mathf.Pow (1.0f / x, 2);*/
		_startPosition.localScale = new Vector3 (1, _aspect, 1);
		_startPosition.eulerAngles = new Vector3 (0, 0, _angle);
		float radangle = Mathf.Abs(_angle * Mathf.Deg2Rad);
		float height = _aspect / 2 * Mathf.Cos(radangle) + 0.5f * Mathf.Sin(radangle);
		Vector3 pos = _startPosition.position;
		pos.y = height;
		_startPosition.position = pos;
		Utilities.CopyTransform (_startPosition, transform);
	}
	public static void publish(){
		foreach(CubeControler listener in listeners){
			listener.place ();
		}
	}
	public static void SetAngle(float angle){
		_angle = angle;
		publish ();
	}
	public static void SetAspect(float aspect){
		_aspect = aspect;
		publish ();
	}
	void OnDestroy(){
		listeners.Remove (this);
	}
}
