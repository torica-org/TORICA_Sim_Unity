using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class StaticText : UIBase
{
    public StaticText(GameObject parent, string objectName, string displayContent, float fontSize) : base() // `base()`を呼び出して`stdUI`のコンストラクタも実行.
    {
        gameObject = UIHelper.NewTextObj(parent, objectName, fontSize); // 静的テキストを生成し，生成されたオブジェクトを保持.
        gameObject.GetComponent<TextMeshProUGUI>().text = displayContent; // テキストを設定.
        rectTransform = gameObject.GetComponent<RectTransform>(); // RectTransformを取得.
    }
}
