using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using System.Runtime.InteropServices;
using System;

public class Data
{
    static public RandCol col = new RandCol();
    static public int mapX = 10;
    static public int mapY = 10;
    static public List<GameObject> Characters = new List<GameObject>();
    static public List<GameObject> Items = new List<GameObject>();
    static public List<Egg> Eggs = new List<Egg>();
    static public displace disp;
    static public ushort camType = 0;
    static public Camera[] cams = new Camera[2];

    [DllImport("graph", CharSet = CharSet.Ansi)]
    private static extern unsafe int** get_map();

    [DllImport("graph", CharSet = CharSet.Ansi)]
    private static extern unsafe int* treatment();

    [DllImport("graph", CharSet = CharSet.Ansi)]
    private static extern unsafe int* get_map_size();

    public static GameObject   getPlayer(int i)
    {
        foreach (GameObject obj in Characters)
        {
           if (obj.GetComponent<displace>().getId() == i)
               return obj;
        }
        return Characters[0];
    }

    public static Egg getEgg(int i)
    {
        foreach (Egg obj in Eggs)
        {
            if (obj.id == i)
                return obj;
        }
        return null;
    }


    unsafe public delegate void Deleg(int *i);

    public static Deleg[] ptr = new Deleg[8];

    private static unsafe void enw(int* i) { }

    private static unsafe void pdi(int* i) { disp.dest(getPlayer(i[1])); }

    private static unsafe void pfk(int* i)
    {
        Transform[] tr = Items[getPlayer(i[1]).GetComponent<displace>().tile].GetComponentsInChildren<Transform>();

        foreach (Transform ch in tr)
        {
            if (ch.name == "Egg")
            {
                Debug.Log("Found Egg");
                ch.localScale = new Vector3(ch.localScale.x + 0.3F, ch.localScale.x + 0.3F, ch.localScale.x + 0.3F);
                Eggs.Add(new Egg(getPlayer(i[1]).GetComponent<displace>().tile));
            }
        }
    }

    private static unsafe void pgt(int* i)
    {
        Transform[] tr = Items[getPlayer(i[1]).GetComponent<displace>().tile].GetComponentsInChildren<Transform>();
        List<Transform> li = new List<Transform>();
 
        foreach (Transform ch in tr)
        {
            if (!ch.name.Contains("Item"))
                li.Add(ch);
        }
        li[i[2]].localScale = new Vector3(li[i[2]].localScale.x - 0.3F, li[i[2]].localScale.x - 0.3F, li[i[2]].localScale.x - 0.3F);
    }

    private static unsafe void pnw(int* i)
    {
        GameObject nw = disp.inst(Characters[0], new Vector3(15, 7.5F, 5), Quaternion.identity);
        displace d = nw.GetComponent<displace>();


        d.setId(i[1]);
        d.setPos(new Vector3(i[2] * 10 + 5, 7.5F, i[3] * 10 + 5));
        d.setRot(new Quaternion(0, i[4] * 90F, 0, 0));
        Characters.Add(nw);
    }

    private static unsafe void ppo(int* i) { getPlayer(i[1]).GetComponent<displace>().UpdateTarget(new Vector3(i[2] * 10 + 5, 7.5F, i[3] * 10 + 5)); getPlayer(i[1]).GetComponent<displace>().tile = i[2] * mapX + i[3]; }

    private static unsafe void seg(int* i) { GameObject.Find("FPSController").GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false; SceneManager.LoadScene("End"); }

    private static unsafe void sgt(int* i) { }

    public static unsafe void Update()
    {
        int* tab;
        int max = 0;
     

        while ((tab = treatment()) != null)
        {
            Debug.Log("New update!");
            if (++max == 20)
                break;
            ptr[tab[0]](tab);
        }
    }

    public static unsafe void InitData()
    {
        GameObject Sample = GameObject.Find("Items");

        cams[0] = GameObject.Find("FirstPersonCharacter").GetComponent<Camera>();
        cams[1] = GameObject.Find("SkyCam").GetComponent<Camera>();
        Data.cams[1].GetComponent<Camera>().enabled = false;

        ptr[0] = new Deleg(enw);
        ptr[1] = new Deleg(pdi);
        ptr[2] = new Deleg(pfk);
        ptr[3] = new Deleg(pgt);
        ptr[4] = new Deleg(pnw);
        ptr[5] = new Deleg(ppo);
        ptr[6] = new Deleg(seg);
        ptr[7] = new Deleg(sgt);

        for (ushort x = 0; x < (mapX); ++x)
        {
            for (ushort y = 0; y < (mapY); ++y)
            {
                Items.Add(GameObject.Instantiate(Sample, new Vector3(x * 10F, 0F, y * 10F), Sample.transform.rotation));
            }
        }


        int** tab = null;
        int* size;

        if ((tab = get_map()) == null)
		{
			Debug.Log("Abort");
			return ;
		}
        else
        {
            size = get_map_size();
            mapX = size[0];
            mapY = size[1];
        }

        // Change items pos when abort succeed

        Debug.Log(Items.Count);
        float scale;
        Transform[] children = Items[0].GetComponentsInChildren<Transform>();
        List<Transform> RealChildren = new List<Transform>();

        for (ushort i=0; tab[i] != null; ++i)
        {
            if (tab[i][1] < 0)
                tab[i][1] = 0;
            children = Items[(tab[i][2] * mapX) + tab[i][3]].GetComponentsInChildren<Transform>();
            RealChildren.Clear();
                
            foreach (var child in children)
            {
                if (!child.name.Contains("Item"))
                {
                  RealChildren.Add(child);
                }
            }
			scale = tab[i][1] * 0.3F;
            Debug.Log(scale);
            if (scale == 0)
                scale = 0.1F;
			RealChildren[tab[i][0]].localScale = new Vector3(scale, scale, scale);
		}

		Debug.Log("END OF INIT");
    }
}

public class Egg
{
    private static int gid = 0;
    private int tile;
    public int id;

    public Egg(int nTile)
    {
        tile = nTile;
        id = gid;
        ++gid;
    }

    public void dellEgg()
    {
        Vector3 scale = Data.Items[tile].transform.localScale;

        scale.x = scale.y = scale.z = scale.x - 0.3F;
        Data.Items[tile].transform.localScale = scale;
    }
}

public class RandCol
{
    System.Random r;

    void Start() { r = new System.Random(); }

    public Color getRandomColor() { return new Color(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255)); }
}

public class launchGame : MonoBehaviour {

    private bool to = true;
    private string ip = "IP";
    private string port = "Port";
    private bool isClickable = false;

    [DllImport("graph", CharSet=CharSet.Ansi)]
    private static extern int initSocket( [Out] StringBuilder ip, ushort port);

    void OnGUI()
    {
        ip = GUI.TextField(new Rect(150, 220, 180, 20), ip, 15);
        port = GUI.TextField(new Rect(150, 320, 180, 20), port, 5);

    }
    void Start ()
    {
        Screen.SetResolution(1280, 768, false);
    }

    void OnMouseEnter()
    {
        isClickable = true;
        transform.position = new Vector3(3F, 1F, -0.9F);
        GetComponent<Renderer>().material.color = Color.cyan;
    }

    void OnMouseExit()
    {
        isClickable = false;
        transform.position = new Vector3(3F, 1F, -0.8F);
        GetComponent<Renderer>().material.color = Color.white;
    }

    void OnMouseDown()
    {
        transform.position = new Vector3(3F, 1F, -0.8F);
    }

    void OnMouseUp()
    {
        ushort iport;
        StringBuilder s = new StringBuilder(ip);

        if (isClickable)
        {
            transform.position = new Vector3(3F, 1F, -0.9F);
            if (System.UInt16.TryParse(port, out iport)
            && initSocket(s, iport) != -1)
                {
                    SceneManager.LoadScene("game");
                }
                else
		{
                GetComponent<Renderer>().material.color = Color.red;
		}
            }
        }

        void Update () {
            if (to)
            {
                transform.Rotate(Vector3.down * (Time.deltaTime * 15));
                if (transform.eulerAngles.y < 120)
                to = false;
            }
            else
            {
                transform.Rotate(Vector3.up * (Time.deltaTime * 15));
                if (transform.eulerAngles.y > 185)
                to = true;
            }
        }
    }
