using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TailRotationSlider : MonoBehaviour
{
    private Text scoreText;
    private AerodynamicCalculator script;
    private Slider CurrentSlider;

    // Use this for initialization
    void Start()
    {
        scoreText = GameObject.Find("TailRotation").GetComponent<Text>();
        script = MyGameManeger.instance.Plane.GetComponent<AerodynamicCalculator>();
        CurrentSlider = GetComponent<Slider>();

        if(MyGameManeger.instance.SettingChanged){
            CurrentSlider.value = MyGameManeger.instance.TailRotation;
        }else{
            MyGameManeger.instance.TailRotation = CurrentSlider.value;
        }
        
        scoreText.text = MyGameManeger.instance.TailRotation.ToString("0.000");
    }

    public void Method()
    {
        Debug.Log("ROTATION");
        MyGameManeger.instance.TailRotation = CurrentSlider.value;
        scoreText.text = MyGameManeger.instance.TailRotation.ToString("0.000");
        MyGameManeger.instance.SettingChanged = true;
        script.transform.rotation = Quaternion.Euler(0.0f, MyGameManeger.instance.StartRotation, MyGameManeger.instance.TailRotation);


    }
}