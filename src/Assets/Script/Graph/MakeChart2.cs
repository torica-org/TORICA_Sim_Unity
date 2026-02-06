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
public class MakeChart2 : MakeCharts
{
    protected void OnEnable()
    {
        DataN = 2;
    }

    protected override void AddData()
    {
        try{
            if(MyGameManeger.instance.PhiList.Count >= i && MyGameManeger.instance.BetaList.Count >= i)
            {
                chart.AddXAxisData(i*airdata.interval  +"s");
                //chart.AddData(0, MyGameManeger.instance.AirSpeedList[i]);
                //chart.AddData(1, MyGameManeger.instance.AltList[i]);
                chart.AddData(0, MyGameManeger.instance.PhiList[i]);
                chart.AddData(1, MyGameManeger.instance.BetaList[i]);
                i++;
            }
        }
        catch(System.Exception){

        }
    }
    
    protected override void SetAxis(){
            for(int e = 0;e < DataN;e++){
            var Chart = chart.AddSerie<Line>();
            if(e==0||e==1){
                Chart.yAxisIndex = 0;
            }
            else{
                Chart.yAxisIndex = 1;
            }
        }
    }
}
