using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading; // 並列処理のために必要
using System.Threading.Tasks; // `Task`型を使用するために必要
using XCharts.Runtime; // XChartを使うために必要


// ===== 概要 ========================================
// データリストとデータ系列名を渡してインスタンス化し，グラフを生成する.
// 実際のグラフの描写処理は`XChartManager`クラスに任せる（`SetChartData`メソッド内で呼び出す）.

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
        string name2 = null, List<float> list2 = null)
    {
        CanvasGroup cg = AirdataChart.GetComponent<CanvasGroup>();
        if (!cg)
        {
            cg = AirdataChart.AddComponent<CanvasGroup>();
        }
        cg.alpha = 0; // 透明度を0（透明）に.

        gameObject = UnityEngine.Object.Instantiate(AirdataChart, parent.transform, false); // 生成時に直接親キャンバスを指定.
        gameObject.name = objectName;

        SetChartData(gameObject, name1, list1, name2, list2);

        rectTransform = gameObject.GetComponent<RectTransform>(); // RectTransformを取得.

        _instances.Add(this); // インスタンスをリストに追加.
    }


    private async void SetChartData(GameObject chartObj, string name1, List<float> list1, string name2, List<float> list2) // グラフの描写処理は非同期的に行う（安定化のため）
    {
        if (!chartObj) return;

        using (XChartManager cm = new XChartManager(chartObj.transform.Find("LineChart").GetComponent<LineChart>())) // インスタンスはこの`using`ブロック内でのみ有効
        {
            cm.CreateChart(name1, list1, name2, list2);
        }
        ; // `using`ブロックを出ると，インスタンスは安全に破棄される

        CanvasGroup cg = chartObj.GetComponent<CanvasGroup>();

        while (cg.alpha < 1 && cg != null)
        {
            await Task.Delay(10); // 0.01秒待機
            if (!cg)
            {
                break;
            }
            cg.alpha += 0.05f; // フェードイン（サイズ設定が遅れ，画面が白くフラッシュする対策として実装．かなりそれっぽい．）.
        }
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
