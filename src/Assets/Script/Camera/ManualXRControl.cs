using System;
using System.Collections;
using UnityEngine;

using UnityEngine.XR.Management;

public class ManualXRControl
{
    private GameManager gm = GameManager.instance;

    public IEnumerator StartXRCoroutine()
    {
        gm.error = true;
        gm.errorText = "Initializing XR...";
        Debug.Log("Initializing XR...");

        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

        gm.error = true;
        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            gm.errorText = "Initializing XR Failed. Check Editor or Player log for details.";
            Debug.LogWarning("Initializing XR Failed. Check Editor or Player log for details.");
            throw new ArgumentNullException(nameof(XRGeneralSettings.Instance.Manager.activeLoader));
        }
        else
        {
            Debug.Log("Starting XR...");
            XRGeneralSettings.Instance.Manager.StartSubsystems();
            gm.errorText = "VR started.";
        }
    }

    public void StopXR()
    {
        gm.error = true;
        gm.errorText = "Stopping XR...";
        Debug.Log("Stopping XR...");

        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        Debug.Log("XR stopped completely.");
        gm.error = true;
        gm.errorText = "VR stopped.";
    }
}