using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureResetter : MonoBehaviour {

    public Hashtable recordings = new Hashtable();

    // Use this for initialization
    void Awake() {
        Component[] gameObjects = this.GetComponentsInChildren<Rigidbody>();


        foreach(Component component in gameObjects)
        {
            if (component.gameObject.GetComponent<Rigidbody>())
            {
                ObjectPosition objectPosition = new ObjectPosition(component.gameObject.transform.localPosition, component.gameObject.transform.localEulerAngles);
                recordings.Add(component.gameObject.GetInstanceID().ToString(), objectPosition);
            }
        }
	}
	
    public void resetPosition()
    {
        StartCoroutine(Resetter());

    }

    IEnumerator Resetter()
    {
        yield return new WaitForSeconds(0.1f);

        Component[] gameObjects = this.GetComponentsInChildren<Rigidbody>();
        foreach (Component component in gameObjects)
        {
            if (component.gameObject.GetComponent<Rigidbody>())
            {
                component.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                int componentID = component.gameObject.GetInstanceID();
                ObjectPosition initialPosition = (ObjectPosition)recordings[componentID.ToString()];

                component.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
                component.gameObject.GetComponent<Rigidbody>().isKinematic = true;

                component.gameObject.transform.localPosition = initialPosition.OriginalPosition;
                component.gameObject.transform.localEulerAngles = initialPosition.OriginalRotation;

                component.gameObject.GetComponent<Rigidbody>().detectCollisions = true;
                component.gameObject.GetComponent<Rigidbody>().isKinematic = false;


            }
        }
    }

    class ObjectPosition
    {
        public Vector3 OriginalPosition { get; set; }
        public Vector3 OriginalRotation { get; set; }

        public ObjectPosition(Vector3 originalPosition, Vector3 originalRotation)
        {
            OriginalPosition = originalPosition;
            OriginalRotation = originalRotation;
        }
    }
}
