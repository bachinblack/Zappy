using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyCamInputs : MonoBehaviour {

	void Start () {
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (Data.camType == 0)
            {
                GameObject.Find("FPSController").GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;

                Data.camType = 1;
                Data.cams[0].GetComponent<Camera>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                Data.cams[1].GetComponent<Camera>().enabled = true;
            }
            else
            {
                GameObject.Find("FPSController").GetComponent<FirstPersonController>().m_MouseLook.lockCursor = true;
                Data.camType = 0;
                Data.cams[0].GetComponent<Camera>().enabled = true;
                Data.cams[1].GetComponent<Camera>().enabled = false;
            }
        }
        if (Data.camType == 0)
            return;
        if ((Input.GetKey("q") || Input.GetKey("left")) && transform.position.x > 35F)
        {
            transform.Translate(new Vector3(-0.5F, 0F, 0F));
        }
        if ((Input.GetKey("d") || Input.GetKey("right")) && transform.position.x < Data.mapX * 10 - 35F)
        {
            transform.Translate(new Vector3(0.5F, 0F, 0F));
        }
        if ((Input.GetKey("s") || Input.GetKey("down")) && transform.position.z > 21F)
        {
            transform.Translate(new Vector3(0F, -0.5F, 0F));
        }
        if ((Input.GetKey("z") || Input.GetKey("up")) && transform.position.z < Data.mapY * 10 - 21F)
        {
            transform.Translate(new Vector3(0F, 0.5F, 0F));
        }

    }
}
