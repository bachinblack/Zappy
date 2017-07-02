using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSun : MonoBehaviour {

    private Vector3 pos;

    void Start () {
        pos = transform.rotation.eulerAngles;
    }
	
	void Update () {
        pos.x = pos.x - (Time.deltaTime * 3);
        if (pos.x < -0.8F)
        {
            pos.x = 180.8F;
            pos.y = -20;
        }
        else if (pos.x < 90F && pos.y != 20)
            pos.y = 20;
        transform.rotation = Quaternion.Euler(pos);
        Data.Update();
    }
}
