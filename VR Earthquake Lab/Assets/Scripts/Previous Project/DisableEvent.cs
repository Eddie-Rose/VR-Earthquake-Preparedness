using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * This class is provided by the previous project. 
 */
public class DisableEvent : MonoBehaviour {
    public UnityEvent OnDisableEvent;
    private void OnDisable() {
        OnDisableEvent.Invoke();
    }
}
