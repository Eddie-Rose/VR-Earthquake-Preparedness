using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * This class is provided by the previous project. 
 */
public class GrowOnSelect : MonoBehaviour {
    public float DefaultSize = 1;
    public float SelectedSize = 1;
    private UIScaler uiScaler;
    private void Start(){
        uiScaler = GetComponent<UIScaler>();
    }
    public void OnSelect() {
        if (uiScaler != null)
            uiScaler.ScaleAt10m = SelectedSize;
        else
            transform.localScale = Vector3.one * SelectedSize;
    }
    public void OnDeselect() {
        if (uiScaler != null)
            uiScaler.ScaleAt10m = DefaultSize;
        else
            transform.localScale = Vector3.one * DefaultSize;
    }
}
