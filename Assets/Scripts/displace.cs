using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class displace : MonoBehaviour {

    private float speed = 2F;
    private Vector3 requiredPos = new Vector3(5, 0, 5);
    private Animator anim;
    private bool walking = false;
    private int id = -1;
    private int level = 0;
    private int team = 0;
    public int tile;

    public void dest(GameObject objectToDestroy)
    {
        Destroy(objectToDestroy, 0F);
    }

    public GameObject inst(GameObject objectToClone, Vector3 pos, Quaternion rot)
    {
        return (Instantiate(objectToClone, pos, rot));
    }

    void Start () {
        anim = GetComponent<Animator>();
        id = -1;
    }

    public void setId(int nid) { id = nid; }

    public  int getId() { return id; }

    public void setPos(Vector3 pos) { transform.position = pos; requiredPos = pos; }

    public void setRot(Quaternion rot) { transform.localRotation = rot; }

    public void UpdateTarget(Vector3 target) { requiredPos = target; }

    public void initValues(int nlvl, int ntm) { level = nlvl; team = ntm; }

    public void levelUp() { ++level; }

	void Update () {
        float step = speed * Time.deltaTime;

        if (transform.position != requiredPos)
        {
            if (walking == false)
            {
                walking = true;
                anim.SetFloat("Speed", 1);
            }
            transform.position = Vector3.MoveTowards(transform.position, requiredPos, step);
        }
        else if (walking == true)
        {
            anim.SetFloat("Speed", 0);
            walking = false;
        }
    }
}
