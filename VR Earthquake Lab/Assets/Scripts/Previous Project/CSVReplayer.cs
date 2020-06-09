using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/*
 * This class is provided by the previous project.
 */
[RequireComponent(typeof(Animation))]
public class CSVReplayer : MonoBehaviour {
    public string PathToCSV;
    private AnimationClip motion;
    private Animation _animation;
	// Use this for initialization
	void Start () {
        _animation = GetComponent<Animation>();
        char sep = Path.DirectorySeparatorChar;
		PathToCSV = Application.streamingAssetsPath + sep + "Cube-CSVs" + sep + transform.name + ".csv";
        Load();
    }
    private void Load() {
        motion = new AnimationClip();
        motion.legacy = true;
        List<Keyframe> rotX = new List<Keyframe>();
        List<Keyframe> rotY = new List<Keyframe>();
        List<Keyframe> rotZ = new List<Keyframe>();
        List<Keyframe> posX = new List<Keyframe>();
        List<Keyframe> posY = new List<Keyframe>();
        List<Keyframe> posZ = new List<Keyframe>();
        #if UNITY_EDITOR
        TextReader sr = File.OpenText(PathToCSV);
        #else
        WWW reader = new WWW(PathToCSV);
        while (!reader.isDone) { }
        TextReader sr = new StringReader(reader.text);
        #endif
        string line = null;
        while ((line = sr.ReadLine()) != null){
            string[] frag = line.Split(',');
            rotX.Add(new Keyframe(float.Parse(frag[0]), float.Parse(frag[1])));
            rotY.Add(new Keyframe(float.Parse(frag[0]), float.Parse(frag[2])));
            rotZ.Add(new Keyframe(float.Parse(frag[0]), float.Parse(frag[3])));
            posX.Add(new Keyframe(float.Parse(frag[0]), float.Parse(frag[4])));
            posY.Add(new Keyframe(float.Parse(frag[0]), float.Parse(frag[5])));
            posZ.Add(new Keyframe(float.Parse(frag[0]), float.Parse(frag[6])));
        }
        motion.SetCurve("", typeof(Transform), "localEuler.x", new AnimationCurve(rotX.ToArray()));
        motion.SetCurve("", typeof(Transform), "localEuler.y", new AnimationCurve(rotY.ToArray()));
        motion.SetCurve("", typeof(Transform), "localEuler.z", new AnimationCurve(rotZ.ToArray()));
        motion.SetCurve("", typeof(Transform), "localPosition.x", new AnimationCurve(posX.ToArray()));
        motion.SetCurve("", typeof(Transform), "localPosition.y", new AnimationCurve(posY.ToArray()));
        motion.SetCurve("", typeof(Transform), "localPosition.z", new AnimationCurve(posZ.ToArray()));
        _animation.AddClip(motion,motion.name);
    } 
	
    public void Replay(){
        _animation.Play(motion.name);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
