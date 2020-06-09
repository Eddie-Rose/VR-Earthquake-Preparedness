using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * A class provided by the previous project. 
 */
[RequireComponent(typeof(GvrReticlePointer))]
public class AddTimedGaze : MonoBehaviour {
    const int INGORE_RAYCAST = 2;
    public float SliderStep = 0.2f;
    public float ClickDelay = 1;
    public float ReticleScale = 0.018f;
    private PointerEventData pointer;
    private GameObject hit;
    private float lastClick;
    private Transform circleProgress;
    // Use this for initialization
    void Start () {
        pointer = new PointerEventData(EventSystem.current);
        pointer.position = new Vector2(Screen.width / 2, Screen.height / 2);
        pointer.button = PointerEventData.InputButton.Left;
        circleProgress = GetComponentInChildren<Canvas>().transform;
    }
	
	// Update is called once per frame
	void Update () {
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);
        if(raycastResults.Count > 0 && raycastResults[0].gameObject.layer != INGORE_RAYCAST){
            setProgress(raycastResults[0].distance);
            if (hit == raycastResults[0].gameObject){
                if(lastClick >= ClickDelay) { 
                    lastClick = 0;
                    click();
                }
                lastClick += Time.deltaTime;
            }
            else {
                lastClick = 0;
                hit = raycastResults[0].gameObject;
            }
        }
        else {
            hit = null;
            setProgress(GetComponent<GvrReticlePointer>().maxReticleDistance);
            lastClick = 0;
        }
        GetComponentInChildren<Image>().fillAmount = Mathf.Clamp(lastClick / ClickDelay, 0, 1);
        
    }

    void click(){
        if (hit.GetComponent<ISubmitHandler>() != null){
            hit.GetComponent<ISubmitHandler>().OnSubmit(pointer);
        }
        else if (hit.GetComponentInParent<Scrollbar>()) {
            Scrollbar slider = hit.GetComponentInParent<Scrollbar>();
            if (slider.value > 0.001)
                slider.value -= SliderStep;
            else
                slider.value = 1;
        }
        else if (hit.GetComponentInParent<ISubmitHandler>() != null){
            hit.GetComponentInParent<ISubmitHandler>().OnSubmit(pointer);
        }
    }
    void setProgress(float distance){
        circleProgress.localPosition = new Vector3(0, 0, distance);
        float actualScale = ReticleScale * distance / 20;
        circleProgress.localScale = new Vector3(actualScale, actualScale);
    }
}
