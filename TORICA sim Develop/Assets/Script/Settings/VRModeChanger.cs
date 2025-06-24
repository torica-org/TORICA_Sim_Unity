using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRModeChanger : MonoBehaviour
{
    public void ChangeVRMode()
    {
        MyGameManeger.instance.VRMode = !MyGameManeger.instance.VRMode;
    }
}
