using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftVrSetting : MonoBehaviour
{
    private bool nowSetting;
    private GameObject VRModeObjects;
    [SerializeField] private GameObject NonVRModeObjects;
    private GameObject obj;

    void Start()
    {
        nowSetting = MyGameManeger.instance.VRMode;
        VRModeObjects = (GameObject)Resources.Load("VR_Item");

        if (nowSetting)
        {
            //VRモード
            obj = Instantiate(VRModeObjects, new Vector3(VRModeObjects.transform.position.x,VRModeObjects.transform.position.y,VRModeObjects.transform.position.z), Quaternion.identity);
            NonVRModeObjects.SetActive(false);
            obj.transform.parent = MyGameManeger.instance.Plane.transform;
        }
        else
        {
            //非VRモード
            Destroy(obj);
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
                obj = Instantiate(VRModeObjects, new Vector3(VRModeObjects.transform.position.x,VRModeObjects.transform.position.y,VRModeObjects.transform.position.z), Quaternion.identity);
                NonVRModeObjects.SetActive(false);
                obj.transform.parent = MyGameManeger.instance.Plane.transform;
            }
            else
            {
                //非VRモード
                Destroy(obj);
                NonVRModeObjects.SetActive(true);
            }
        }
    }
}
