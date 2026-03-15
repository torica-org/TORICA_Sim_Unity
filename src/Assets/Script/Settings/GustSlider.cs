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
            CurrentSlider.value = GameManager.instance.GustMag*10f;
        }else{
            GameManager.instance.GustMag = CurrentSlider.value*0.1f;
        }
        
        scoreText.text = GameManager.instance.GustMag.ToString("0.000");

    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentSlider.value != GameManager.instance.GustMag*10f){
            CurrentSlider.value = GameManager.instance.GustMag*10f;
        }
    }

    public void Method()
    {
        GameManager.instance.GustMag = CurrentSlider.value*0.1f;
        scoreText.text = GameManager.instance.GustMag.ToString("0.000");
        GameManager.instance.SettingChanged = true;
    }
}
