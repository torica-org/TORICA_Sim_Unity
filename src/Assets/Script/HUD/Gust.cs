using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gust : MonoBehaviour
{
    private Text scoreText;
    private AerodynamicCalculator script;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = this.GetComponent<Text>();
        script = GameManager.instance.Plane.GetComponent<AerodynamicCalculator>();
    }

    // Update is called once per frame
    void Update()
    {
        string DirectionText;
        if (script.LocalGustDirection >= 0)
        {
          DirectionText = "R ";
        }
        else
        {
          DirectionText = "L ";
        }
        DirectionText += Mathf.Abs(script.LocalGustDirection).ToString("0");

        scoreText.text = 
            "\n" + script.LocalGustMag.ToString("0.000") + " m/s"
            + "\n" + DirectionText + " deg";
    }
}
