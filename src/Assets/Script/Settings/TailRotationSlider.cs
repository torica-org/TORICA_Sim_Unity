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
        script = GameManager.instance.Plane.GetComponent<AerodynamicCalculator>();
        CurrentSlider = GetComponent<Slider>();

        if(GameManager.instance.SettingChanged){
            CurrentSlider.value = GameManager.instance.TailRotation;
        }else{
            GameManager.instance.TailRotation = CurrentSlider.value;
        }
        
        scoreText.text = GameManager.instance.TailRotation.ToString("0.000");
    }

    public void Method()
    {
        GameManager.instance.TailRotation = CurrentSlider.value;
        scoreText.text = GameManager.instance.TailRotation.ToString("0.000");
        GameManager.instance.SettingChanged = true;
        //script.transform.rotation = Quaternion.Euler(0.0f, GameManager.instance.StartRotation, GameManager.instance.TailRotation);
        script.transform.rotation = Quaternion.Euler(GameManager.instance.StartRoll, GameManager.instance.StartRotation, GameManager.instance.TailRotation);


    }
}
