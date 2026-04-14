using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

// ===== 概要 =========================
// 任意の型の値を表示する静的テキストを生成する.

// ===== 使い方 =========================
// ---------------------------------------------------------------------------
// string dist = Distance.ToString("0.000") + " m";
// StaticText<string> textDist = new(basePanel, "TextDist", dist, 150.0f);
// GameObject textObj = textDist.gameObject; // プロパティにより取得.
// RectTransform textDistRect = textDist.rectTransform; // プロパティにより取得.
// ---------------------------------------------------------------------------

// basePanel: 親のゲームオブジェクト.
// "TextDist": 生成するテキストのゲームオブジェクト名.
// dist: 表示する内容（この例では距離を文字列に変換）.
// 150.0f: フォントサイズ.

// ===== 静的テキストを生成するクラス ========================================================
public class StaticText<T> : UIBase
{
    public StaticText(GameObject parent, string objectName, T displayContent, float fontSize = 0.0f)
    {
        // 生成時に直接親キャンバスを指定
        gameObject = UnityEngine.Object.Instantiate(DefaultText, parent.transform, false);

        // その他の設定
        gameObject.name = objectName;
        TextMeshProUGUI textTmp = gameObject.GetComponent<TextMeshProUGUI>();
        textTmp.enableAutoSizing = false;
        if (fontSize > 0.0f) // フォントサイズが0以下の場合はデフォルトのサイズを使用する
        {
            textTmp.fontSize = fontSize;
        }
        textTmp.alignment = TextAlignmentOptions.Center; // 中央寄せ
        textTmp.text = Formatter(displayContent); // テキストを設定.

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