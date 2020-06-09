using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * This is provided by the previous project. 
 */
public class StateListener : MonoBehaviour {
    public StateController.State ListenerState;
    public UnityEvent StateUpdateEvent;
    private void Awake(){
        StateController.AddListner(this);
    }
    public void OnStateUpdate(){
        //Debug.Log(this);
        StateUpdateEvent.Invoke();
    }
    private void OnDestroy() {
        StateController.RemoveListner(this);
    }
}
