using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GustDirectionSlider : MonoBehaviour
{
    private Text scoreText;
    private AerodynamicCalculator script;
    private Slider CurrentSlider;

    // Use this for initialization
    void Start()
    {
        scoreText = GameObject.Find("GustDirection").GetComponent<Text>();
        script = GameManager.instance.Plane.GetComponent<AerodynamicCalculator>();
        CurrentSlider = GetComponent<Slider>();

        if(GameManager.instance.SettingChanged){
            CurrentSlider.value = GameManager.instance.GustDirection/15f;
        }else{
            GameManager.instance.GustDirection = CurrentSlider.value*15f;
        }
        
        scoreText.text = GameManager.instance.GustDirection.ToString("0.000");
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentSlider.value != GameManager.instance.GustDirection/15f)
        {
            CurrentSlider.value = GameManager.instance.GustDirection/15f;
        }
    }

    public void Method()
    {
        GameManager.instance.GustDirection = CurrentSlider.value*15f;
        scoreText.text = GameManager.instance.GustDirection.ToString("0.000");
        GameManager.instance.SettingChanged = true;
    }
}
