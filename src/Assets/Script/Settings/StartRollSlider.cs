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
        script = GameManager.instance.Plane.GetComponent<AerodynamicCalculator>();
        CurrentSlider = GetComponent<Slider>();

        if (GameManager.instance.SettingChanged) {
            CurrentSlider.value = GameManager.instance.StartRoll;
        } else  {
            GameManager.instance.StartRoll = CurrentSlider.value;
        }

        scoreText.text = GameManager.instance.StartRoll.ToString("0.000");
    }

    public void Method()
    {
        GameManager.instance.StartRoll = CurrentSlider.value;
        scoreText.text = GameManager.instance.StartRoll.ToString("0.000");
        GameManager.instance.SettingChanged = true;
        //script.transform.rotation = Quaternion.Euler(0.0f, GameManager.instance.StartRotation, GameManager.instance.TailRotation);
        script.transform.rotation = Quaternion.Euler(GameManager.instance.StartRoll, GameManager.instance.StartRotation, GameManager.instance.TailRotation);


    }
}
