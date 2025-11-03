using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StartRollSlider : MonoBehaviour
{
    private Text scoreText;
    private AerodynamicCalculator script;
    private Slider CurrentSlider;

    // Use this for initialization
    void Start()
    {
        scoreText = GameObject.Find("StartRoll").GetComponent<Text>();
        script = MyGameManeger.instance.Plane.GetComponent<AerodynamicCalculator>();
        CurrentSlider = GetComponent<Slider>();

        if (MyGameManeger.instance.SettingChanged) {
            CurrentSlider.value = MyGameManeger.instance.StartRoll;
        } else  {
            MyGameManeger.instance.StartRoll = CurrentSlider.value;
        }

        scoreText.text = MyGameManeger.instance.StartRoll.ToString("0.000");
    }

    public void Method()
    {
        MyGameManeger.instance.StartRoll = CurrentSlider.value;
        scoreText.text = MyGameManeger.instance.StartRoll.ToString("0.000");
        MyGameManeger.instance.SettingChanged = true;
        //script.transform.rotation = Quaternion.Euler(0.0f, MyGameManeger.instance.StartRotation, MyGameManeger.instance.TailRotation);
        script.transform.rotation = Quaternion.Euler(MyGameManeger.instance.StartRoll, MyGameManeger.instance.StartRotation, MyGameManeger.instance.TailRotation);


    }
}
