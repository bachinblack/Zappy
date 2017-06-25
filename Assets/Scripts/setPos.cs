using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setPos : MonoBehaviour {

	void Start () {
        System.Random random = new System.Random();

        transform.position = new Vector3(random.Next(1, Data.mapSize - 1) * 10, 12F, random.Next(1, Data.mapSize - 1) * 10);
	}
}
