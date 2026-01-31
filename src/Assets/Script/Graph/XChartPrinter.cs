using System.Collections;
using System.IO;
using UnityEngine;
using XCharts.Runtime; // XChartを使うために必要

public class XChartPrinter : MonoBehaviour
{
    private Camera graphCamera; // 撮影用カメラ
    private LineChart chart;    // XChartのグラフ本体
    private Vector2Int imageSize = new Vector2Int(1920, 1440); // 保存サイズ

    private void Start ()
    {
        graphCamera = GameObject.Find("GraphCamera").GetComponent<Camera>();
        chart = GameObject.Find("ChartForPrinter").GetComponent<LineChart>();
    }

    // 外部から呼ぶ関数
    public void PrintGraph(float[] dataPoints, string fileName)
    {
        StartCoroutine(CaptureProcess(dataPoints, fileName));
    }

    private IEnumerator CaptureProcess(float[] dataPoints, string fileName)
    {
        // 1. XChartにデータをセット
        chart.ClearData();

        // シリーズ0番（折れ線など）にデータを追加
        // ※XChartsのバージョンによって書き方が若干異なる場合があります
        foreach (var val in dataPoints)
        {
            chart.AddData(0, val);
        }

        // 重要: XChartの描画更新を待つ
        // データをセットした瞬間に見た目は変わらないため、1フレーム待ちます
        chart.RefreshChart();
        yield return new WaitForEndOfFrame();
        // もし反映されない場合はもう1フレーム待つ
        // yield return null; 

        // 2. RenderTextureを作成 (使い捨て)
        RenderTexture rt = new RenderTexture(imageSize.x, imageSize.y, 24);
        graphCamera.targetTexture = rt;

        // 3. カメラで撮影（レンダリング）
        graphCamera.Render();

        // 4. RenderTextureからTexture2Dにピクセルを移す
        RenderTexture.active = rt; // ReadPixelsの読み取り元をrtに切り替え
        Texture2D texture = new Texture2D(imageSize.x, imageSize.y, TextureFormat.RGB24, false);
        texture.ReadPixels(new Rect(0, 0, imageSize.x, imageSize.y), 0, 0);
        texture.Apply();

        // 後始末
        graphCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        // 5. 保存
        SaveTexture(texture, fileName);

        Destroy(texture); // メモリ解放
    }

    // 外部から呼ぶ関数
    public void PrintGraph(LineChart linechart, string fileName)
    {
        // 2. RenderTextureを作成 (使い捨て)
        RenderTexture rt = new RenderTexture(imageSize.x, imageSize.y, 24);
        graphCamera.targetTexture = rt;

        // 3. カメラで撮影（レンダリング）
        graphCamera.Render();

        // 4. RenderTextureからTexture2Dにピクセルを移す
        RenderTexture.active = rt; // ReadPixelsの読み取り元をrtに切り替え
        Texture2D texture = new Texture2D(imageSize.x, imageSize.y, TextureFormat.RGB24, false);
        texture.ReadPixels(new Rect(0, 0, imageSize.x, imageSize.y), 0, 0);
        texture.Apply();

        // 後始末
        graphCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        // 5. 保存
        SaveTexture(texture, fileName);

        Destroy(texture); // メモリ解放
    }

    private void SaveTexture(Texture2D tex, string fileName)
    {
        // exeと同じ階層/Graphs フォルダに保存
        string dirPath = Path.Combine(Directory.GetParent(Application.dataPath).FullName, "Graphs");
        if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);

        string fullPath = Path.Combine(dirPath, fileName);
        File.WriteAllBytes(fullPath, tex.EncodeToPNG());

        Debug.Log("XChartグラフを保存しました: " + fullPath);
        System.Diagnostics.Process.Start(@dirPath);
    }
}