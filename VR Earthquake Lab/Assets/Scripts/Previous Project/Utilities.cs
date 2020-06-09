using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Provided by the previous project. 
 */
public class Utilities : MonoBehaviour {
	public static void CopyTransform(Transform fromTransform, Transform toTransform){
		toTransform.position = fromTransform.position;
		toTransform.rotation = fromTransform.rotation;
		toTransform.localScale = fromTransform.localScale;
	}
    public static float clamp(float input){
        while (input > 180)
            input -= 360;
        while (input < -180)
            input += 360;
        return input;
    }
    public static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }
    public static Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }
    public static float AnimationCurveLength(AnimationCurve curve){
        if (curve == null || curve.length == 0)
            return 0;
        return curve[curve.length - 1].time;
    }
}
