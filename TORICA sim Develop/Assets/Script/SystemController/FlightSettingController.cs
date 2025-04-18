using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class FlightSettingController : MonoBehaviour
{
    private GameObject FlightSetting;
    private Slider TakeoffVelocitySlider;
    //public FlightSettingCloseButton closeButton;

    private SaveCsvScript SaveCsvScript;
    // Start is called before the first frame update
    
    public void OnEnables()
    {
        FlightSetting = GameObject.Find("FlightSetting");
        TakeoffVelocitySlider = GameObject.Find("TakeOffVelocitySlider").GetComponent<Slider>();
        
        MyGameManeger.instance.FlightSettingActive = true;
        FlightSetting.SetActive(MyGameManeger.instance.FlightSettingActive);
                
        Time.timeScale=(float)Convert.ToInt32(!MyGameManeger.instance.FlightSettingActive &!MyGameManeger.instance.SettingActive & !MyGameManeger.instance.Landing);

    }
    
    void Start()
    {
        SaveCsvScript = this.GetComponent<SaveCsvScript>();
    }

    void Update()
    {
        if(Input.GetButtonDown("StartButton") && !MyGameManeger.instance.EnterFlight){
            MyGameManeger.instance.EnterFlight = true;
            MyGameManeger.instance.FlightSettingActive = !MyGameManeger.instance.FlightSettingActive;
            FlightSetting.SetActive(MyGameManeger.instance.FlightSettingActive);
            Time.timeScale=(float)Convert.ToInt32(!MyGameManeger.instance.FlightSettingActive & !MyGameManeger.instance.Landing);
            SaveCsvScript.SetFile();
        }
    }
}
