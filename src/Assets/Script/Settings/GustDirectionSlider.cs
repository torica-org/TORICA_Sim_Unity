using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GustDirectionSlider : MonoBehaviour
{
    private Text scoreText;
    private AerodynamicCalculator script;
    private Slider CurrentSlider;
    private GameManager gm = GameManager.instance;

    // Use this for initialization
    void Start()
    {
        scoreText = GameObject.Find("GustDirection").GetComponent<Text>();
        script = gm.Plane.GetComponent<AerodynamicCalculator>();
        CurrentSlider = GetComponent<Slider>();

        if(gm.SettingChanged){
            CurrentSlider.value = Config.GustDirection/15f;
        }else{
            Config.GustDirection = CurrentSlider.value*15f;
        }

        Method();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentSlider.value != Config.GustDirection/15f)
        {
            CurrentSlider.value = Config.GustDirection/15f;
        }
        //Debug.Log(Config.GustDirection);
    }

    public void Method()
    {
        Config.GustDirection = CurrentSlider.value*15f;

        string DirectionText = "";
        if (Config.GustDirection > 0)
        {
          DirectionText = "R ";
        }
        else if (Config.GustDirection < 0)
        {
          DirectionText = "L ";
        }
        scoreText.text = DirectionText + Mathf.Abs(Config.GustDirection).ToString("0");

        gm.SettingChanged = true;
    }
}
