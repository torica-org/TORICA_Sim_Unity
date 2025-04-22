using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMethod : MonoBehaviour
{
    [SerializeField] private ChangeAircraft changeAircraft;
    [SerializeField] private FlightSettingController flightSettingController;
    [SerializeField] private SettingController settingController;
    [SerializeField] private ModelController modelController;
    

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
        changeAircraft.OnEnables();
        modelController.OnEnables();
    }
}