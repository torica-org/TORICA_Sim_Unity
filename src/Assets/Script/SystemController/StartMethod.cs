using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StartMethod : MonoBehaviour
{
    [SerializeField] private ChangeAircraft changeAircraft;
    [SerializeField] private FlightSettingController flightSettingController;
    [SerializeField] private SettingController settingController;
    [SerializeField] private ModelInstantiater modelInstantiater;
    [SerializeField] private FlightModelController flightModelController;
    [SerializeField] private ChangeFlightModel changeFlightModel;
    private GameObject PlaneParent;

    void OnEnable(){
        flightSettingController.OnEnables();

        settingController.OnEnables();

        modelInstantiater.OnEnables();

        changeFlightModel.OnEnables();

        flightModelController.OnEnables();

        MyGameManeger.instance.Plane.GetComponent<AerodynamicCalculator>().OnEnables();

        changeAircraft.OnEnables();
    }
}