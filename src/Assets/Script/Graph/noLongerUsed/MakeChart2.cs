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
            if(GameManager.instance.PhiList.Count >= i && GameManager.instance.BetaList.Count >= i)
            {
                chart.AddXAxisData(i*airdata.interval  +"s");
                //chart.AddData(0, GameManager.instance.AirspeedList[i]);
                //chart.AddData(1, GameManager.instance.AltList[i]);
                chart.AddData(0, GameManager.instance.PhiList[i]);
                chart.AddData(1, GameManager.instance.BetaList[i]);
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
