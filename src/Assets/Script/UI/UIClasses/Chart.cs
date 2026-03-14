using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chart : UIBase
{
    public Chart(GameObject parent, string objectName, 
        string name1, List<float> list1, 
        string name2 = null, List<float> list2 = null) : base() // `base()`を呼び出して`stdUI`のコンストラクタも実行.
    {
        gameObject = UIHelper.NewChartObj(parent, objectName, 
            name1, list1, name2, list2); // エアデータチャートを生成し，生成されたオブジェクトを保持.
        rectTransform = gameObject.GetComponent<RectTransform>(); // RectTransformを取得.
    }
}
