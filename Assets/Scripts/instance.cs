﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instance : MonoBehaviour {

    public GameObject tile;

    void Start () {
        //tile = GameObject.Find("tile");

        Vector3 scale = transform.localScale;
        scale.x = 10F * Data.mapSize;
        scale.z = 10F * Data.mapSize;
        transform.localScale = scale;
        /*for (int i = 0; i < Data.mapSize; i++)
        {
            for (int j = 0; j < Data.mapSize; j++)
                Instantiate(tile, new Vector3(i * 11.0F, 0, j * 11.0F), Quaternion.identity);
        }*/
    }
	

	void Update () {
		
	}
}