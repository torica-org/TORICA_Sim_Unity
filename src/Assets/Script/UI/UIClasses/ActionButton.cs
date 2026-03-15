using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events; //UnityAction使うにはこれ忘れないように


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
    public ActionButton(GameObject parent, string objectName, string displayContent, 
        float fontSize, UnityAction callback) : base() // `base()`を呼び出して`UIBase`のコンストラクタも実行.
    {
        gameObject = UIHelper.NewButtonObj(parent, objectName, displayContent, fontSize, callback); // ボタンを生成し，生成されたオブジェクトを保持.
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
