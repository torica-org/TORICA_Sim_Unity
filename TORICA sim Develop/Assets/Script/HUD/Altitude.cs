using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Altitude : MonoBehaviour
{
    private Text scoreText;
    private Rigidbody PlaneRigidbody;
    private AerodynamicCalculator script;//AerodynamicCalculatorスクリプトにアクセスするための変数

    // Start is called before the first frame update
    void Start()
    {
        scoreText = this.GetComponent<Text>();
        PlaneRigidbody = MyGameManeger.instance.Plane.GetComponent<Rigidbody>();
        script = MyGameManeger.instance.Plane.GetComponent<AerodynamicCalculator>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = script.ALT.ToString("0.000");
    }
}
