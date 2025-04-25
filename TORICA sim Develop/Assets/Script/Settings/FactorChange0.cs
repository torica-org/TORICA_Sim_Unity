using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactorChange0 : MonoBehaviour
{

    private Slider CurrentSlider;

    // Start is called before the first frame update
    void Start()
    {
        CurrentSlider = GetComponent<Slider>();
        
        if(MyGameManeger.instance.SettingChanged){
            CurrentSlider.value = MyGameManeger.instance.massRightFactor;
        }else{
            MyGameManeger.instance.massRightFactor = MyGameManeger.instance.DefaultFactor;
        }
    }
    void Update()
    {
        if(CurrentSlider.value != MyGameManeger.instance.massRightFactor){
            CurrentSlider.value = MyGameManeger.instance.massRightFactor;
        }
    }

    public void Method()
    {
        MyGameManeger.instance.massRightFactor = CurrentSlider.value;
        MyGameManeger.instance.SettingChanged = true;
    }
}