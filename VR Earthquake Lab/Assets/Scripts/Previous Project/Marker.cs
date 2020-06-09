using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Provided by the previous project. 
 */
public class Marker : MonoBehaviour {
    private bool isHitting;
    public bool IsHitting(){
        return isHitting;
    }
    private void OnEnable(){
        Placeable placeable = Player.GetInstance().GetHeldObject();
        transform.rotation = placeable.transform.localRotation;
        BoxCollider bounds = placeable.Base.GetComponent<BoxCollider>();
        GetComponent<BoxCollider>().center = bounds.center;
        GetComponent<BoxCollider>().size = bounds.size*0.95f;
        transform.GetChild(0).localPosition = bounds.center;
        transform.GetChild(0).localScale = bounds.size;
    }
    private void OnTriggerStay(Collider other) {
        if (other.gameObject.tag.Equals("Ground"))
            return;
        isHitting = true;
        GetComponentInChildren<Renderer>().material.color = Color.red;
    }
    private void OnTriggerExit(Collider other){
       isHitting = false;
        GetComponentInChildren<Renderer>().material.color = Color.green;
    }
}
