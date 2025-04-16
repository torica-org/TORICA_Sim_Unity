using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ConnectionSettingText : MonoBehaviour
{
    private Text scoreText;
    private AerodynamicCalculator script;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreText = this.GetComponent<Text>();
        script = MyGameManeger.instance.Plane.GetComponent<AerodynamicCalculator>();

        RefreshText();
    }

    // Update is called once per frame
    void Update()
    {
        if(MyGameManeger.instance.FlightSettingActive){
            RefreshText();
            Debug.Log("AAA");
        }
    }

    void RefreshText()
    {
        scoreText.text = "";
        if(MyGameManeger.instance.FrameUseable){     
            Debug.Log("BBB");   
            scoreText.text += Math.Round(script.massRightRaw,2,  MidpointRounding.AwayFromZero)+"\n";
            scoreText.text += Math.Round(script.massLeftRaw,2,  MidpointRounding.AwayFromZero)+"\n";
            scoreText.text += Math.Round(script.massBackwardRightRaw,2,  MidpointRounding.AwayFromZero)+"\n";
            scoreText.text += Math.Round(script.massBackwardLeftRaw,2,  MidpointRounding.AwayFromZero)+"\n\n";
            scoreText.text += Math.Round(script.massRightRaw+script.massLeftRaw,2,  MidpointRounding.AwayFromZero)+Math.Round(script.massBackwardRightRaw,2,  MidpointRounding.AwayFromZero)+Math.Round(script.massBackwardLeftRaw,2,  MidpointRounding.AwayFromZero)+"\n\n";
            scoreText.text += Math.Round(script.pitchGravity,2,  MidpointRounding.AwayFromZero)+"\n";
            scoreText.text += Math.Round(script.pitchGravityPilot,2,  MidpointRounding.AwayFromZero)+"\n";
            scoreText.text += Math.Round(script.dr,2,  MidpointRounding.AwayFromZero)+"\n";
        }

    }
}
