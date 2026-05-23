using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWindController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(Config.GustRandom){
            Config.GustMagnitude = Random.Range(0,60)*0.1f;
            Config.GustDirection =Random.Range(-12,12)*15f;
            GameManager.instance.SettingChanged = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("k")){
            Config.GustRandom = !Config.GustRandom;
            if(Config.GustRandom){
                Config.GustMagnitude = Random.Range(0,60)*0.1f;
                Config.GustDirection =Random.Range(-12,12)*15f;
                GameManager.instance.SettingChanged = true;
            }
        }
    }
}
