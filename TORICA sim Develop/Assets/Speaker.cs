using UnityEngine;
using System;  // Needed for Math

public class Speaker : MonoBehaviour
{
	public double frequency = 0;
	public double gain = 0;
	private double increment;
	private double phase;
	private double sampling_frequency = 48000;

    private decimal interval = 1.0m;
    private decimal frameNumber;

    private bool onoff;
    private AerodynamicCalculator script;//AerodynamicCalculatorスクリプトにアクセスするための変数
    private Rigidbody PlaneRigidbody;

    void Start(){
        script = MyGameManeger.instance.Plane.GetComponent<AerodynamicCalculator>();
        PlaneRigidbody = MyGameManeger.instance.Plane.GetComponent<Rigidbody>();

        frequency = 1320;
        gain = 0;
        interval = 1m;
    }
    void Update(){
        if(MyGameManeger.instance.SettingActive){
            gain = 0;
        }
    }

    void FixedUpdate(){
        if(MyGameManeger.instance.EnterFlight){
            Debug.Log("ZZZ");
        
            if(script.Airspeed > 10){
                frequency = 440;
            }
            else if(script.Airspeed > 5){
                frequency = 880;
            }
            else{
                frequency = 1320;
            }

            if(!MyGameManeger.instance.TakeOff){
                interval = 1m;
            }
            else if(PlaneRigidbody.position.y > 2f){
                interval = 0.5m;
            }
            else if(PlaneRigidbody.position.y > 1.3f){
                interval = 0.24m;
                Debug.Log("CCC");
            }
            else{
                interval = 0.124m;
                Debug.Log("DDD");
            }

            if(frameNumber%(interval/0.02m) == 0)//interval[s]ごとにリストに追加
            {
                if(onoff){
                    Debug.Log("OFF");
                    gain = 0;
                    onoff = false;
                }
                else
                {
                    Debug.Log("ON");
                    gain = 0.1;
                    onoff = true;
                }
            }
        }
        frameNumber++;//0.02秒経過
    }

	void OnAudioFilterRead(float[] data, int channels)
	{
		increment = frequency * 2 * Math.PI / sampling_frequency;

		for (var i = 0; i < data.Length; i = i + channels)
		{
			phase = phase + increment;
			data[i] = (float)(gain*Math.Sin(phase));
			if (channels == 2) data[i + 1] = data[i];
			if (phase > 2 * Math.PI) phase = 0;
		}
	}
}