using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events; // UnityActionのために必要
using UnityEngine.SceneManagement; // `LoadScene`のために必要

public class PreFlightScreen
{
    private GameManager gm = GameManager.instance;
    private GameObject basePanel;
    private GameObject baseScrollView;
    private GameObject scrollContent; // `Scroll View`を作成したときに自動的にできる子オブジェクト.


    public PreFlightScreen(GameObject basePanel, GameObject baseScrollView)
    {
        this.basePanel = basePanel;
        this.baseScrollView = baseScrollView;
        scrollContent = baseScrollView.transform.Find("Viewport/Content").gameObject; // `Scroll View`の子オブジェクトである`Viewport/Content`を探して代入.
        // scrollContentの子オブジェクトはスクロールできる.
        // Content Size Fitterをアタッチすると，Contentのサイズが子オブジェクトに合わせて自動的に変わるので，スクロールできる範囲も自動的に変わる.
        // スクロールしたい方を`Preffered Size`にして，もう片方を`Unconstrained`にするのが一般的. 今回は縦スクロールなので，縦を`Preferred Size`，横を`Unconstrained`にするのが良いと思う.
        // `Layout Group`がないと上手く動かないっぽい.
    }

    public void Test()
    {
        StaticText<string> staticText = new(basePanel, "StaticTextTest", "カテゴリー", 25.0f);
        staticText.rectTransform.anchoredPosition = new Vector2(-500, 300);

        DynamicText<float> dynamicText = new(scrollContent, "TextDistUpper", () => { return gm.massLeftFactor; }, 50.0f);
        GameObject textObj = dynamicText.gameObject;
        RectTransform textRect = dynamicText.rectTransform;
        textRect.anchoredPosition = new Vector2(-200, 200);

        DynamicSlider slider = new(scrollContent, "SliderTestUpper", 
            (x) => { gm.massLeftFactor = x; }, () => { return gm.massLeftFactor; }, 0.0f, 1.0f, 0.1f);
        GameObject sliderObj = slider.gameObject;
        RectTransform sliderRect = slider.rectTransform;
        sliderRect.anchoredPosition = new Vector2(300, 200);
        sliderRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 300); // RectTransformのx軸方向のサイズを変更する


        DynamicText<float> dynamicText1 = new(scrollContent, "TextDistLower", () => { return gm.massRightFactor; }, 50.0f);
        dynamicText1.rectTransform.anchoredPosition = new Vector2(0, 100);

        DynamicSlider slider1 = new(scrollContent, "SliderTestLower", 
            (x) => { gm.massLeftFactor = x; }, () => { return gm.massLeftFactor; }, 0.0f, 1.0f, 0.1f);
        slider1.rectTransform.anchoredPosition = new Vector2(300, 100);

        DynamicSlider slider2 = new(scrollContent, "SliderTestLower",
            (x) => { gm.massLeftFactor = x; }, () => { return gm.massLeftFactor; }, 0.0f, 1.0f, 0.1f);
        slider2.rectTransform.anchoredPosition = new Vector2(300, -500);



    }
    /*
    void Update()
    {
        print(gm.massLeftFactor);
    }
    */

}
