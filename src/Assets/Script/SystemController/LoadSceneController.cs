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
        script = GameManager.instance.Plane.GetComponent<AerodynamicCalculator>();

        if(GameManager.instance.FlightMode == "TestFlight"){
            Platform.SetActive(false);  
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(2) || Input.GetButtonDown("ResetButton") || (OnJoyStickReset && 0.5f <= ((script.JoyStickNow-GameManager.instance.JoyStick0)/GameManager.instance.JoyStickFactor) && ((script.JoyStickNow-GameManager.instance.JoyStick0)/GameManager.instance.JoyStickFactor) <= 1.0f)){
            if(GameManager.instance.EnterFlight){
                GameManager.instance.SettingMode = 0;
            }
            Time.timeScale=1f;
            OnJoyStickReset = false;
            SceneManager.LoadScene("FlightScene");
        }

        if(GameManager.instance.Landing){
            seconds += 0.1f;
            if (seconds >= 2)
            {
                seconds = 0;
                OnJoyStickReset = true;
            }
        }

        if(Input.GetKeyDown("m")){
            if(GameManager.instance.FlightMode == "BirdmanRally"){
                GameManager.instance.FlightMode = "TestFlight";
                Time.timeScale=1f;
                SceneManager.LoadScene("FlightScene");
            }else if(GameManager.instance.FlightMode == "TestFlight"){
                GameManager.instance.FlightMode = "BirdmanRally";
                Time.timeScale=1f;
                SceneManager.LoadScene("FlightScene");
            }

        }
    }
}
