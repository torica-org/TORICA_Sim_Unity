using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events; // UnityActionのために必要

public class UISystem : MonoBehaviour
{
    private VisualElement preFlight;
    private VisualElement inFlight;
    private VisualElement landing;
    private VisualElement pause;

    private void Start()
    {
        GameObject preFlightObj = GameObject.Find("PreFlightScreen");
        if (preFlightObj != null)
        {
            preFlight = preFlightObj.GetComponent<UIDocument>().rootVisualElement;
        }

        GameObject inFlightObj = GameObject.Find("InFlightScreen");
        if (inFlightObj != null)
        {
            inFlight = inFlightObj.GetComponent<UIDocument>().rootVisualElement;
        }

        GameObject landingObj = GameObject.Find("LandingScreen");
        if (landingObj != null)
        {
            landing = landingObj.GetComponent<UIDocument>().rootVisualElement;
        }

        GameObject pauseObj = GameObject.Find("PauseScreen");
        if (pauseObj != null)
        {
            pause = pauseObj.GetComponent<UIDocument>().rootVisualElement;
        }
    }

    void Update()
    {
        switch (GameManager.instance.status)
        {
            case GameManager.Status.PreFlight:
                if (preFlight != null) preFlight.style.display = DisplayStyle.Flex; // PreflightのUIを表示
                if (inFlight != null) inFlight.style.display = DisplayStyle.None; // InFlightのUIを非表示
                if (landing != null) landing.style.display = DisplayStyle.None; // LandingのUIを非表示
                if (pause != null) pause.style.display = DisplayStyle.None; // PauseのUIを非表示
                break;
            case GameManager.Status.InFlight:
                if (preFlight != null) preFlight.style.display = DisplayStyle.None; // PreflightのUIを非表示
                if (inFlight != null) inFlight.style.display = DisplayStyle.Flex; // InFlightのUIを表示
                if (landing != null) landing.style.display = DisplayStyle.None; // LandingのUIを非表示
                if (pause != null) pause.style.display = DisplayStyle.None; // PauseのUIを非表示
                break;
            case GameManager.Status.Landing:
                if (preFlight != null) preFlight.style.display = DisplayStyle.None; // PreflightのUIを非表示
                if (inFlight != null) inFlight.style.display = DisplayStyle.None; // InFlightのUIを非表示
                if (landing != null) landing.style.display = DisplayStyle.Flex; // LandingのUIを表示
                if (pause != null) pause.style.display = DisplayStyle.None; // PauseのUIを非表示
                break;
            case GameManager.Status.Pause:
                if (preFlight != null) preFlight.style.display = DisplayStyle.None; // PreflightのUIを非表示
                if (inFlight != null) inFlight.style.display = DisplayStyle.None; // InFlightのUIを非表示
                if (landing != null) landing.style.display = DisplayStyle.None; // LandingのUIを非表示
                if (pause != null) pause.style.display = DisplayStyle.Flex; // PauseのUIを表示
                break;
        }
    }
}
