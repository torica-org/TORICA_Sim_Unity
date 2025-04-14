using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMethod : MonoBehaviour
{
    public ChangeAircraft changeAircraft;
    public FlightSettingController flightSettingController;
    public SettingController settingController;
    public ModelController modelController;

    private GameObject PlaneParent;

    void OnEnable(){
        flightSettingController.OnEnables();

        settingController.OnEnables();

        PlaneParent = GameObject.Find("Plane");
        foreach(Transform item in PlaneParent.transform){
            if(item.gameObject.activeSelf){
                item.gameObject.GetComponent<AerodynamicCalculator>().OnEnables();
            }
        }

        //MyGameManeger.instance.Plane.GetComponent<AerodynamicCalculator>().OnEnables();

        changeAircraft.OnEnables();
        modelController.OnEnables();
    }
}