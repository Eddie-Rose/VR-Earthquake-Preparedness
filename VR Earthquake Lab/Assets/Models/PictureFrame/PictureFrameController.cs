using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureFrameController : MonoBehaviour {
    float timeToFall;
    bool isCollided;
    AudioSource pictureFrameHitSound;
    public GameObject funnyZain;

    private Vector3 originalPosition;
    private Vector3 originalRotation;

    public GameObject floor;

    void Awake()
    {
        originalPosition = this.gameObject.transform.localPosition;
        originalRotation = this.gameObject.transform.localEulerAngles;
    }

    // Use this for initialization
    void Start () {
        pictureFrameHitSound = gameObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!StateController.isSimulationStarted())
        {
            timeToFall = 0;
        }
        if (timeToFall > 7)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        timeToFall += Time.deltaTime;

        funnyZain.SetActive(GameSettingManager.Instance.GameSettings.enableJumpScare && GameSettingManager.Instance.GameSettings.isExihibitonMode);
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (floor != null)
        {
            if (collision.collider.Equals(floor.GetComponent<Collider>()))
            {
                if (!isCollided)
                {
                    PlaySource();
                    isCollided = true;
                }

            }
        }

    }

    void PlaySource()
    {
        pictureFrameHitSound.Play();
    }

    public void resetPosition()
    {
        StartCoroutine(Resetter());
    }

    IEnumerator Resetter()
    {
        yield return new WaitForSeconds(0.2f);

        this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        this.GetComponent<Rigidbody>().detectCollisions = false;
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.GetComponent<Rigidbody>().useGravity = false;

        this.gameObject.transform.localPosition = originalPosition;
        this.gameObject.transform.localEulerAngles = originalRotation;

        this.GetComponent<Rigidbody>().detectCollisions = true;
        this.GetComponent<Rigidbody>().isKinematic = false;
    }
}
