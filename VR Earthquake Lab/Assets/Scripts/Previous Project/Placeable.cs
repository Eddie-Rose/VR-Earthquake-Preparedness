using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Provided by the previous project. 
 */
public class Placeable : MonoBehaviour {
    public Transform Base;
    private GameObject placeTarget;

    public void PickUp(){
        if (!Player.GetInstance().SetHeldObject(this))
            return;
        if(placeTarget != null)
            placeTarget.SetActive(true);
        transform.SetParent(Player.GetInstance().RightHand, false);
        transform.localPosition = Vector3.zero;
        StateController.PickUp();
    }
    public void Place(GameObject target){
        Player.GetInstance().SetHeldObject(null);
        placeTarget = target;
        placeTarget.SetActive(false);
        transform.SetParent(null, false);
        transform.position = target.transform.position;
        if (Base != null)
            transform.position -= Base.position- transform.position;
        StateController.Place();
    }

    public void PlacePlayer()
    {
        Player.GetInstance().SetPlayerPosition(this.gameObject.transform.position);
    }
    public void Delete(){
        Destroy(gameObject);
    }
    public void RotateLeft(){
        transform.Rotate(Vector3.up, 90);
    }
    public void RotateRight() {
        transform.Rotate(Vector3.up, -90);
    }
}
