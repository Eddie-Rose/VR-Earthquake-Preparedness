using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A controller class to contol the menus that the user can interact with and popup the buttons that the user can click on. 
 */
public class ActionMenu : MonoBehaviour {
    public float MinRadius = 0.5f;
    public float MaxRadius = 2.5f;
    const float OPEN_SPEED = 5.0f;
    public float Seperation = 50.0f;
    public SphereCollider RootCollider;
    public GameObject RootItem;
    public List<Transform> MenuItems;
    private float open = 0.0f;
    private bool secondary = false;
    private float angle = 0;
    // Use this for initialization
    void Start () {
        for(int i = 0;i < MenuItems.Count; i++){
            ActionMenu childMenu = MenuItems[i].GetComponent<ActionMenu>();
            if (childMenu != null){
                childMenu.secondary = true;
                childMenu.angle = 360 / MenuItems.Count * i;
            }
        }
    }
    public void CursorEnter(){
        if (StateController.isStopped())
        {
            return;
        }
        StopAllCoroutines();
        StartCoroutine(Open());
    }
    public void CursorExit(){
        StopAllCoroutines();
        StartCoroutine(Close());
    }
    IEnumerator Open(){
        while (open < 1.0f){
            if(open > 0.2f){
                SetItemsActive(true);
            }
            open += Time.deltaTime * OPEN_SPEED;
            FanChildren(Seperation * open);
            if (RootCollider != null)
                RootCollider.radius = Mathf.Lerp(MinRadius, MaxRadius, open);
            yield return null;
        }
    }
    IEnumerator Close(){
        SetItemsActive(false);
        if (RootCollider != null)
            RootCollider.radius = MinRadius;
        while (open > 0.0f){
            open -= Time.deltaTime * OPEN_SPEED;
            FanChildren(Seperation * open);
            yield return null;
        }
    }
    private void FanChildren(float amount){
        for (int i = 0; i < MenuItems.Count; i++) {
            if (!secondary){
                MenuItems[i].localPosition = Utilities.DegreeToVector2(360 * i / MenuItems.Count) * amount;
            }
            else{
                float maxFanAngle = 180f * (MenuItems.Count - 1) / MenuItems.Count;
                MenuItems[i].localPosition = Utilities.DegreeToVector2(180 * i / MenuItems.Count + angle - maxFanAngle / 2) * amount;
            }
        }
    }
    private void SetItemsActive(bool enabled){
        if(RootItem != null)
            RootItem.SetActive(enabled);
        foreach (Transform item in MenuItems) {
            item.gameObject.SetActive(enabled);
        }
    }
    private void OnDisable(){
        SetItemsActive(false);
        StopAllCoroutines();
        open = 0.0f;
    }
}
