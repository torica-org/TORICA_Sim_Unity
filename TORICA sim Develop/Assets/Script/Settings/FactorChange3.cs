using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactorChange3 : MonoBehaviour
{

    private Slider CurrentSlider;

    // Start is called before the first frame update
    void Start()
    {
        CurrentSlider = GetComponent<Slider>();
        
        if(MyGameManeger.instance.SettingChanged){
            CurrentSlider.value = MyGameManeger.instance.massBackwardLeftFactor;
        }else{
            MyGameManeger.instance.massBackwardLeftFactor = MyGameManeger.instance.DefaultFactor;
        }
    }
    void Update()
    {
        if(CurrentSlider.value != MyGameManeger.instance.massBackwardLeftFactor){
            CurrentSlider.value = MyGameManeger.instance.massBackwardLeftFactor;
        }
    }

    public void Method()
    {
        MyGameManeger.instance.massBackwardLeftFactor = CurrentSlider.value;
        MyGameManeger.instance.SettingChanged = true;
    }
}