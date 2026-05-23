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
            CurrentSlider.value = Config.TakeoffRoll;
        } else  {
            Config.TakeoffRoll = CurrentSlider.value;
        }

        scoreText.text = Config.TakeoffRoll.ToString("0.000");
    }

    public void Method()
    {
        Config.TakeoffRoll = CurrentSlider.value;
        scoreText.text = Config.TakeoffRoll.ToString("0.000");
        GameManager.instance.SettingChanged = true;
        //script.transform.rotation = Quaternion.Euler(0.0f, GameManager.instance.StartRotation, GameManager.instance.TailRotation);
        script.transform.rotation = Quaternion.Euler(Config.TakeoffRoll, Config.TakeoffYaw, Config.TakeoffPitch);
    }
}
