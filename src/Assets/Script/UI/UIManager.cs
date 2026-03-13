using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ===== 以下，共存できない =====
using UnityEngine.UI; // uGUI
// using UnityEngine.UIElements; // UI Toolkit
// ===== ================ =====
using TMPro;
using UnityEngine.Events;　//UnityAction使うにはこれ忘れないように
using UnityEngine.SceneManagement;


public class UIManager : UIHelper
{
    [TextArea(5, 15)]
    [Tooltip("備忘録や仕様のメモなどを自由に書き込めます")]
    public string note = "BasePanelにボタンやXChartをInstantiateして，UIを組み立てる方式です．" +
    "ページが切り替わっている用に見えて，同じPanel上で組み立て直しています．" +
    "`UIHelper.cs`にUI作成用のヘルパー関数があります．";


    private GameObject um;
    private Canvas canvas;

    private PreFlightScreen preFlight;
    private ResultScreen result;

    private bool isPause = false; // 一時停止状態を示すフラグ.


    // ===== 画面管理 ===========================================
    public enum Screens
    {
        None, // 非表示.
        InFlight, // フライト中.

        // ===== ResultScreenのページ. =====
        ResultTwoGraphs, // 結果（グラフ2つ）.
        ResultFourGraphs, // 結果（グラフ4つ）.

        // ===== PreFlightScreenのページ. =====
        PreFlightTest, // フライト前の設定画面.
    }

    [System.NonSerialized] private Screens previousScreen = Screens.None; // 前回の画面状態を保存する変数.
    public static Screens screen = Screens.None;
    // ==========================================================


    void Start()
    {
        InitUIHelper();

        um = this.gameObject;
        um.AddComponent<UIHelper>();
        basePanel = GameObject.Find("BasePanel");
        canvas = um.GetComponent<Canvas>();
        if (canvas == null)
        {
            canvas = um.AddComponent<Canvas>();
        }
        if(um.GetComponent<CanvasScaler>() == null)
        {
            CanvasScaler scaler = um.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920f, 1080f);
        }

        canvas.enabled = true;

        preFlight = um.AddComponent<PreFlightScreen>(); // `PreFlightScreen`をコンポーネントに追加
        result = um.AddComponent<ResultScreen>(); // `ResultScreen`をコンポーネントに追加.
       
        screen = Screens.PreFlightTest;
        // screen = Screens.Result;
    }

    void Update()
    {
        if (gm.FlightSettingActive)
        {
            screen = Screens.PreFlightTest;
        }
        else if (!gm.FlightSettingActive && !gm.Landing) // フライト中.
        {
            screen = Screens.InFlight;
            canvas.enabled = false;
        }
        else if (gm.Landing && screen == Screens.InFlight) // 着水.
        {
            canvas.enabled = true;
            screen = Screens.ResultTwoGraphs;
        }

        RefleshScreen();
    }


    public void RefleshScreen()
    {
        if (screen != previousScreen)
        {
            previousScreen = screen; // 現在の画面状態を保存.

            DestroyAllChildren(basePanel); // `basePanel`上の全ての子オブジェクトを破棄.

            switch (screen)
            {
                case Screens.InFlight:
                    break;
                case Screens.ResultTwoGraphs:
                    result.ShowResultTwoGraphs();
                    break;
                case Screens.ResultFourGraphs:
                    result.ShowResultFourGraphs();
                    break;
                case Screens.PreFlightTest:
                    preFlight.Test();
                    break;
                default:
                    break;
            } // switch (screen)

        } // if (isScreenChanged)

    } // RefleshScreen()


}
