using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FlightSettingText : MonoBehaviour
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
        if(GameManager.instance.FlightSettingActive){
            RefreshText();
        }
    }

    void RefreshText()
    {
        scoreText.text = "";
        
        if(Config.RandomizeWind){
            scoreText.text += "ON"+"\n\n\n\n\n\n\n\n";
        }else{
            scoreText.text += "OFF"+"\n\n\n\n\n\n\n\n";
        }
        if(Config.ExportLog){
            scoreText.text += "ON"+"\n";   
        }else{
            scoreText.text += "OFF"+"\n";
        }
    }
}
