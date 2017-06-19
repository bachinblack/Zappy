using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instance : MonoBehaviour {

    public GameObject tile;
    // Use this for initialization
    void Start () {
        tile = GameObject.Find("tile");

        for (int i = 0; i < Data.mapSize; i++)
        {
            for (int j = 0; j < Data.mapSize; j++)
                Instantiate(tile, new Vector3(i * 11.0F, 0, j * 11.0F), Quaternion.identity);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
