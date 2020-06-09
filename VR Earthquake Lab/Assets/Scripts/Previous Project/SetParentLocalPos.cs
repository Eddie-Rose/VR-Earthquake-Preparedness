using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Provided by the previous project. 
 */
public class SetParentLocalPos : MonoBehaviour {
    public void SetParent(Transform parent){
        transform.SetParent(parent, false);
    }
}
