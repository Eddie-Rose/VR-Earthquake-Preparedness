using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Provided by the previous project. 
 */
public class PlacementGrid : MonoBehaviour {
    public int width = 0;
    public int height = 0;
    public float GridSpacing = 1.0f;
    public Transform NodePrefab;
    public Marker Marker;
    private Transform[,] grid;

    private void Start() {
        grid = new Transform[width, height];
        for(int i = 0; i < width; i++){
            for(int j = 0; j < height; j++){
                Vector3 position = new Vector3(i - (width-1) / 2.0f, 0, j - (height - 1) / 2.0f) * GridSpacing;
                grid[i,j] = Instantiate<Transform>(NodePrefab, position, Quaternion.identity, transform);
            }
        }
        gameObject.SetActive(false);
    }
}
