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
            CurrentSlider.value = Config.GustMagnitude*10f;
        }else{
            Config.GustMagnitude = CurrentSlider.value*0.1f;
        }
        
        scoreText.text = Config.GustMagnitude.ToString("0.000");

    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentSlider.value != Config.GustMagnitude*10f){
            CurrentSlider.value = Config.GustMagnitude*10f;
        }
    }

    public void Method()
    {
        Config.GustMagnitude = CurrentSlider.value*0.1f;
        scoreText.text = Config.GustMagnitude.ToString("0.000");
        GameManager.instance.SettingChanged = true;
    }
}
