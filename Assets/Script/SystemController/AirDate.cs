using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class AirDate : MonoBehaviour
{
    private AerodynamicCalculator script;//AerodynamicCalculatorスクリプトにアクセスするための変数
    private Rigidbody PlaneRigidbody;
    [System.NonSerialized] public decimal frameNumber;//インターバル管理用、0.02秒に1値が増加する
    [System.NonSerialized] public decimal interval=0.1m;//リストに追加する間隔[s]
    [System.NonSerialized] public int ListNumber;//リストの要素番号

    private float time;
    private StreamWriter sw;

    SaveCsvScript SaveCsvScript;

    // Start is called before the first frame update
    void Start()
    {
        script = MyGameManeger.instance.Plane.GetComponent<AerodynamicCalculator>();
        PlaneRigidbody = MyGameManeger.instance.Plane.GetComponent<Rigidbody>();

        MyGameManeger.instance.AirSpeedList.Clear();
        MyGameManeger.instance.AltList.Clear();
        MyGameManeger.instance.AlphaList.Clear();
        MyGameManeger.instance.BetaList.Clear();
        MyGameManeger.instance.ThetaList.Clear();
        MyGameManeger.instance.PhiList.Clear();
        MyGameManeger.instance.PitchGravityList.Clear();
        MyGameManeger.instance.drList.Clear();
        
        SaveCsvScript = this.GetComponent<SaveCsvScript>();
    }

    // Update is called once per frame
    void FixedUpdate()//0.02秒ごとに実行
    {
        time += Time.deltaTime;

        if(frameNumber%(interval/0.02m) == 0)//interval[s]ごとにリストに追加
        {
            // Calculate rotation
            float q1 = MyGameManeger.instance.Plane.transform.rotation.x;
            float q2 = -MyGameManeger.instance.Plane.transform.rotation.y;
            float q3 = -MyGameManeger.instance.Plane.transform.rotation.z;
            float q4 = MyGameManeger.instance.Plane.transform.rotation.w;
            float C11 = q1*q1-q2*q2-q3*q3+q4*q4;
            float C22 = -q1*q1+q2*q2-q3*q3+q4*q4;
            float C12 = 2f*(q1*q2+q3*q4);
            float C13 = 2f*(q1*q3-q2*q4);
            float C32 = 2f*(q2*q3-q1*q4);
            float phi = -Mathf.Atan(-C32/C22)*Mathf.Rad2Deg;
            float theta = -Mathf.Asin(C12)*Mathf.Rad2Deg; 
            float psi = -Mathf.Atan(-C13/C11)*Mathf.Rad2Deg; 

            //リストに追加
            MyGameManeger.instance.AirSpeedList.Add((float)Math.Round(script.Airspeed, 2,  MidpointRounding.AwayFromZero));
            MyGameManeger.instance.AltList.Add((float)Math.Round(script.ALT, 2,  MidpointRounding.AwayFromZero));
            MyGameManeger.instance.AlphaList.Add((float)Math.Round(script.alpha, 2,  MidpointRounding.AwayFromZero));
            MyGameManeger.instance.BetaList.Add((float)Math.Round(script.beta, 2,  MidpointRounding.AwayFromZero));
            MyGameManeger.instance.ThetaList.Add((float)Math.Round(theta, 2,  MidpointRounding.AwayFromZero));
            MyGameManeger.instance.PhiList.Add((float)Math.Round(phi, 2,  MidpointRounding.AwayFromZero));
            MyGameManeger.instance.PitchGravityList.Add((float)Math.Round(script.pitchGravity, 2, MidpointRounding.AwayFromZero));
            MyGameManeger.instance.drList.Add((float)Math.Round(script.dr, 2 ,MidpointRounding.AwayFromZero));
            
            /*
            MyGameManeger.instance.AirSpeedList.Add(RoundFloat(script.Airspeed, 2));
            MyGameManeger.instance.AltList.Add(RoundFloat(script.ALT, 2));
            MyGameManeger.instance.AlphaList.Add(RoundFloat(script.alpha, 2));
            MyGameManeger.instance.BetaList.Add(RoundFloat(script.beta, 2));
            MyGameManeger.instance.ThetaList.Add(RoundFloat(theta, 2)); 
            MyGameManeger.instance.PhiList.Add(RoundFloat(phi, 2));
            */
            
            ListNumber++;//リストの要素番号を進める

            if(MyGameManeger.instance.SaveCsv && MyGameManeger.instance.EnterFlight)
            {
                SaveCsvScript.SaveData(time.ToString("F1") ,script.Airspeed.ToString("F3") ,script.ALT.ToString("F3") ,script.alpha.ToString("F3") ,script.beta.ToString("F3") ,theta.ToString("F3") ,phi.ToString("F3") );
            }
        }
        frameNumber++;//0.02秒経過
    }

    float RoundFloat(float value, int decimals)
    {
        float factor = Mathf.Pow(10f, decimals);
        // 100 倍して四捨五入 → また 100 で割る
        return Mathf.Round(value * factor) / factor;
    }

}
