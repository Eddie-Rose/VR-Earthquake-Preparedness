using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * This is the class for displaying the loading progress of the scenario scene (office, living room or hospital) in the loading scene. 
 */
public class LoadingSceneManager : MonoBehaviour {
    public SimpleHealthBar healthBar;

    // Use this for initialization
    void Start () {
        StartCoroutine(LoadAsyncOperation());
        SceneLoadManager.Instance.OnDestroy();
    }

    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator LoadAsyncOperation()
    {
        string scene = SceneLoadManager.Instance.prefabName;

        GameSettingManager.Instance.ScenarioName = scene;

        AsyncOperation game = SceneManager.LoadSceneAsync(scene);
        while (game.progress<1)
        {
            healthBar.UpdateBar(game.progress, 1);
            yield return new WaitForEndOfFrame();
        }
        
    }
}
