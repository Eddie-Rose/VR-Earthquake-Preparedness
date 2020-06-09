using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * A class to handle the sceneloading. 
 */
public class SceneLoadManager : MonoBehaviour {

    public int SceneIndex;
    public Transform prefabOnLoad;
    public string prefabName;
    private static SceneLoadManager _instance;

    public static SceneLoadManager Instance { get { return _instance; } }
    private void Awake() {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }
    public void SetPrefab(Transform prefab) {
        prefabOnLoad = prefab;
        prefabName = prefab.name;
    }
    public void LoadScene() {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(4);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        if(prefabOnLoad != null) {
            Instantiate(prefabOnLoad);
        }
    }

    public void OnDestroy()
    {
        Destroy(this.gameObject);
    }
}



