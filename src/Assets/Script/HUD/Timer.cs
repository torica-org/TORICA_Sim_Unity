using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text timeText;//Textコンポーネントを保持するための変数
    float timeValue = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        timeText = GameObject.Find("TimeValue").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MyGameManeger.instance.FlightSettingActive)
        {
            timeValue = 0.0f;
            timeText.text = "0.00";
        }
        else
        {
            timeValue += Time.deltaTime;
            timeText.text = timeValue.ToString("0.00");
        }
    }
}
