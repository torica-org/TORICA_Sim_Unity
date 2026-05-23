using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TakeoffVelocitySlider : MonoBehaviour
{
    private Text scoreText;
    private AerodynamicCalculator script;
    private Slider CurrentSlider;

    // Use this for initialization
    void Start()
    {
        scoreText = GameObject.Find("TakeoffVelocity").GetComponent<Text>();
        script = GameManager.instance.Plane.GetComponent<AerodynamicCalculator>();
        CurrentSlider = GetComponent<Slider>();

        if(GameManager.instance.SettingChanged){
            CurrentSlider.value = Config.TakeoffSpeed*10f;
        }else{
            Config.TakeoffSpeed = CurrentSlider.value*0.1f;
        }
        
        scoreText.text = Config.TakeoffSpeed.ToString("0.000");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Method()
    {
        Config.TakeoffSpeed = CurrentSlider.value*0.1f;
        scoreText.text = Config.TakeoffSpeed.ToString("0.000");
        GameManager.instance.SettingChanged = true;
    }
}
