using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events; // UnityActionのために必要
using UnityEngine.SceneManagement; // `LoadScene`のために必要

public class PreFlightScreen
{
    private GameManager gm = GameManager.instance;
    private GameObject basePanel;

    public PreFlightScreen(GameObject basePanel)
    {
        this.basePanel = basePanel;
    }

    public void Test()
    {
        DynamicSlider slider = new(basePanel, "SliderTestUpper", (x) => { gm.massLeftFactor = x; }, () => { return gm.massLeftFactor; }, 0.0f, 1.0f);
        slider.rectTransform.anchoredPosition = new Vector2(300, 200);
        DynamicSlider slider1 = new(basePanel, "SliderTestLower", (x) => { gm.massLeftFactor = x; }, () => { return gm.massLeftFactor; }, 0.0f, 1.0f);
        slider1.rectTransform.anchoredPosition = new Vector2(300, 100);
        DynamicText<float> dynamicText = new(basePanel, "TextDistUpper", () => { return gm.massLeftFactor; }, 50.0f);
        dynamicText.rectTransform.anchoredPosition = new Vector2(0, 200);
        DynamicText<float> dynamicText1 = new(basePanel, "TextDistLower", () => { return gm.massLeftFactor; }, 50.0f);
        dynamicText1.rectTransform.anchoredPosition = new Vector2(0, 100);
    }
    /*
    void Update()
    {
        print(gm.massLeftFactor);
    }
    */

}
