using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Text))]
public class Stats : MonoBehaviour {
    private Text text;
    private float physicsLatency;
    private float lastPhysicsUpdate;
    private float lastFrameUpdate;
    void Start(){
        text = GetComponent<Text>();
    }
    void FixedUpdate() {
        physicsLatency = Time.realtimeSinceStartup - lastPhysicsUpdate;
        lastPhysicsUpdate = Time.realtimeSinceStartup;
    }
    void Update()
    {
        float latency = Time.realtimeSinceStartup - lastFrameUpdate;
        lastFrameUpdate = Time.realtimeSinceStartup;
        text.text = Round(1 / latency,1) + " FPS\n" + Round(1000*latency,1) + "ms\nPhysics: " + Round(1000 * physicsLatency,1) +"ms";
    }
    private float Round(float num,int decimals){
        return Mathf.Round(num * Mathf.Pow(10 , decimals)) / Mathf.Pow(10 , decimals);
    }
}
