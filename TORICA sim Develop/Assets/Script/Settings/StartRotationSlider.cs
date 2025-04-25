using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StartRotationSlider : MonoBehaviour
{
    private Text scoreText;
    private AerodynamicCalculator script;
    private Slider CurrentSlider;

    // Use this for initialization
    void Start()
    {
        scoreText = GameObject.Find("StartRotation").GetComponent<Text>();
        script = MyGameManeger.instance.Plane.GetComponent<AerodynamicCalculator>();
        CurrentSlider = GetComponent<Slider>();

        if(MyGameManeger.instance.SettingChanged){
            CurrentSlider.value = MyGameManeger.instance.StartRotation;
        }else{
            MyGameManeger.instance.StartRotation = CurrentSlider.value;
        }
        
        scoreText.text = MyGameManeger.instance.StartRotation.ToString("0.000");
    }

    public void Method()
    {
        MyGameManeger.instance.StartRotation = CurrentSlider.value;
        scoreText.text = MyGameManeger.instance.StartRotation.ToString("0.000");
        MyGameManeger.instance.SettingChanged = true;
        script.transform.rotation = Quaternion.Euler(0.0f, MyGameManeger.instance.StartRotation, MyGameManeger.instance.TailRotation);
    }
}