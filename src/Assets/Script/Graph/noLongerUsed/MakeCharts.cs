using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
#if INPUT_SYSTEM_ENABLED
using Input = XCharts.Runtime.InputHelper;
#endif
using XCharts.Runtime;

[DisallowMultipleComponent]
[ExecuteInEditMode]
public class MakeCharts : MonoBehaviour
{
    protected GameManager gm;
    protected GameObject systemcontroller;
    protected AirData airdata;

    protected LineChart chart;
    protected XAxis xAxis;
    protected YAxis yAxis;
    protected int i;
    protected int DataN = 1;

    protected string ChartTitle;

    private Camera graphCamera; // 撮影用カメラ
    private Vector2Int imageSize = new Vector2Int(1920, 1440); // 保存サイズ


    protected void Awake()
    {
        gm = GameManager.instance;
        systemcontroller = GameObject.Find("SystemController");
        airdata = systemcontroller.GetComponent<AirData>();

        graphCamera = GameObject.Find("GraphCamera").GetComponent<Camera>();

        chart = GameObject.Find("ChartForPrinter").GetComponent<LineChart>();
        if (chart == null)
        {
            chart = GameObject.Find("ChartForPrinter").AddComponent<LineChart>();
            chart.Init();
        }
        /*
        chart.EnsureChartComponent<Title>().show = false;
        //chart.EnsureChartComponent<Title>().text = "Line Simple";

        chart.EnsureChartComponent<Tooltip>().show = true;
        chart.EnsureChartComponent<Legend>().show = true;

        xAxis = chart.EnsureChartComponent<XAxis>();
        yAxis = chart.EnsureChartComponent<YAxis>();

        // xAxis.axisLabel.numericFormatter = "F0";
        // yAxis.axisLabel.numericFormatter = "F0";

        xAxis.show = true;
        yAxis.show = true;
        xAxis.type = Axis.AxisType.Category;
        yAxis.type = Axis.AxisType.Value;

        xAxis.splitNumber = 5;
        xAxis.boundaryGap = true;

        chart.RemoveData();
        // 設定を反映
        chart.RefreshChart();

        SetAxis();
        */
    }


    protected virtual void AddData() { }


    protected virtual void SetAxis() { }


    /*
    protected void FixedUpdate()
    {
        if(gm.EnterFlight & !gm.SettingActive & !gm.FlightSettingActive & !gm.Landing & airdata.frameNumber%(airdata.interval/0.02m) == 0){
            AddData();
        }
    }
    */

    public void AddData_Debug()
    {
        StartCoroutine(AddingProcess());
    }

    private IEnumerator AddingProcess()
    {
        chart.EnsureChartComponent<Title>().show = false;
        //chart.EnsureChartComponent<Title>().text = "Line Simple";

        chart.EnsureChartComponent<Tooltip>().show = true;
        chart.EnsureChartComponent<Legend>().show = true;

        xAxis = chart.EnsureChartComponent<XAxis>();
        yAxis = chart.EnsureChartComponent<YAxis>();

        // xAxis.axisLabel.numericFormatter = "F0";
        // yAxis.axisLabel.numericFormatter = "F0";

        xAxis.show = true;
        yAxis.show = true;
        //xAxis.type = Axis.AxisType.Category;
        //yAxis.type = Axis.AxisType.Value;

        xAxis.splitNumber = 5;
        xAxis.boundaryGap = true;

        chart.RemoveData();

        SetAxis();

        yield return new WaitForEndOfFrame();

        int loopCount = 0;
        while (true)
        {
            try
            {
                AddData();
                loopCount++;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
                Debug.Log(loopCount);
                Debug.Log(airdata.frameNumber);
                i = 0;
                break;
            }
        }

        // 変更を反映
        chart.RefreshChart();

        yield return null;

        yield return new WaitForEndOfFrame();
    }

    public void SaveGraph(string fileName)
    {
        StartCoroutine(CaptureProcess(fileName));
    }


    private IEnumerator CaptureProcess(string fileName)
    {
        int loopCount = 0;
        while (true)
        {
            try
            {
                AddData();
                loopCount++;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
                Debug.Log(loopCount);
                Debug.Log(airdata.frameNumber);
                break;
            }
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
