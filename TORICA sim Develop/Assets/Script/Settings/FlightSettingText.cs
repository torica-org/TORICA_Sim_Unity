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
        
        if(MyGameManeger.instance.RandomWind){
            scoreText.text += "ON"+"\n\n\n\n\n\n\n";
        }else{
            scoreText.text += "OFF"+"\n\n\n\n\n\n\n";
        }
        if(MyGameManeger.instance.SaveCsv){
            scoreText.text += "ON"+"\n";   
        }else{
            scoreText.text += "OFF"+"\n";
        }
    }
}
