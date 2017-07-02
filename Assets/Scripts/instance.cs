using UnityEngine;
using System.Runtime.InteropServices;

public class instance : MonoBehaviour {

    private displace disp;

    public void destroyGameObject(GameObject objectToDestroy)
    {
        Destroy(objectToDestroy);
    }

    void Start () {
        var mat = GetComponent<Renderer>().material;
        GameObject chara = GameObject.Find("Character");

        Data.InitData();
        Data.Characters.Add(chara);

        // map resizing
        mat.SetTextureScale("_MainTex", new Vector2(Data.mapX, Data.mapY));
        Vector3 scale = transform.localScale;
        scale.x = 10F * Data.mapX;
        scale.z = 10F * Data.mapY;
        transform.localScale = scale;
        transform.position = new Vector3(Data.mapX * 5, 2.5F, Data.mapY * 5);
        chara.GetComponent<Renderer>().material.color = Color.blue;
        disp = chara.GetComponent<displace>();
        Data.disp = disp;
        /*
        unsafe
        {
            int* map;

            map = unitPNW();
            Data.ptr[map[0]](map);

            map = unitPPO();
            Data.ptr[map[0]](map);

            map = unitPFK();
            Data.ptr[map[0]](map);

            map = unitPGT();
            Data.ptr[map[0]](map);

            map = unitPDI();
            Data.ptr[map[0]](map);

            map = unitSEG();
            Data.ptr[map[0]](map);
        }
        */
    }

    /*
    [DllImport("graph", CharSet = CharSet.Ansi)]
    private static extern unsafe int* unitPNW();

    [DllImport("graph", CharSet = CharSet.Ansi)]
    private static extern unsafe int* unitPPO();

    [DllImport("graph", CharSet = CharSet.Ansi)]
    private static extern unsafe int* unitPDI();

    [DllImport("graph", CharSet = CharSet.Ansi)]
    private static extern unsafe int* unitPFK();

    [DllImport("graph", CharSet = CharSet.Ansi)]
    private static extern unsafe int* unitPGT();

    [DllImport("graph", CharSet = CharSet.Ansi)]
    private static extern unsafe int* unitSEG();
    */
}