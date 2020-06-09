using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Provided by the previous project. 
 */
public class RenderQueueEditor : MonoBehaviour
{

    public UnityEngine.Rendering.CompareFunction comparison = UnityEngine.Rendering.CompareFunction.Always;
    private void Start()
    {
        Graphic uiElement = GetComponent<Graphic>();
        Material existingGlobalMat = uiElement.materialForRendering;
        Material updatedMaterial = new Material(existingGlobalMat);
        updatedMaterial.SetInt("unity_GUIZTestMode", (int)comparison);
        uiElement.material = updatedMaterial;
    }
}
