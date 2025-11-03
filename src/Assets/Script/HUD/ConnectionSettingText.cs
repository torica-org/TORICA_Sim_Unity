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
        }
    }

    void RefreshText()
    {
        scoreText.text = "";
        //if(MyGameManeger.instance.FrameUseable){
            scoreText.text += Math.Round(script.massRight,3,  MidpointRounding.AwayFromZero)+"\n";
            scoreText.text += Math.Round(script.massLeft,3,  MidpointRounding.AwayFromZero)+"\n";
            scoreText.text += Math.Round(script.massBackwardRight,3,  MidpointRounding.AwayFromZero)+"\n";
            scoreText.text += Math.Round(script.massBackwardLeft,3,  MidpointRounding.AwayFromZero)+"\n\n";
            scoreText.text += Math.Round(script.massRight+script.massLeft,3,  MidpointRounding.AwayFromZero)+Math.Round(script.massBackwardRight,2,  MidpointRounding.AwayFromZero)+Math.Round(script.massBackwardLeft,2,  MidpointRounding.AwayFromZero)+"\n\n";
            scoreText.text += Math.Round(script.pitchGravity,3,  MidpointRounding.AwayFromZero)+"\n";
            scoreText.text += Math.Round(script.pitchGravityPilot,3,  MidpointRounding.AwayFromZero)+"\n";
            scoreText.text += Math.Round(script.dr,3,  MidpointRounding.AwayFromZero)+"\n\n";
        //}
        scoreText.text += MyGameManeger.instance.VRMode ? "VRモード" : "非VRモード";
    }
}
