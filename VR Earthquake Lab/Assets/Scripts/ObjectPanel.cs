using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPanel : MonoBehaviour {
    public List<FurnitureObject> FurnitureOptions= new List<FurnitureObject>();
    public Transform PreviewTransform;
    public float RotationSpeed;
    private int index = 0;
    private GameObject displayModel;

    private void Start() {
        RefreshDisplayModel();
    }
    private void Update() {
        PreviewTransform.Rotate(Vector3.up, RotationSpeed*360*Time.deltaTime);
    }
    public void Next(){
        index++;
        if (index >= FurnitureOptions.Count)
            index = 0;
        RefreshDisplayModel();
    }
    public void Back(){
        index--;
        if (index < 0)
            index = FurnitureOptions.Count-1;
        RefreshDisplayModel();
    }
    public void Place(){
        if (Player.GetInstance().GetHeldObject() != null)
            return;
        Transform newFurniture = Instantiate(FurnitureOptions[index].PlaceableModel.transform);
        newFurniture.GetComponent<Placeable>().PickUp();
    }
    private void RefreshDisplayModel(){
        if (displayModel != null)
            Destroy(displayModel);
        displayModel = Instantiate(FurnitureOptions[index].DisplayModel, PreviewTransform);
    }
}
[Serializable]
public class FurnitureObject{
    public GameObject DisplayModel;
    public Placeable PlaceableModel;
}