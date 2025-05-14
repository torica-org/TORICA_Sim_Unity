using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class FlightSettingController : MonoBehaviour
{
    private GameObject FlightSetting;
    private SaveCsvScript SaveCsvScript;
    private AerodynamicCalculator script;//AerodynamicCalculatorスクリプトにアクセスするための変数
    private bool OnStartTrigger;

    // Start is called before the first frame update
    
    public void OnEnables()
    {
        FlightSetting = GameObject.Find("FlightSetting");

        MyGameManeger.instance.FlightSettingActive = true;
        FlightSetting.SetActive(MyGameManeger.instance.FlightSettingActive);
                
        Time.timeScale=(float)Convert.ToInt32(!MyGameManeger.instance.FlightSettingActive &!MyGameManeger.instance.SettingActive & !MyGameManeger.instance.Landing);
    }
    
    void Start()
    {
        script = MyGameManeger.instance.Plane.GetComponent<AerodynamicCalculator>();

        SaveCsvScript = this.GetComponent<SaveCsvScript>();
    }

    void Update()
    {
        if(-0.5f >= ((script.JoyStickNow-MyGameManeger.instance.JoyStick0)/MyGameManeger.instance.JoyStickFactor) && ((script.JoyStickNow-MyGameManeger.instance.JoyStick0)/MyGameManeger.instance.JoyStickFactor) >= -1.0f && !MyGameManeger.instance.EnterFlight){
            //OnStartTrigger = true;
        }

        if( (Input.GetButtonDown("StartButton") || OnStartTrigger) && !MyGameManeger.instance.EnterFlight){
            MyGameManeger.instance.EnterFlight = true;
            MyGameManeger.instance.FlightSettingActive = !MyGameManeger.instance.FlightSettingActive;
            FlightSetting.SetActive(MyGameManeger.instance.FlightSettingActive);
            Time.timeScale=(float)Convert.ToInt32(!MyGameManeger.instance.FlightSettingActive & !MyGameManeger.instance.Landing);
            SaveCsvScript.SetFile();
            OnStartTrigger = false;
        }
    }
}
