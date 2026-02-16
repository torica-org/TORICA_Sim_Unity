using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// ===== 以下，共存できない =====
using UnityEngine.UI; // uGUI
// using UnityEngine.UIElements; // UI Toolkit
// ===== ================ =====

using UnityEngine.Events;　//UnityAction使うにはこれ忘れないように


public class UIManager : MonoBehaviour
{
    private MyGameManeger gm = MyGameManeger.instance;

    private GameObject uiBase;
    private Canvas canvas;
    private GameObject buttonExportGraph;


    private bool LandingEventExecuted = false;

    private XChartPrinter printer;

    void Start()
    {
        uiBase = GameObject.Find("UIBase");
        canvas = uiBase.GetComponent<Canvas>();

        // 中央原点から左上に向かって正
        buttonExportGraph = InstantiateUIBtn(canvas, "Export Graph", -50, 50, OnClickedExportGraph);
        canvas.enabled = false;

        printer = GameObject.Find("XChartPrinter").GetComponent<XChartPrinter>();
    }


    // コールバック用のデリゲートを定義
    public UnityAction callback;

    public GameObject InstantiateUIBtn(Canvas _canvas, string name, float pos_x, float pos_y, UnityAction callback)
    {
        GameObject DefaultButton = (GameObject)Resources.Load("UIParts/DefaultButton");

        // 生成時に直接親キャンバスを指定し、ローカル座標を維持します
        GameObject ui_btn = Instantiate(DefaultButton, _canvas.transform, false);

        // RectTransformを取得して配置を設定します
        RectTransform rect = ui_btn.GetComponent<RectTransform>();

        // アンカーを画面の左下に設定 (0, 0)
        rect.anchorMin = new Vector2(1f, 0f);
        rect.anchorMax = new Vector2(1f, 0f);

        // ピボット（ボタン自身の基準点）も左下に設定 (0, 0)
        rect.pivot = new Vector2(1f, 0f);

        // 左下を基準にした座標 (pos_x, pos_y) を設定
        rect.anchoredPosition = new Vector2(pos_x, pos_y);

        // その他の設定
        ui_btn.name = name;
        ui_btn.transform.Find("Text").gameObject.GetComponent<TMP_Text>().text = name;

        // クリックイベントを付与
        ui_btn.GetComponent<Button>().onClick.AddListener(callback);

        return ui_btn;
    }

    void Update()
    {
        if (gm.Landing && !LandingEventExecuted) // 着水かつ未実行
        {
            OnLandingEvent();
            LandingEventExecuted = true;
        }
        else if (!gm.FlightSettingActive && !gm.Landing) // フライト中
        {
            LandingEventExecuted= false;
        }
    }

    private void OnLandingEvent()
    {
        canvas.enabled = true;
    }

    private void OnClickedExportGraph()
    {
        Debug.Log("Clicked!!!");
        printer.ExportAllGraphs();
    }
}
