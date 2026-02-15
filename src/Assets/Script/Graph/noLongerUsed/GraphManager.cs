using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphManager : MonoBehaviour
{
    private XChartPrinter printer;
    private MakeChart1 chart1;
    private MakeChart2 chart2;
    private MakeChart3 chart3;
    private MakeChart4 chart4;
    private MakeChart5 chart5;

    void Start()
    {
        printer = GameObject.Find("XChartPrinter").GetComponent<XChartPrinter>();
        chart1 = GameObject.Find("ChartForPrinter").AddComponent<MakeChart1>();
        /*
        // テストデータ作成
        float[] data = new float[100];
        for (int i = 0; i < 100; i++) data[i] = Random.Range(0f, 100f);
        printer.PrintGraph(data, "Chart1.png");

        // テストデータ作成
        for (int i = 0; i < 100; i++) data[i] = Random.Range(0f, 100f);
        printer.PrintGraph(data, "Chart2.png");

        // テストデータ作成
        for (int i = 0; i < 100; i++) data[i] = Random.Range(0f, 100f);
        printer.PrintGraph(data, "Chart3.png");

        // テストデータ作成
        for (int i = 0; i < 100; i++) data[i] = Random.Range(0f, 100f);
        printer.PrintGraph(data, "Chart4.png");

        // テストデータ作成
        for (int i = 0; i < 100; i++) data[i] = Random.Range(0f, 100f);
        printer.PrintGraph(data, "Chart5.png");
        */
    }

    /*
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 40, 80, 20), "Export Graph"))
        {
            // chart1.SaveGraph("Theta_Alpha.png");
            chart1.AddData_Debug();
        }

        if (GUI.Button(new Rect(10, 70, 80, 20), "Export Graph"))
        {
            printer.CallIEnumerator();
        }
    }
    */
}