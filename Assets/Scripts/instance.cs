using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instance : MonoBehaviour {

    
    private displace disp;

    void Start () {
        var mat = GetComponent<Renderer>().material;
        GameObject chara = GameObject.Find("Character");

        Data.CharactersList.Add(chara);
        mat.SetTextureScale("_MainTex", new Vector2(Data.mapSize, Data.mapSize));
        Vector3 scale = transform.localScale;
        scale.x = 10F * Data.mapSize;
        scale.z = 10F * Data.mapSize;
        transform.localScale = scale;
        transform.position = new Vector3(Data.mapSize * 5, 2.5F, Data.mapSize * 5);
        disp = chara.GetComponent<displace>();
        chara.GetComponent<Renderer>().material.color = Color.blue;
        //Data.col.getRandomColor();
        // chara instance
        Data.CharactersList.Add(Instantiate(Data.CharactersList[0], new Vector3(15, 7.5F, 5), Quaternion.identity));
        disp.setPos(new Vector3(15, 7.5F, 5));
        disp.UpdateTarget(new Vector3(15, 7.5F, 45));
    }
	

	void Update () {
		
	}
}