using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TailSetDegSlider : MonoBehaviour
{
    private Text scoreText;
    private AerodynamicCalculator script;
    private Slider CurrentSlider;

    // Use this for initialization
    void Start()
    {
        scoreText = GameObject.Find("TailSetDeg").GetComponent<Text>();
        script = GameManager.instance.Plane.GetComponent<AerodynamicCalculator>();
        CurrentSlider = GetComponent<Slider>();

        if(GameManager.instance.SettingChanged){
            CurrentSlider.value = GameManager.instance.TailSetDeg;
        }else{
            GameManager.instance.TailSetDeg = CurrentSlider.value;
        }
        
        scoreText.text = GameManager.instance.TailSetDeg.ToString("0.000");
    }

    public void Method()
    {
        CurrentSlider.value = Mathf.Round(CurrentSlider.value / 0.5f) * 0.5f;

        GameManager.instance.TailSetDeg = CurrentSlider.value;
        scoreText.text = GameManager.instance.TailSetDeg.ToString("0.000");
        GameManager.instance.SettingChanged = true;
    }
}
