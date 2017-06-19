using UnityEngine.SceneManagement;
using UnityEngine;

public class Data
{
    static public int mapSize = 0;
}

public class launchGame : MonoBehaviour {

    bool to = true;
    private string ip = "IP address";
    private string port = "Port";
    private string size = "Map Size (10 to 30)";
    //public static int mapSize = 0;
    bool isClickable = false;

    void OnGUI()
    {
        ip = GUI.TextField(new Rect(150, 120, 180, 20), ip, 15);
        port = GUI.TextField(new Rect(150, 220, 180, 20), port, 5);
        size = GUI.TextField(new Rect(150, 320, 180, 20), size, 20);
    
    }
    void Start ()
    {
        Screen.SetResolution(800, 500, false);
    }

    void OnMouseEnter()
    {
        isClickable = true;
        transform.position = new Vector3(2.46F, -0.85F, -0.9F);
        GetComponent<Renderer>().material.color = Color.cyan;
    }

    void OnMouseExit()
    {
        isClickable = false;
        transform.position = new Vector3(2.46F, -0.85F, -0.8F);
        GetComponent<Renderer>().material.color = Color.white;
    }

    void OnMouseDown()
    {
        transform.position = new Vector3(2.46F, -0.85F, -0.8F);
    }

    void OnMouseUp()
    {
        if (isClickable)
        {
            transform.position = new Vector3(2.46F, -0.85F, -0.9F);
            if (!System.Int32.TryParse(size, out Data.mapSize) || Data.mapSize < 10 || Data.mapSize > 30)
            {
                GetComponent<Renderer>().material.color = Color.red;
            }
            else
                SceneManager.LoadScene("game");
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
