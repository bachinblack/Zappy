using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class displace : MonoBehaviour {

    private float speed = 2F;
    private Vector3 requiredPos;
    private Animator anim;
    private bool walking = false;

    void Start () {
        anim = GetComponent<Animator>();
        // position to initialize at the creation
        transform.position = new Vector3(5, 7.5F, 5);

        // position to modifie at character's update
        UpdateTarget(new Vector3(5, 7.5F, 25));
    }
	

    public void setPos(Vector3 pos)
    {
        transform.position = pos;
    }

    public void UpdateTarget(Vector3 target)
    {
        requiredPos = target;
        walking = true;
        anim.SetFloat("Speed", 1);
    }

	void Update () {
        float step = speed * Time.deltaTime;

        if (transform.position != requiredPos)
            transform.position = Vector3.MoveTowards(transform.position, requiredPos, step);
        else if (walking == true)
        {
            anim.SetFloat("Speed", 0);
            walking = false;
        }
        transform.LookAt(requiredPos);


    }
}
