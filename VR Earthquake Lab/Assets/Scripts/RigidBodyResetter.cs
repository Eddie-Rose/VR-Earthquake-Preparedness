using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Provided by the previous project. 
 */
public class RigidBodyResetter : MonoBehaviour {
    private Transform savedPosition;
    private Rigidbody rigidbody;
    private void Start(){
        rigidbody = GetComponent<Rigidbody>();
        savedPosition = new GameObject().transform;
        rigidbody.isKinematic = true;
    }
    public void SavePosition() {
        Utilities.CopyTransform(transform, savedPosition);
        rigidbody.isKinematic = false;
    }
    public void ResetPosition(){
        Utilities.CopyTransform(savedPosition, transform);
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }
    private void OnDestroy(){
        if(savedPosition != null)
            Destroy(savedPosition.gameObject);
    }
}