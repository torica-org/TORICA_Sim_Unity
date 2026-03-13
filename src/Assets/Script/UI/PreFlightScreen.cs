using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events; // UnityActionのために必要
using UnityEngine.SceneManagement; // `LoadScene`のために必要

public class PreFlightScreen : UIHelper // `UIHelper`を継承し，クラスに含まれるメンバ関数を利用
{
    void Start()
    {
        InitUIHelper();
    }


    public void Test()
    {
        NewSlider slider = new(basePanel, "SliderTest", (x) => { gm.massLeftFactor = x; }, () => { return gm.massLeftFactor; }, 0.0f, 1.0f);
        RectTransform sliderTest = slider.GetRectTransform();
        sliderTest.anchoredPosition = new Vector2(300, 200);
        NewSlider slider1 = new(basePanel, "SliderTest", (x) => { gm.massLeftFactor = x; }, () => { return gm.massLeftFactor; }, 0.0f, 1.0f);
        RectTransform sliderTest1 = slider1.GetRectTransform();
        sliderTest1.anchoredPosition = new Vector2(300, 100);
        RectTransform dynamicText = NewDynamicFloatTextRect(basePanel, "TextDist", () => { return gm.massLeftFactor; }, 50.0f);
        dynamicText.anchoredPosition = new Vector2(0, 200);
    }
    /*
    void Update()
    {
        print(gm.massLeftFactor);
    }
    */

}
