using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

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
    public StaticText(GameObject parent, string objectName, T displayContent, float fontSize) : base() // `base()`を呼び出して`UIBase`のコンストラクタも実行.
    {
        gameObject = UIHelper.NewTextObj(parent, objectName, fontSize); // 静的テキストを生成し，生成されたオブジェクトを保持.
        gameObject.GetComponent<TextMeshProUGUI>().text = UIHelper.Formatter(displayContent); // テキストを設定.
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
