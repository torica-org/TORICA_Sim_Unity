using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;　//UnityAction使うにはこれ忘れないように

public class ActionButton : UIBase
{
    public ActionButton(GameObject parent, string objectName, string displayContent, float fontSize, UnityAction callback) : base() // `base()`を呼び出して`stdUI`のコンストラクタも実行.
    {
        gameObject = UIHelper.NewButtonObj(parent, objectName, displayContent, fontSize, callback); // ボタンを生成し，生成されたオブジェクトを保持.
        rectTransform = gameObject.GetComponent<RectTransform>(); // RectTransformを取得.
    }
}
