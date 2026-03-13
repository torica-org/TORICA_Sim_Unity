using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class AirData : MonoBehaviour
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
        script = GameManager.instance.Plane.GetComponent<AerodynamicCalculator>();
        PlaneRigidbody = GameManager.instance.Plane.GetComponent<Rigidbody>();

        GameManager.instance.AirspeedList.Clear();
        GameManager.instance.AltList.Clear();
        GameManager.instance.AlphaList.Clear();
        GameManager.instance.BetaList.Clear();
        GameManager.instance.ThetaList.Clear();
        GameManager.instance.PhiList.Clear();
        GameManager.instance.CenterOfGList.Clear();
        GameManager.instance.drList.Clear();
        
        SaveCsvScript = this.GetComponent<SaveCsvScript>();
    }

    // Update is called once per frame
    void FixedUpdate()//0.02秒ごとに実行
    {
        time += Time.deltaTime;

            // Calculate rotation
            float q1 = GameManager.instance.Plane.transform.rotation.x;
            float q2 = -GameManager.instance.Plane.transform.rotation.y;
            float q3 = -GameManager.instance.Plane.transform.rotation.z;
            float q4 = GameManager.instance.Plane.transform.rotation.w;
            float C11 = q1*q1-q2*q2-q3*q3+q4*q4;
            float C22 = -q1*q1+q2*q2-q3*q3+q4*q4;
            float C12 = 2f*(q1*q2+q3*q4);
            float C13 = 2f*(q1*q3-q2*q4);
            float C32 = 2f*(q2*q3-q1*q4);
            float phi = -Mathf.Atan(-C32/C22)*Mathf.Rad2Deg;
            float theta = -Mathf.Asin(C12)*Mathf.Rad2Deg; 
            float psi = -Mathf.Atan(-C13/C11)*Mathf.Rad2Deg; 

            //リストに追加
            GameManager.instance.AirspeedList.Add((float)Math.Round(script.Airspeed, 2,  MidpointRounding.AwayFromZero));
            GameManager.instance.AltList.Add((float)Math.Round(script.ALT, 2,  MidpointRounding.AwayFromZero));
            GameManager.instance.AlphaList.Add((float)Math.Round(script.alpha, 2,  MidpointRounding.AwayFromZero));
            GameManager.instance.BetaList.Add((float)Math.Round(script.beta, 2,  MidpointRounding.AwayFromZero));
            GameManager.instance.ThetaList.Add((float)Math.Round(theta, 2,  MidpointRounding.AwayFromZero));
            GameManager.instance.PhiList.Add((float)Math.Round(phi, 2,  MidpointRounding.AwayFromZero));
            GameManager.instance.CenterOfGList.Add((float)Math.Round(script.centerOfG, 2, MidpointRounding.AwayFromZero));
            GameManager.instance.drList.Add((float)Math.Round(script.dr, 2 ,MidpointRounding.AwayFromZero));
            
            /*
            GameManager.instance.AirspeedList.Add(RoundFloat(script.Airspeed, 2));
            GameManager.instance.AltList.Add(RoundFloat(script.ALT, 2));
            GameManager.instance.AlphaList.Add(RoundFloat(script.alpha, 2));
            GameManager.instance.BetaList.Add(RoundFloat(script.beta, 2));
            GameManager.instance.ThetaList.Add(RoundFloat(theta, 2)); 
            GameManager.instance.PhiList.Add(RoundFloat(phi, 2));
            */
            
            ListNumber++;//リストの要素番号を進める

        if (frameNumber % (interval / 0.02m) == 0)//interval[s]ごとにリストに追加
        {
            if (GameManager.instance.SaveCsv && GameManager.instance.EnterFlight)
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

    /*
    void OnGUI()
    {
        if (GUILayout.Button("Show Log"))
        {
            int i = 0;
            while(true)
            {
                try
                {
                    float speed = GameManager.instance.AirspeedList[i];
                    float alt = GameManager.instance.AltList[i];
                    float alpha = GameManager.instance.AlphaList[i];
                    float beta = GameManager.instance.BetaList[i];
                    float theta = GameManager.instance.ThetaList[i]; ;
                    float phi = GameManager.instance.PhiList[i];
                    float centerOfG = GameManager.instance.CenterOfGList[i];
                    float dr = GameManager.instance.drList[i];
                    Debug.Log("speed: " + speed + ", alt: " + alt + ", alpha: " + alpha + ", beta: " + beta + ", theta: " + theta + ", phi: " + phi + ", centerOfG: " + centerOfG + ", dr: " + dr);
                    i++;
                }
                catch (System.Exception e)
                {
                    Debug.LogWarning(e.Message);
                    Debug.Log(i);
                    break;
                }
            }
        }
    }
    */

}
