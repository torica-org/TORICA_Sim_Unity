using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ===== 使い方 ========================================
// ----------------------------------------------------------------------------------------------------------------------------------------------
// Chart airdataChart1 = new(basePanel, "ChartPitchAlpha", "ピッチ(theta)", gm.ThetaList, "迎角(alpha)", gm.AlphaList);
// GameObject chartObj = airdataChart1.gameObject; // プロパティにより取得.
// RectTransform AirdataChart1Rect = airdataChart1.rectTransform; // プロパティにより取得.
// ----------------------------------------------------------------------------------------------------------------------------------------------

// basePanel: 親のゲームオブジェクト.
// "ChartPitchAlpha": 生成するグラフのゲームオブジェクト名.
// "ピッチ(theta)": グラフの1つ目のデータセットの名前.
// gm.ThetaList: グラフの1つ目のListのデータセット.
// "迎角(alpha)": グラフの2つ目のListのデータセットの名前（省略可能）.
// gm.AlphaList: グラフの2つ目のListのデータセット（省略可能）.


// ===== グラフを生成するクラス ============================================================================
public class Chart : UIBase
{
    public Chart(GameObject parent, string objectName, 
        string name1, List<float> list1, 
        string name2 = null, List<float> list2 = null) : base() // `base()`を呼び出して`UIBase`のコンストラクタも実行.
    {
        gameObject = UIHelper.NewChartObj(parent, objectName, 
            name1, list1, name2, list2); // エアデータチャートを生成し，生成されたオブジェクトを保持.
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
