using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Airspeed : MonoBehaviour
{
    private Text scoreText;//Textコンポーネントにアクセスするための変数
    private AerodynamicCalculator script;//AerodynamicCalculatorスクリプトにアクセスするための変数

    // Start is called before the first frame update
    void Start()
    {
        scoreText = this.GetComponent<Text>();
        script = MyGameManeger.instance.Plane.GetComponent<AerodynamicCalculator>();
    }

    // Update is called once per frame
    void FixedUpdate()//0.02秒ごとに実行
    {
        scoreText.text = script.Airspeed.ToString("0.000");
    }
}
