using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightModelController : MonoBehaviour
{
    public void OnEnables()
    {
        if(GameManager.instance.FlightModel == null){
            Debug.Log("FlightModel is null");
            GameManager.instance.FlightModel = GameManager.instance.DefaultFlightModel;
        }

        switch(GameManager.instance.FlightModel){
            case "isoSim1":
                GameManager.instance.Plane.AddComponent<isoSim1>();
                break;
            //case "isoSim2":
                //GameManager.instance.Plane.AddComponent<isoSim2>();
                //break;
            default:
                Debug.Log("error");
                break;
        }
    }
}
