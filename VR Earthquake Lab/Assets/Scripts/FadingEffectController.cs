using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * The controller class for handling the fading in and fading out effect of the task panel showing in the training mode. 
 */
public class FadingEffectController : MonoBehaviour {
    public float changeTimeSeconds = 5;
    public float startAlpha = 0;
    public float endAlpha = 1;
    public GameObject controlPanel;
    float changeRate = 0;
    float timeSoFar = 0;
    bool fading = false;
    CanvasGroup canvasGroup;


    void Start()
    {
        if (!GameSettingManager.Instance.isTrainingMode)
        {
            Destroy(gameObject);
        }
        canvasGroup = this.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.Log("Must have canvas group attached!");
            this.enabled = false;
        }
        FadeOut();
    }

    public void FadeIn()
    {
        startAlpha = 0;
        endAlpha = 1;
        timeSoFar = 0;
        fading = true;
        StartCoroutine(FadeCoroutine());
    }

    public void FadeOut()
    {
        if (GameSettingManager.Instance.isTrainingMode)
        {
            controlPanel.SetActive(false);
        }
        
        startAlpha = 1;
        endAlpha = 0;
        timeSoFar = 0;
        fading = true;
        StartCoroutine(FadeCoroutine());

        
        if (TrainingManager.Instance.IsTrainingFinished())
        {
            SceneManager.LoadScene("SceneSelection");
        }
    }

    IEnumerator FadeCoroutine()
    {
        changeRate = (endAlpha - startAlpha) / changeTimeSeconds;
        SetAlpha(startAlpha);
        while (fading)
        {
            timeSoFar += Time.deltaTime;

            if (timeSoFar > changeTimeSeconds)
            {
                fading = false;
                SetAlpha(endAlpha);
                yield break;
            }
            else
            {
                SetAlpha(canvasGroup.alpha + (changeRate * Time.deltaTime));
            }

            yield return null;
        }
        

    }



    private void SetAlpha(float alpha)
    {
        canvasGroup.alpha = Mathf.Clamp(alpha, 0, 1);
        if (canvasGroup.alpha == 0)
        {
            controlPanel.SetActive(true);
        }
    }

    public void Reset()
    {
        canvasGroup.alpha = 1;
        FadeOut();
    }
}
