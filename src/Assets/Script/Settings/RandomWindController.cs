using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWindController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.instance.RandomWind){
            GameManager.instance.GustMag = Random.Range(0,60)*0.1f;
            GameManager.instance.GustDirection =Random.Range(-12,12)*15f;
            GameManager.instance.SettingChanged = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("k")){
            GameManager.instance.RandomWind = !GameManager.instance.RandomWind;
            if(GameManager.instance.RandomWind){
                GameManager.instance.GustMag = Random.Range(0,60)*0.1f;
                GameManager.instance.GustDirection =Random.Range(-12,12)*15f;
                GameManager.instance.SettingChanged = true;
            }
        }
    }
}
