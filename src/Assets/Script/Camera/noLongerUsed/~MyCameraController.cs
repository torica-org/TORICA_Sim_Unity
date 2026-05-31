/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour
{
    private GameObject HorizontalLine;
    private Camera FPSCamera; // Camera
    private Camera TPSCamera; // Camera
    //private Camera SideCamera;;
    private bool VRModeNow;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("displays connected: " + Display.displays.Length);


        // 2026/03/18
        // Display 1 は自動でつくので、Display 2 以降をループで有効化する
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }

        if (GameManager.instance.VRMode)
        {
            FPSCamera = GameManager.instance.Plane.transform.Find("VR_Item(Clone)/[CameraRig]/FPSCamera").gameObject.GetComponent<Camera>();
        }
        else
        {
            FPSCamera = GameManager.instance.Plane.transform.Find("FPSCamera").gameObject.GetComponent<Camera>();
        }


        FPSCamera = GameManager.instance.Plane.transform.Find("FPSCamera").gameObject.GetComponent<Camera>();
        TPSCamera = GameManager.instance.Plane.transform.Find("TPSCamera").gameObject.GetComponent<Camera>();
        //SideCamera = GameObject.Find("SideViewCamera");
        //HorizontalLine = GameObject.Find("HUD").transform.Find("HorizontalLine").gameObject;
        VRModeNow = GameManager.instance.VRMode;

        //GameManager.instance.CameraSwitch = true:FPS false:TPS
        //TPSCamera.enabled = !GameManager.instance.CameraSwitch;
        //FPSCamera.enabled = GameManager.instance.CameraSwitch;

        TPSCamera.enabled = true;
        FPSCamera.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("v")){SwitchCamera();}

        if(VRModeNow != GameManager.instance.VRMode){
            if(GameManager.instance.VRMode){
                //FPSCamera = GameManager.instance.Plane.transform.Find("VR_Item(Clone)/[CameraRig]/FPSCamera").gameObject.GetComponent<Camera>();
                VRModeNow = GameManager.instance.VRMode;
                //TPSCamera.enabled = !GameManager.instance.CameraSwitch;
                //FPSCamera.enabled = GameManager.instance.CameraSwitch;
            }else{
                //FPSCamera = GameManager.instance.Plane.transform.Find("FPSCamera").gameObject.GetComponent<Camera>();
                VRModeNow = GameManager.instance.VRMode;
                //TPSCamera.enabled = !GameManager.instance.CameraSwitch;
                //FPSCamera.enabled = GameManager.instance.CameraSwitch;
            }
        }

    }

    void SwitchCamera()
    {
        //FPSCamera.enabled = !FPSCamera.enabled;
        //TPSCamera.enabled = !TPSCamera.enabled;
        Config.IsMainDisplayTPS = !Config.IsMainDisplayTPS;

        if (Config.IsMainDisplayTPS)
        {
            TPSCamera.targetDisplay = 0;
            FPSCamera.targetDisplay = 1;
        }
        else
        {
            TPSCamera.targetDisplay = 1;
            FPSCamera.targetDisplay = 0;
        }

        //GameManager.instance.HorizontalLineActive = GameManager.instance.HorizontalLineActive & !GameManager.instance.CameraSwitch;
        //HorizontalLine.SetActive(GameManager.instance.HorizontalLineActive);
    }
}
*/
