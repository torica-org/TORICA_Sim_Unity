using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events; // UnityActionのために必要
using UnityEngine.SceneManagement; // `LoadScene`のために必要

public class ResultScreen : UIHelper // `UIHelper`を継承し，クラスに含まれるメンバ関数を利用
{

    void Start()
    {
        InitUIHelper();
    }


    public void ShowResultTwoGraphs() // 距離と`Retry`ボタン，グラフを2つ表示
    {
        // ボタンに登録するデリゲートをラムダ式で定義
        UnityAction OnClickChangePage = () =>
        {
            UIManager.screen = UIManager.Screens.ResultFourGraphs; // `ResultFourGraphs`に遷移
        };

        UnityAction OnClickReloadScene = () =>
        {
            SceneManager.LoadScene("FlightScene"); // `FlightScene`を再読み込み
        };

        /* フリーズした
        RectTransform sliderTest = NewSliderRect(basePanel, "SliderTest", ref gm.massLeftFactor, 0.0f, 1.0f, 0.5f);
        sliderTest.anchoredPosition = new Vector2(30, 200);

        RectTransform dynamicText = NewDynamicFloatTextRect(basePanel, "TextDist", ref gm.massLeftFactor, 150.0f);
        dynamicText.anchoredPosition = new Vector2(0, 200);
        */

        // `>`ボタンの生成
        RectTransform buttonChangePageRightRect = NewButtonRect(basePanel, "ButtonChangePageRight", ">", 50.0f, OnClickChangePage);
        buttonChangePageRightRect.anchorMin = new Vector2(1f, 0.5f); // アンカーの最小値
        buttonChangePageRightRect.anchorMax = new Vector2(1f, 0.5f); // アンカーの最大値
        buttonChangePageRightRect.pivot = new Vector2(1f, 0.5f); // ピボット（ボタン自身の基準点）
        buttonChangePageRightRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 50); // RectTransformのx軸方向のサイズを変更する
        buttonChangePageRightRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 150); // RectTransformのy軸方向のサイズを変更する
        buttonChangePageRightRect.anchoredPosition = new Vector2(-20, 0); // アンカーを基準にした座標 (pos_x, pos_y) を設定

        // `<`ボタンの生成
        RectTransform buttonChangePageLeftRect = NewButtonRect(basePanel, "ButtonChangePageLeft", "<", 50.0f, OnClickChangePage);
        buttonChangePageLeftRect.anchorMin = new Vector2(0f, 0.5f); // アンカーの最小値
        buttonChangePageLeftRect.anchorMax = new Vector2(0f, 0.5f); // アンカーの最大値
        buttonChangePageLeftRect.pivot = new Vector2(0f, 0.5f); // ピボット（ボタン自身の基準点）
        buttonChangePageLeftRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 50); // RectTransformのx軸方向のサイズを変更する
        buttonChangePageLeftRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 150); // RectTransformのy軸方向のサイズを変更する
        buttonChangePageLeftRect.anchoredPosition = new Vector2(20, 0); // アンカーを基準にした座標 (pos_x, pos_y) を設定

        // 距離を表示するテキストの生成
        float Distance = (PlaneRigidbody.position - GameManager.instance.PlatformPosition).magnitude;
        if (GameManager.instance.FlightMode == "BirdmanRally") Distance -= 10f;
        string dist = Distance.ToString("0.000") + " m";
        RectTransform textDistRect = NewStaticTextRect(basePanel, "TextDist", dist, 150.0f);
        textDistRect.anchorMin = new Vector2(0.05f, 0.75f); // アンカーの最小値
        textDistRect.anchorMax = new Vector2(0.5f, 0.75f); // アンカーの最大値
        textDistRect.pivot = new Vector2(0.5f, 0.5f); // ピボット（ボタン自身の基準点）
        textDistRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 800); // RectTransformのx軸方向のサイズを変更する
        textDistRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 400); // RectTransformのy軸方向のサイズを変更する
        textDistRect.anchoredPosition = new Vector2(0, 0); // アンカーを基準にした座標 (pos_x, pos_y) を設定

        // `Retry(R)`ボタンの生成
        RectTransform buttonRetryRect = NewButtonRect(basePanel, "ButtonRetry", "Retry(R)", 70.0f, OnClickReloadScene);
        buttonRetryRect.anchorMin = new Vector2(0.5f, 0.75f); // アンカーの最小値
        buttonRetryRect.anchorMax = new Vector2(0.95f, 0.75f); // アンカーの最大値
        buttonRetryRect.pivot = new Vector2(0.5f, 0.5f); // ピボット（ボタン自身の基準点）
        buttonRetryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 400); // RectTransformのx軸方向のサイズを変更する
        buttonRetryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100); // RectTransformのy軸方向のサイズを変更する
        buttonRetryRect.anchoredPosition = new Vector2(0, 0); // アンカーを基準にした座標 (pos_x, pos_y) を設定

        // 左下のグラフの生成
        RectTransform AirdataChart1Rect = NewChartRect(basePanel, "ChartAirspeed", "機速", gm.AirspeedList);
        AirdataChart1Rect.anchorMin = new Vector2(0.05f, 0.01f); // アンカーの最小値
        AirdataChart1Rect.anchorMax = new Vector2(0.49f, 0.49f); // アンカーの最大値
        AirdataChart1Rect.pivot = new Vector2(0.5f, 0.5f); // ピボット（ボタン自身の基準点）
        AirdataChart1Rect.anchoredPosition = new Vector2(0, 0); // アンカーを基準にした座標 (pos_x, pos_y) を設定

        // 右下のグラフの生成
        RectTransform AirdataChart2Rect = NewChartRect(basePanel, "ChartAlt", "高度", gm.AltList);
        AirdataChart2Rect.anchorMin = new Vector2(0.51f, 0.01f); // アンカーの最小値
        AirdataChart2Rect.anchorMax = new Vector2(0.95f, 0.49f); // アンカーの最大値
        AirdataChart2Rect.pivot = new Vector2(0.5f, 0.5f); // ピボット（ボタン自身の基準点）
        AirdataChart2Rect.anchoredPosition = new Vector2(0, 0); // アンカーを基準にした座標 (pos_x, pos_y) を設定

        print("ShowResultTwoGraphs");
    }


    public void ShowResultFourGraphs() // グラフを4つ表示
    {
        // ボタンに登録するデリゲートをラムダ式で定義
        UnityAction OnClickChangePage = () =>
        {
            UIManager.screen = UIManager.Screens.ResultTwoGraphs; // `ResultTwoGraphs`に遷移
        };

        // `>`ボタンの生成
        RectTransform buttonChangePageRightRect = NewButtonRect(basePanel, "ButtonChangePageRight", ">", 50.0f, OnClickChangePage);
        buttonChangePageRightRect.anchorMin = new Vector2(1f, 0.5f); // アンカーの最小値
        buttonChangePageRightRect.anchorMax = new Vector2(1f, 0.5f); // アンカーの最大値
        buttonChangePageRightRect.pivot = new Vector2(1f, 0.5f); // ピボット（ボタン自身の基準点）
        buttonChangePageRightRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 50); // RectTransformのx軸方向のサイズを変更する
        buttonChangePageRightRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 150); // RectTransformのy軸方向のサイズを変更する
        buttonChangePageRightRect.anchoredPosition = new Vector2(-20, 0); // アンカーを基準にした座標 (pos_x, pos_y) を設定

        // `<`ボタンの生成
        RectTransform buttonChangePageLeftRect = NewButtonRect(basePanel, "ButtonChangePageLeft", "<", 50.0f, OnClickChangePage);
        buttonChangePageLeftRect.anchorMin = new Vector2(0f, 0.5f); // アンカーの最小値
        buttonChangePageLeftRect.anchorMax = new Vector2(0f, 0.5f); // アンカーの最大値
        buttonChangePageLeftRect.pivot = new Vector2(0f, 0.5f); // ピボット（ボタン自身の基準点）
        buttonChangePageLeftRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 50); // RectTransformのx軸方向のサイズを変更する
        buttonChangePageLeftRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 150); // RectTransformのy軸方向のサイズを変更する
        buttonChangePageLeftRect.anchoredPosition = new Vector2(20, 0); // アンカーを基準にした座標 (pos_x, pos_y) を設定

        // 左上のグラフの生成
        RectTransform AirdataChart1Rect = NewChartRect(basePanel, "ChartPitchAlpha", "ピッチ(theta)", gm.ThetaList, "迎角(alpha)", gm.AlphaList);
        AirdataChart1Rect.anchorMin = new Vector2(0.05f, 0.51f); // アンカーの最小値
        AirdataChart1Rect.anchorMax = new Vector2(0.49f, 0.99f); // アンカーの最大値
        AirdataChart1Rect.pivot = new Vector2(0.5f, 0.5f); // ピボット（ボタン自身の基準点）
        AirdataChart1Rect.anchoredPosition = new Vector2(0, 0); // アンカーを基準にした座標 (pos_x, pos_y) を設定

        // 右上のグラフの生成
        RectTransform AirdataChart2Rect = NewChartRect(basePanel, "ChartRollBeta", "ロール(phi)", gm.PhiList, "横滑り角(beta)", gm.BetaList);
        AirdataChart2Rect.anchorMin = new Vector2(0.51f, 0.51f); // アンカーの最小値
        AirdataChart2Rect.anchorMax = new Vector2(0.95f, 0.99f); // アンカーの最大値
        AirdataChart2Rect.pivot = new Vector2(0.5f, 0.5f); // ピボット（ボタン自身の基準点）
        AirdataChart2Rect.anchoredPosition = new Vector2(0, 0); // アンカーを基準にした座標 (pos_x, pos_y) を設定

        //左下のグラフの生成
        RectTransform AirdataChart3Rect = NewChartRect(basePanel, "ChartCenterOfG", "全備重心", gm.CenterOfGList);
        AirdataChart3Rect.anchorMin = new Vector2(0.05f, 0.01f); // アンカーの最小値
        AirdataChart3Rect.anchorMax = new Vector2(0.49f, 0.49f); // アンカーの最大値
        AirdataChart3Rect.pivot = new Vector2(0.5f, 0.5f); // ピボット（ボタン自身の基準点）
        AirdataChart3Rect.anchoredPosition = new Vector2(0, 0); // アンカーを基準にした座標 (pos_x, pos_y) を設定

        // 右下のグラフの生成
        RectTransform AirdataChart4Rect = NewChartRect(basePanel, "ChartRudder", "ラダー", gm.drList);
        AirdataChart4Rect.anchorMin = new Vector2(0.51f, 0.01f); // アンカーの最小値
        AirdataChart4Rect.anchorMax = new Vector2(0.95f, 0.49f); // アンカーの最大値
        AirdataChart4Rect.pivot = new Vector2(0.5f, 0.5f); // ピボット（ボタン自身の基準点）
        AirdataChart4Rect.anchoredPosition = new Vector2(0, 0); // アンカーを基準にした座標 (pos_x, pos_y) を設定
    }
}
