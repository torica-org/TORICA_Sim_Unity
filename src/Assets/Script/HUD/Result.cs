using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using TMPro;

public class Result : MonoBehaviour
{
    private Text scoreText;
    private Rigidbody PlaneRigidbody;
    private TMP_Text distOnly;
    private TMP_Text distOnlySimple;
    
    
    // Start is called before the first frame update
    void Start()
    {
        scoreText = this.GetComponent<Text>();
        distOnly = GameObject.Find("ResultDist").GetComponent<TMP_Text>();
        distOnlySimple = GameObject.Find("SimpleResultDist").GetComponent<TMP_Text>();
        PlaneRigidbody = MyGameManeger.instance.Plane.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float Distance = (PlaneRigidbody.position-MyGameManeger.instance.PlatformPosition).magnitude;
        if(MyGameManeger.instance.FlightMode=="BirdmanRally"){Distance-=10f;}

        string dist = Distance.ToString("0.000") + " m";
        distOnly.text = dist;
        distOnlySimple.text = dist;
        if (MyGameManeger.instance.RudderErrorMode == 0){
            
        }

        float value = (float)Math.Round(MyGameManeger.instance.RudderErrorValue,2,MidpointRounding.AwayFromZero);
        scoreText.text = "トラブル:\n";

        switch(MyGameManeger.instance.RudderErrorMode){
            case 1:
                scoreText.text += "ラダー"+value+"に固定,\n";
                break;
            case 2:
                scoreText.text += "ラダー"+value+"に確率で固定,\n";
                break;
            case 3:
                scoreText.text += "ラダー"+value+"がニュートラル,\n";
                break;
        }

        value = (float)Math.Round(MyGameManeger.instance.CenterOfMassErrorValue,2,MidpointRounding.AwayFromZero);
        if(MyGameManeger.instance.CenterOfMassErrorValue != 0){
            scoreText.text += "重心"+value+"ズレ\n";
        }

        scoreText.text += "倍率変化:";

        value = (float)Math.Round(MyGameManeger.instance.CenterOfMassRandValue,2,MidpointRounding.AwayFromZero);
        if(MyGameManeger.instance.CenterOfMassRandValue != 1){
            scoreText.text += "重心×"+value+",";
        }

        value = (float)Math.Round(MyGameManeger.instance.GustRandValue,2,MidpointRounding.AwayFromZero);
        if(MyGameManeger.instance.GustRandValue != 0){
            scoreText.text += "風+"+value+",";
        }

        value = (float)Math.Round(MyGameManeger.instance.RudderRandValue,2,MidpointRounding.AwayFromZero);
        if(MyGameManeger.instance.RudderRandValue != 1){
            scoreText.text += "ラダー×"+value;
        }

        value = (float)Math.Round(MyGameManeger.instance.CgeRandValue,2,MidpointRounding.AwayFromZero);
        if(MyGameManeger.instance.CgeRandValue != 1){
            scoreText.text += "地面効果×"+value;
        }

        
    }
}
