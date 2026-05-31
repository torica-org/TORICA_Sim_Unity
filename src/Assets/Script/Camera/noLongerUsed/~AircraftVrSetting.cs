/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftVrSetting : MonoBehaviour
{
    private GameManager gm = GameManager.instance;
    private bool nowSetting;
    private GameObject VRModeObjects;
    [SerializeField] private GameObject NonVRModeObjects;
    private GameObject obj;
    private bool VREnable;

    private ManualXRControl manualXRControl;

    void Start()
    {
        #if USE_STEAMVR
        VREnable = true;
        // ここにVR用のコントローラー設定などのコードを記述
        #else
        // USE_STEAMVR シンボルが定義されていない（＝SteamVRが無効な）場合の処理
        VREnable = false;
        GameManager.instance.VRMode = false;
        // ここにPC用のカメラ設定や入力設定のコードを記述
        #endif
        if (gm.VRMode)
        {
            manualXRControl = new();
            //StartCoroutine(manualXRControl.StartXRCoroutine());
            VREnable = true;
        }
        else
        {
            VREnable = false;
        }


        // 2026/03/18
        nowSetting = GameManager.instance.VRMode;
        VRModeObjects = (GameObject)Resources.Load("VR_Item");

        if (nowSetting & VREnable)
        {
            //VRモード
            obj = Instantiate(VRModeObjects, new Vector3(VRModeObjects.transform.position.x, VRModeObjects.transform.position.y, VRModeObjects.transform.position.z), Quaternion.identity);
            NonVRModeObjects.SetActive(false);
            obj.transform.parent = GameManager.instance.Plane.transform;
            obj.transform.localPosition = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);
        }
        else
        {
            //非VRモード
            if (VREnable)
            {
                Destroy(obj);
            }
            NonVRModeObjects.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 2026/03/18
        if (nowSetting != GameManager.instance.VRMode & VREnable)
        {
            nowSetting = GameManager.instance.VRMode;
            if (nowSetting)
            {
                //VRモード
                obj = Instantiate(VRModeObjects, new Vector3(VRModeObjects.transform.position.x, VRModeObjects.transform.position.y, VRModeObjects.transform.position.z), Quaternion.identity);
                NonVRModeObjects.SetActive(false);
                obj.transform.parent = GameManager.instance.Plane.transform;
                obj.transform.localPosition = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);
            }
            else
            {
                manualXRControl.StopXR(); // XR停止
                //非VRモード
                if (VREnable)
                {
                    Destroy(obj);
                }
                NonVRModeObjects.SetActive(true);
            }
        }
    }

}
*/

