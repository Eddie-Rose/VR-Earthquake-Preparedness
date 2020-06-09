using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Provided by the previous project.
 */
public class PlaceTarget : MonoBehaviour {
    public GameObject PlaceButton;
    private Marker marker;
    private void Start(){
        marker = GetComponentInParent<PlacementGrid>().Marker;
    }
    public void OnTargeted(){
        marker.gameObject.SetActive(true);
        marker.transform.position = transform.position;
        PlaceButton.SetActive(true);
        StartCoroutine(CheckMarkerHit());
    }
    public void OnPlacedOn() {
        Player.GetInstance().GetHeldObject().Place(gameObject);
    }

    public void OnRotateLeft(){
        Player.GetInstance().GetHeldObject().RotateLeft();
        Retarget();
    }
    public void OnRotateRight(){
        Player.GetInstance().GetHeldObject().RotateRight();
        Retarget();
    }
    private void Retarget(){
        marker.gameObject.SetActive(false);
        OnTargeted();
    }
    IEnumerator CheckMarkerHit(){
        yield return new WaitForFixedUpdate();
        if (marker.IsHitting()){
            PlaceButton.SetActive(false);
        }
    }
}