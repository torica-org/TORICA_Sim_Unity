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

    // ===============================================================================================

    //private LineChart chart;
    private Serie serie;
    private int m_DataNum = 8;

    private void OnEnable()
    {
        StartCoroutine(PieDemo());
    }

    IEnumerator PieDemo()
    {
        while (true)
        {
            StartCoroutine(AddSimpleLine());
            yield return new WaitForSeconds(2);
            StartCoroutine(ChangeLineType());
            yield return new WaitForSeconds(8);
            StartCoroutine(LineAreaStyleSettings());
            yield return new WaitForSeconds(5);
            StartCoroutine(LineArrowSettings());
            yield return new WaitForSeconds(2);
            StartCoroutine(LineSymbolSettings());
            yield return new WaitForSeconds(7);
            StartCoroutine(LineLabelSettings());
            yield return new WaitForSeconds(3);
            StartCoroutine(LineMutilSerie());
            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator AddSimpleLine()
    {
        chart = gameObject.GetComponent<LineChart>();
        if (chart == null)
        {
            chart = gameObject.AddComponent<LineChart>();
            chart.Init();
        }
        chart.GetChartComponent<Title>().text = "LineChart - 折线图";
        chart.GetChartComponent<Title>().subText = "普通折线图";

        var yAxis = chart.GetChartComponent<YAxis>();
        yAxis.minMaxType = Axis.AxisMinMaxType.Custom;
        yAxis.min = 0;
        yAxis.max = 100;

        chart.RemoveData();
        serie = chart.AddSerie<Line>("Line");

        for (int i = 0; i < m_DataNum; i++)
        {
            chart.AddXAxisData("x" + (i + 1));
            chart.AddData(0, UnityEngine.Random.Range(30, 90));
        }
        yield return new WaitForSeconds(1);
    }

    IEnumerator ChangeLineType()
    {
        chart.GetChartComponent<Title>().subText = "LineTyle - 曲线图";
        serie.lineType = LineType.Smooth;
        chart.RefreshChart();
        yield return new WaitForSeconds(1);

        chart.GetChartComponent<Title>().subText = "LineTyle - 阶梯线图";
        serie.lineType = LineType.StepStart;
        chart.RefreshChart();
        yield return new WaitForSeconds(1);

        serie.lineType = LineType.StepMiddle;
        chart.RefreshChart();
        yield return new WaitForSeconds(1);

        serie.lineType = LineType.StepEnd;
        chart.RefreshChart();
        yield return new WaitForSeconds(1);

        chart.GetChartComponent<Title>().subText = "LineTyle - 虚线";
        serie.lineStyle.type = LineStyle.Type.Dashed;
        chart.RefreshChart();
        yield return new WaitForSeconds(1);

        chart.GetChartComponent<Title>().subText = "LineTyle - 点线";
        serie.lineStyle.type = LineStyle.Type.Dotted;
        chart.RefreshChart();
        yield return new WaitForSeconds(1);

        chart.GetChartComponent<Title>().subText = "LineTyle - 点划线";
        serie.lineStyle.type = LineStyle.Type.DashDot;
        chart.RefreshChart();
        yield return new WaitForSeconds(1);

        chart.GetChartComponent<Title>().subText = "LineTyle - 双点划线";
        serie.lineStyle.type = LineStyle.Type.DashDotDot;
        chart.RefreshChart();

        serie.lineType = LineType.Normal;
        chart.RefreshChart();
    }

    IEnumerator LineAreaStyleSettings()
    {
        chart.GetChartComponent<Title>().subText = "AreaStyle 面积图";

        serie.EnsureComponent<AreaStyle>();
        serie.areaStyle.show = true;
        chart.RefreshChart();
        yield return new WaitForSeconds(1f);

        chart.GetChartComponent<Title>().subText = "AreaStyle 面积图";
        serie.lineType = LineType.Smooth;
        serie.areaStyle.show = true;
        chart.RefreshChart();
        yield return new WaitForSeconds(1f);

        chart.GetChartComponent<Title>().subText = "AreaStyle 面积图 - 调整透明度";
        while (serie.areaStyle.opacity > 0.4)
        {
            serie.areaStyle.opacity -= 0.6f * Time.deltaTime;
            chart.RefreshChart();
            yield return null;
        }
        yield return new WaitForSeconds(1);

        chart.GetChartComponent<Title>().subText = "AreaStyle 面积图 - 渐变";
        serie.areaStyle.toColor = Color.white;
        chart.RefreshChart();
        yield return new WaitForSeconds(1);
    }

    IEnumerator LineArrowSettings()
    {
        chart.GetChartComponent<Title>().subText = "LineArrow 头部箭头";
        chart.GetSerie(0).EnsureComponent<LineArrow>();
        serie.lineArrow.show = true;
        serie.lineArrow.position = LineArrow.Position.Start;
        chart.RefreshChart();
        yield return new WaitForSeconds(1);

        chart.GetChartComponent<Title>().subText = "LineArrow 尾部箭头";
        serie.lineArrow.position = LineArrow.Position.End;
        chart.RefreshChart();
        yield return new WaitForSeconds(1);
        serie.lineArrow.show = false;
    }

    /// <summary>
    /// SerieSymbol 相关设置
    /// </summary>
    /// <returns></returns>
    IEnumerator LineSymbolSettings()
    {
        chart.GetChartComponent<Title>().subText = "SerieSymbol 图形标记";
        while (serie.symbol.size < 5)
        {
            serie.symbol.size += 2.5f * Time.deltaTime;
            chart.RefreshChart();
            yield return null;
        }
        chart.GetChartComponent<Title>().subText = "SerieSymbol 图形标记 - 空心圆";
        yield return new WaitForSeconds(1);

        chart.GetChartComponent<Title>().subText = "SerieSymbol 图形标记 - 实心圆";
        serie.symbol.type = SymbolType.Circle;
        chart.RefreshChart();
        yield return new WaitForSeconds(1);

        chart.GetChartComponent<Title>().subText = "SerieSymbol 图形标记 - 三角形";
        serie.symbol.type = SymbolType.Triangle;
        chart.RefreshChart();
        yield return new WaitForSeconds(1);

        chart.GetChartComponent<Title>().subText = "SerieSymbol 图形标记 - 正方形";
        serie.symbol.type = SymbolType.Rect;
        chart.RefreshChart();
        yield return new WaitForSeconds(1);

        chart.GetChartComponent<Title>().subText = "SerieSymbol 图形标记 - 菱形";
        serie.symbol.type = SymbolType.Diamond;
        chart.RefreshChart();
        yield return new WaitForSeconds(1);

        chart.GetChartComponent<Title>().subText = "SerieSymbol 图形标记";
        serie.symbol.type = SymbolType.EmptyCircle;
        chart.RefreshChart();
        yield return new WaitForSeconds(1);
    }

    /// <summary>
    /// SerieLabel相关配置
    /// </summary>
    /// <returns></returns>
    IEnumerator LineLabelSettings()
    {
        chart.GetChartComponent<Title>().subText = "SerieLabel 文本标签";
        serie.EnsureComponent<LabelStyle>();
        chart.RefreshChart();
        while (serie.label.offset[1] < 20)
        {
            serie.label.offset = new Vector3(serie.label.offset.x, serie.label.offset.y + 20f * Time.deltaTime);
            chart.RefreshChart();
            yield return null;
        }
        yield return new WaitForSeconds(1);

        chart.RefreshChart();
        yield return new WaitForSeconds(1);

        serie.label.textStyle.color = Color.white;
        serie.label.background.color = Color.grey;
        serie.labelDirty = true;
        chart.RefreshChart();
        yield return new WaitForSeconds(1);

        serie.label.show = false;
        chart.RefreshChart();
    }

    /// <summary>
    /// 添加多条线图
    /// </summary>
    /// <returns></returns>
    IEnumerator LineMutilSerie()
    {
        chart.GetChartComponent<Title>().subText = "多系列";
        var serie2 = chart.AddSerie<Line>("Line2");
        serie2.lineType = LineType.Normal;
        for (int i = 0; i < m_DataNum; i++)
        {
            chart.AddData(1, UnityEngine.Random.Range(30, 90));
        }
        yield return new WaitForSeconds(1);

        var serie3 = chart.AddSerie<Line>("Line3");
        serie3.lineType = LineType.Normal;
        for (int i = 0; i < m_DataNum; i++)
        {
            chart.AddData(2, UnityEngine.Random.Range(30, 90));
        }
        yield return new WaitForSeconds(1);

        var yAxis = chart.GetChartComponent<YAxis>();
        yAxis.minMaxType = Axis.AxisMinMaxType.Default;
        chart.GetChartComponent<Title>().subText = "多系列 - 堆叠";
        serie.stack = "samename";
        serie2.stack = "samename";
        serie3.stack = "samename";
        chart.RefreshChart();
        yield return new WaitForSeconds(1);
    }

// ===============================================================================================

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