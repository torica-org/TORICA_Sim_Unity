using System.Collections;
using System.Collections.Generic;
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
    protected GameObject systemcontroller;
    protected AirDate airdata;

    protected LineChart chart;
    protected XAxis xAxis;
    protected YAxis yAxis; 
    protected int i;
    protected int DataN = 1;

    protected string ChartTitle;

    protected void Start()
    {
        systemcontroller = GameObject.Find("SystemController");
        airdata = systemcontroller.GetComponent<AirDate>();
    
        chart = gameObject.GetComponent<LineChart>();
        if (chart == null)
        {
            chart = gameObject.AddComponent<LineChart>();
            chart.Init();
        }
        chart.EnsureChartComponent<Title>().show = false;
        //chart.EnsureChartComponent<Title>().text = "Line Simple";

        chart.EnsureChartComponent<Tooltip>().show = true;
        chart.EnsureChartComponent<Legend>().show = true;

        xAxis = chart.EnsureChartComponent<XAxis>();
        yAxis = chart.EnsureChartComponent<YAxis>();

        /*
        xAxis.axisLabel.numericFormatter = "F0";
        yAxis.axisLabel.numericFormatter = "F0";
        */
        
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
    }

    protected void FixedUpdate()
    {
        if(MyGameManeger.instance.EnterFlight & !MyGameManeger.instance.SettingActive & !MyGameManeger.instance.FlightSettingActive & !MyGameManeger.instance.Landing & airdata.frameNumber%(airdata.interval/0.02m) == 0){
            AddData();
        }
    }

    protected virtual void AddData(){}

    protected virtual void SetAxis(){}
}