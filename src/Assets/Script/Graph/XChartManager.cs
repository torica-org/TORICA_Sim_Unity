using System; // `IDisposable`に必要
using System.Collections;
using System.Collections.Generic; // Listを使うために必要
using System.IO;
using UnityEngine;
using XCharts.Runtime; // XChartを使うために必要

public class XChartManager : IDisposable // : MonoBehaviour
{
    protected GameManager gm;
    protected GameObject systemcontroller;
    protected AirData airdata;

    private LineChart chart;    // XChartのグラフ本体

    private XAxis xAxis;
    private YAxis yAxis;

    private Serie serie1;
    private Serie serie2;

    private const int pickedDataNum = 100; // プロットするデータの数（数に根拠はない）
    private int[] pickedIndex = new int[pickedDataNum]; // ピックするデータのインデックスを保持する配列

    private int dataCount = 0;


    public XChartManager(LineChart _chart)
    {
        gm = GameManager.instance; // `GameManager.instance`を`gm`として保持
        systemcontroller = GameObject.Find("SystemController"); // `SystemController`への参照を取得
        airdata = systemcontroller.GetComponent<AirData>(); // `AirData`

        chart = _chart;
        chart.GetChartComponent<Title>().show = false; // タイトルの表示
        chart.EnsureChartComponent<Tooltip>().show = true; // グラフ上にカーソルを当てた際の値の表示
        // chart.tooltip.animation.unscaledTime = true; // 物理演算停止中でも値の表示のアニメーションを実行
        chart.EnsureChartComponent<Legend>().show = true; // 凡例の表示

        xAxis = chart.EnsureChartComponent<XAxis>(); // X軸（横軸：時間）を取得
        xAxis.splitNumber = 7; // 表示する目盛りの数を7に設定
        xAxis.axisLabel.numericFormatter = "f0";

        yAxis = chart.EnsureChartComponent<YAxis>(); // Y軸（縦軸）を取得
        yAxis.axisLabel.numericFormatter = "f2"; // 表示する桁数を小数点以下2桁に設定
    }

    
    public void CreateChart(string name1, List<float> list1, string name2, List<float> list2)
    {
        int listCount = 1;
        if (list2 != null && name2 != null) listCount++;

        dataCount = list1.Count;
        PickIndex();

        chart.RemoveData(); // データを除去

        serie1 = chart.AddSerie<Line>(name1); // Line型のSerieを追加
        if(listCount > 1) serie2 = chart.AddSerie<Line>(name2);

        foreach (var serie in chart.series) // Serie型配列要素それぞれに対して
        {
            serie.animation.unscaledTime = true; // 物理演算停止中でもアニメーションを実行
            serie.symbol.show = false; // プロット点非表示
        }

        for (int i = 0; i < pickedDataNum; i++)
        {
            chart.AddXAxisData((0.02 * pickedIndex[i]) + "s"); // ピックしたインデックスは各時点でのフレーム数であるため
            chart.AddData(0, list1[pickedIndex[i]]); // 系列0(serie1)に追加
            if(listCount > 1) chart.AddData(1, list2[pickedIndex[i]]); // 系列1(serie2)に追加
        }

        chart.RefreshChart(); // グラフを更新;
    }


    private void PickIndex() // ピックするpickedDataNum個のインデックス（それ以下ならそのまま）の配列を返す．
    {
        if (dataCount <= pickedDataNum - 1) // データ数がpickedDataNum個以下
        {
            for (int i = 0; i < dataCount; i++)
            {
                pickedIndex[i] = i; // インデックスと各要素を一致させる
            }
            // pickedDataNum = dataCount; // pickedDataNumを更新
        }
        else // データ数がpickedDataNumより大きい
        {
            float indexInterval = (float)dataCount / (float)(pickedDataNum - 1); // pickedDataNum個に分割したときの間隔（小数点以下切り捨て）

            for (int i = 0; i < pickedDataNum - 1; i++)
            {
                if (indexInterval * (float)i > dataCount) // 最終要素が最大値を超えた場合
                {
                    pickedIndex[i] = dataCount; // 最終要素に最大値を代入
                }
                else
                {
                    pickedIndex[i] = (int)Mathf.Round(indexInterval * (float)i); // indexInterval倍の値を次々に代入
                }
                // print(pickedIndex[i]);
            }
            // print("dataCount: " + dataCount);
        }
    }

    
    public void Dispose() // usingブロックが使えるようになり，抜けると自動的に実行
    {
        pickedIndex = null; // 参照を切る -> 自動的に破棄される
    }
}
