using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events; //UnityAction使うにはこれ忘れないように
using UnityEngine.UI;
using TMPro;


// ===== 概要 ======================================
// クリックイベント付きのボタンを生成する.
// インスタンス化の際に，クリックイベントのコールバック関数を渡す.


// ===== 使い方 ======================================
// ----------------------------------------
// ActionButton buttonChangePageRight = new(basePanel, "ButtonChangePageRight", ">", 50.0f, OnClickChangePage);
// Gameobject buttonObj = buttonChangePageRight.gameObject; // プロパティにより取得.
// RectTransform buttonChangePageRightRect = buttonChangePageRight.rectTransform; // プロパティにより取得.
// -----------------------------------------------------------------------------

// basePanel: 親のゲームオブジェクト.
// "ButtonChangePageRight": 生成するボタンのゲームオブジェクト名.
// ">": ボタンに表示するテキスト.
// 50.0f: フォントサイズ.
// OnClickChangePage: ボタンがクリックされたときに呼び出される関数.



// ===== クリックイベント付きのボタンを生成するクラス =====================================================
public class ActionButton : UIBase
{
    public ActionButton(GameObject parent, string objectName, string displayContent, UnityAction callback) // オーバーロード.
        : this(parent, objectName, displayContent, 0.0f, callback) // フォントサイズのデフォルト値を0.0fに設定
    {
        // フォントサイズを指定しない場合は、0.0fを渡してもう一つのコンストラクタを呼び出す.
    }

    public ActionButton(GameObject parent, string objectName, string displayContent, float fontSize, UnityAction callback)
    {
        // 生成時に直接親キャンバスを指定
        gameObject = UnityEngine.Object.Instantiate(DefaultButton, parent.transform, false);

        // その他の設定
        gameObject.name = objectName;
        TextMeshProUGUI btnTmp = gameObject.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
        btnTmp.text = displayContent;
        btnTmp.enableAutoSizing = false;

        if (fontSize > 0.0f) // フォントサイズが0以下の場合はデフォルトのサイズを使用する
        {
            btnTmp.fontSize = fontSize;
        }

        // クリックイベントを付与
        // コールバックはデリゲート(Unity Action)により取得
        gameObject.GetComponent<Button>().onClick.AddListener(callback);

        rectTransform = gameObject.GetComponent<RectTransform>(); // RectTransformを取得.

        _instances.Add(this); // インスタンスをリストに追加.
    }

    public override void Dispose()
    {
        _mainThreadContext.Post(_ =>
        {
            UnityEngine.Object.Destroy(gameObject);
            gameObject = null;
            rectTransform = null;
        }, null);
    }
}
