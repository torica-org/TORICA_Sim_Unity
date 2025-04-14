using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Angle : MonoBehaviour
{
    private Text scoreText;
    private AerodynamicCalculator script;
    private float nz_max;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = this.GetComponent<Text>();
        script = MyGameManeger.instance.Plane.GetComponent<AerodynamicCalculator>();

        nz_max=0.000f;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = 
            "\n\n" + script.alpha.ToString("0.000") 
            + "\n" + script.beta.ToString("0.000");
            //+ "\n\n" + script.nz.ToString("0.000")
            //+ "\n" + nz_max.ToString("0.000");
        
        if(script.nz>nz_max){
            nz_max=script.nz;
        }
    }
}
