using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HUDController : MonoBehaviour
{
    //private GameObject HUD;
    private GameObject HorizontalLine;
    private GameObject HUDCanvas;
    //private GameObject SideViewCamera;

    // Start is called before the first frame update
    void Start()
    {
        HUDCanvas = GameObject.Find("HUD");
        //HorizontalLine = GameObject.Find("HUD").transform.Find("HorizontalLine").gameObject;
        //SideViewCamera = GameObject.Find("SideViewCamera");
        
        HUDCanvas.SetActive(Config.ShowHUD);
        //HorizontalLine.SetActive(GameManager.instance.HorizontalLineActive);
        //SideViewCamera.SetActive(GameManager.instance.HUDActive);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("h")){
            Config.ShowHUD = !Config.ShowHUD;
            HUDCanvas.SetActive(Config.ShowHUD);
            //SideViewCamera.SetActive(GameManager.instance.HUDActive);
        }

        if(Config.MainCamera && Input.GetKeyDown("l")){
            //HorizontalLine.SetActive(!GameManager.instance.HorizontalLineActive);
            //GameManager.instance.HorizontalLineActive = !GameManager.instance.HorizontalLineActive;
        }
    }
}
