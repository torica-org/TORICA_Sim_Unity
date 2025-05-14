using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneController : MonoBehaviour
{
    private GameObject Platform;
    private bool OnJoyStickReset;
    private AerodynamicCalculator script;//AerodynamicCalculatorスクリプトにアクセスするための変数
    float seconds;
    // Start is called before the first frame update
    void Start()
    {
        Platform = GameObject.Find("Platform");
        script = MyGameManeger.instance.Plane.GetComponent<AerodynamicCalculator>();

        if(MyGameManeger.instance.FlightMode == "TestFlight"){
            Platform.SetActive(false);  
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(2) || Input.GetButtonDown("ResetButton") || (OnJoyStickReset && 0.5f <= ((script.JoyStickNow-MyGameManeger.instance.JoyStick0)/MyGameManeger.instance.JoyStickFactor) && ((script.JoyStickNow-MyGameManeger.instance.JoyStick0)/MyGameManeger.instance.JoyStickFactor) <= 1.0f)){
            if(MyGameManeger.instance.EnterFlight){
                MyGameManeger.instance.SettingMode = 0;
            }
            Time.timeScale=1f;
            OnJoyStickReset = false;
            SceneManager.LoadScene("FlightScene");
        }

        if(MyGameManeger.instance.Landing){
            seconds += 0.1f;
            if (seconds >= 2)
            {
                seconds = 0;
                OnJoyStickReset = true;
            }
        }

        if(Input.GetKeyDown("m")){
            if(MyGameManeger.instance.FlightMode == "BirdmanRally"){
                MyGameManeger.instance.FlightMode = "TestFlight";
                Time.timeScale=1f;
                SceneManager.LoadScene("FlightScene");
            }else if(MyGameManeger.instance.FlightMode == "TestFlight"){
                MyGameManeger.instance.FlightMode = "BirdmanRally";
                Time.timeScale=1f;
                SceneManager.LoadScene("FlightScene");
            }

        }
    }
}
