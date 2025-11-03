using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class FlightSettingCloseButton : MonoBehaviour
{
    private GameObject FlightSetting;
    //[System.NonSerialized] public bool firstPush = false;
    [SerializeField] private SaveCsvScript SaveCsvScript;

    // Start is called before the first frame update
    void Start()
    {
        FlightSetting = GameObject.Find("FlightSetting");
    }

    void Update()
    {

    }

    public void OnClick()
    {
        if (!MyGameManeger.instance.EnterFlight)
        {
            MyGameManeger.instance.EnterFlight = true;
            //firstPush = true;
            MyGameManeger.instance.FlightSettingActive = !MyGameManeger.instance.FlightSettingActive;
            FlightSetting.SetActive(MyGameManeger.instance.FlightSettingActive);
            Time.timeScale=(float)Convert.ToInt32(!MyGameManeger.instance.FlightSettingActive & !MyGameManeger.instance.Landing);
            SaveCsvScript.SetFile();
        }
    }
}
