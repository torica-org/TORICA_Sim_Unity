using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactorChange1 : MonoBehaviour
{

    private Slider CurrentSlider;

    // Start is called before the first frame update
    void Start()
    {
        CurrentSlider = GetComponent<Slider>();
        
        if(MyGameManeger.instance.SettingChanged){
            CurrentSlider.value = MyGameManeger.instance.massLeftFactor;
        }else{
            MyGameManeger.instance.massLeftFactor = MyGameManeger.instance.DefaultFactor;
        }
    }
    void Update()
    {
        if(CurrentSlider.value != MyGameManeger.instance.massLeftFactor){
            CurrentSlider.value = MyGameManeger.instance.massLeftFactor;
        }
    }

    public void Method()
    {
        MyGameManeger.instance.massLeftFactor = CurrentSlider.value;
        MyGameManeger.instance.SettingChanged = true;
    }
}