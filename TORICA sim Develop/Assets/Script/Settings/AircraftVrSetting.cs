using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftVrSetting : MonoBehaviour
{
    private bool nowSetting;
    [SerializeField] private GameObject VRModeObjects;
    [SerializeField] private GameObject NonVRModeObjects;

    void Start()
    {
        nowSetting = MyGameManeger.instance.VRMode;
        if (nowSetting)
        {
            //VRモード
            VRModeObjects.SetActive(true);
            NonVRModeObjects.SetActive(false);
        }
        else
        {
            //非VRモード
            VRModeObjects.SetActive(false);
            NonVRModeObjects.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (nowSetting != MyGameManeger.instance.VRMode)
        {
            nowSetting = MyGameManeger.instance.VRMode;
            if (nowSetting)
            {
                //VRモード
                VRModeObjects.SetActive(true);
                NonVRModeObjects.SetActive(false);
            }
            else
            {
                //非VRモード
                VRModeObjects.SetActive(false);
                NonVRModeObjects.SetActive(true);
            }
        }
    }
}
