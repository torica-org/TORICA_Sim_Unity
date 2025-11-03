using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWindController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(MyGameManeger.instance.RandomWind){
            MyGameManeger.instance.GustMag = Random.Range(0,60)*0.1f;
            MyGameManeger.instance.GustDirection =Random.Range(-12,12)*15f;
            MyGameManeger.instance.SettingChanged = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("k")){
            MyGameManeger.instance.RandomWind = !MyGameManeger.instance.RandomWind;
            if(MyGameManeger.instance.RandomWind){
                MyGameManeger.instance.GustMag = Random.Range(0,60)*0.1f;
                MyGameManeger.instance.GustDirection =Random.Range(-12,12)*15f;
                MyGameManeger.instance.SettingChanged = true;
            }
        }
    }
}
