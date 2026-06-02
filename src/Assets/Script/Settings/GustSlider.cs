using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GustSlider : MonoBehaviour
{
    private Text scoreText;
    private AerodynamicCalculator script;
    private Slider CurrentSlider;

    // Use this for initialization
    void Start()
    {
        scoreText = GameObject.Find("Gust").GetComponent<Text>();
        script = GameManager.instance.Plane.GetComponent<AerodynamicCalculator>();
        CurrentSlider = GetComponent<Slider>();

        if(GameManager.instance.SettingChanged){
            CurrentSlider.value = Config.WindMagnitude*10f;
        }else{
            Config.WindMagnitude = CurrentSlider.value*0.1f;
        }
        
        scoreText.text = Config.WindMagnitude.ToString("0.000");

    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentSlider.value != Config.WindMagnitude*10f){
            CurrentSlider.value = Config.WindMagnitude*10f;
        }
    }

    public void Method()
    {
        Config.WindMagnitude = CurrentSlider.value*0.1f;
        scoreText.text = Config.WindMagnitude.ToString("0.000");
        GameManager.instance.SettingChanged = true;
    }
}
