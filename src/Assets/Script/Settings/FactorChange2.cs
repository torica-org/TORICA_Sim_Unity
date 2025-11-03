using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactorChange2 : MonoBehaviour
{

    private Slider CurrentSlider;

    // Start is called before the first frame update
    void Start()
    {
        CurrentSlider = GetComponent<Slider>();
        
        if(MyGameManeger.instance.SettingChanged){
            CurrentSlider.value = MyGameManeger.instance.massBackwardRightFactor;
        }else{
            MyGameManeger.instance.massBackwardRightFactor = MyGameManeger.instance.DefaultFactor;
        }
    }
    void Update()
    {
        if(CurrentSlider.value != MyGameManeger.instance.massBackwardRightFactor){
            CurrentSlider.value = MyGameManeger.instance.massBackwardRightFactor;
        }
    }

    public void Method()
    {
        MyGameManeger.instance.massBackwardRightFactor = CurrentSlider.value;
        MyGameManeger.instance.SettingChanged = true;
    }
}