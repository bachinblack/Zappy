using UnityEngine;
using UnityEngine.UI;
using System;

namespace cakeslice
{
    public class Highlight : MonoBehaviour
    {
        private GameObject hud;
        private Text Name;
        private Text Nb;
        private Transform parent;
        
        void Start()
        {
            GetComponent<Outline>().enabled = false;
            parent = transform.parent;
            hud = GameObject.Find("Hud");
            Text[] txt = hud.GetComponentsInChildren<Text>();
            Name = txt[0];
            Nb = txt[1];
            Name.text = "lol";
        }

        private void OnMouseEnter()
        {
            GetComponent<Outline>().enabled = true;
            if (Data.camType == 1)
            {
                hud.SetActive(true);
                Name.text = parent.name;
                if (parent.name != "Egg")
                {
                    if (parent.transform.localScale.x == 0.1F)
                        Nb.text = "0";
                    else
                        Nb.text = (parent.transform.localScale.x / 3F * 10F).ToString();
                }
            }
        }
        private void OnMouseExit()
        {
            GetComponent<Outline>().enabled = false;
            hud.SetActive(false);
        }
    }
}