using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SettingController : MonoBehaviour
{
    private GameObject Setting;
    private Slider TakeoffVelocitySlider;

    // Start is called before the first frame update
    public void OnEnables()
    {
        Setting = GameObject.Find("Setting");

        MyGameManeger.instance.SettingActive = false;
        Setting.SetActive(MyGameManeger.instance.SettingActive);
    }

    void Start(){
        //Time.timeScale=(float)Convert.ToInt32(!MyGameManeger.instance.SettingActive & !MyGameManeger.instance.Landing);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("tab") && !MyGameManeger.instance.FlightSettingActive){
            MyGameManeger.instance.SettingActive = !MyGameManeger.instance.SettingActive;
            Setting.SetActive(MyGameManeger.instance.SettingActive);
            Time.timeScale=(float)Convert.ToInt32(!MyGameManeger.instance.FlightSettingActive & !MyGameManeger.instance.SettingActive & !MyGameManeger.instance.Landing);
        }
        if(Input.GetKeyDown("c")){
            MyGameManeger.instance.MousePitchControl = !MyGameManeger.instance.MousePitchControl;
        }
    }
}
