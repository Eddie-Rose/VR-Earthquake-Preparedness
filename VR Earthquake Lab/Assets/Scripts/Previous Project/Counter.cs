using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

/*
 * This class is provided by the previous project. 
 */
[RequireComponent(typeof(InputField))]
public class Counter : MonoBehaviour {
	public int Count = 1;
	private InputField field;
	public void Start(){
		field = GetComponent<InputField> ();
	}
	public void onUpdate(){
		try{
			Count = int.Parse(field.text);
		}
		catch(System.FormatException exception){
			Count = 0;
			field.text = 0.ToString();
		}
		CubeManager.OnNumberUpdate (Count);
	}
	public void Add(){
		field.text = (Count+1).ToString();
		onUpdate ();
	}
	public void Remove(){
		field.text = (Count-1).ToString();
		onUpdate ();
	}
}
