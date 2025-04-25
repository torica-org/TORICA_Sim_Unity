using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightModelController : MonoBehaviour
{
    public void OnEnables()
    {
        if(MyGameManeger.instance.FlightModel == null){
            MyGameManeger.instance.FlightModel = MyGameManeger.instance.DefaultFlightModel;
        }

        switch(MyGameManeger.instance.FlightModel){
            case "isoSim1":
                MyGameManeger.instance.Plane.AddComponent<isoSim1>();
                break;
            default:
                Debug.Log("error");
                break;
        }
    }
}