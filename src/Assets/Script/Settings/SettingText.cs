using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingText : MonoBehaviour
{
    private Text scoreText;
    private AerodynamicCalculator script;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreText = this.GetComponent<Text>();
        script = GameManager.instance.Plane.GetComponent<AerodynamicCalculator>();

        RefreshText();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.SettingMode == 1){
            RefreshText();
        }
    }

    void RefreshText()
    {
        scoreText.text = "\n";
        scoreText.text += GameManager.instance.FlightMode+"\n\n";

        if(GameManager.instance.isMainDisplayTPS){
            scoreText.text += "FPS"+"\n\n";
        }else{
            scoreText.text += "TPS"+"\n\n";
        }
        if(GameManager.instance.HUDActive){
            scoreText.text += "ON"+"\n\n";
        }else{
            scoreText.text += "OFF"+"\n\n";
        }
        if(GameManager.instance.MousePitchControl){
            scoreText.text += "Mouse"+"\n\n";
        }else{
            scoreText.text += "Keyboard"+"\n\n";
        }
    }
}
