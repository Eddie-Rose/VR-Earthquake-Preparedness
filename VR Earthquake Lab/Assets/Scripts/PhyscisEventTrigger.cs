using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhyscisEventTrigger : MonoBehaviour {
    public List<CollisionEvent> CollisionEvents = new List<CollisionEvent>();
    public List<RestraintEvent> RestraintEvents = new List<RestraintEvent>();
    void OnCollisionEnter(Collision collision){
        float impactForce = collision.impulse.magnitude / Time.fixedDeltaTime;
        foreach(CollisionEvent collisionEvent in CollisionEvents) {
            collisionEvent.OnCollision(impactForce);
        }
    }
    void OnJointBreak(float breakForce) { 
        foreach(RestraintEvent restraintEvent in RestraintEvents) {
            restraintEvent.OnBreak();
        }
    }
}
[Serializable]
public class RestraintEvent {
    public Joint restraint;
    public UnityEvent BreakEvent = new UnityEvent();
    public void OnBreak() {
        if (restraint == null){
            BreakEvent.Invoke();
        }
    }
}
[Serializable]
public class CollisionEvent {
    public float ImpactForceThreshold;
    public UnityEvent ImpactEvent = new UnityEvent();
    public void OnCollision(float impactForce) {
        if (impactForce > ImpactForceThreshold) {
            ImpactEvent.Invoke();
        }
    }
}