using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactorChange : MonoBehaviour
{

    private Slider CurrentSlider;
    private float factor;
    [System.NonSerialized] private int LoadSell;//0:Right 1:Left 2:BackwardRight 3:BackwardLeft

    // Start is called before the first frame update
    void Start()
    {
        switch(factor){
            case 0:
                factor = MyGameManeger.instance.massRightFactor;
                break;
            case 1:
                factor = MyGameManeger.instance.massLeftFactor;
                break;
            case 2:
                factor = MyGameManeger.instance.massBackwardRightFactor;
                break;
            case 3:
                factor = MyGameManeger.instance.massBackwardLeftFactor;
                break;
        }

        CurrentSlider = GetComponent<Slider>();

        if(MyGameManeger.instance.SettingChanged){
            CurrentSlider.value = factor;
        }else{
            factor = CurrentSlider.value*0.1f;
        }

        switch(factor){
            case 0:
                MyGameManeger.instance.massRightFactor = factor;
                break;
            case 1:
                MyGameManeger.instance.massLeftFactor = factor;
                break;
            case 2:
                MyGameManeger.instance.massBackwardRightFactor = factor;
                break;
            case 3:
                MyGameManeger.instance.massBackwardLeftFactor = factor;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(factor){
            case 0:
                factor = MyGameManeger.instance.massRightFactor;
                break;
            case 1:
                factor = MyGameManeger.instance.massLeftFactor;
                break;
            case 2:
                factor = MyGameManeger.instance.massBackwardRightFactor;
                break;
            case 3:
                factor = MyGameManeger.instance.massBackwardLeftFactor;
                break;
        }

        if(CurrentSlider.value != factor){
            CurrentSlider.value = factor;
            MyGameManeger.instance.SettingChanged = true;
        }

        switch(factor){
            case 0:
                MyGameManeger.instance.massRightFactor = factor;
                break;
            case 1:
                MyGameManeger.instance.massLeftFactor = factor;
                break;
            case 2:
                MyGameManeger.instance.massBackwardRightFactor = factor;
                break;
            case 3:
                MyGameManeger.instance.massBackwardLeftFactor = factor;
                break;
        }
    }

    public void Method()
    {
        switch(factor){
            case 0:
                factor = MyGameManeger.instance.massRightFactor;
                break;
            case 1:
                factor = MyGameManeger.instance.massLeftFactor;
                break;
            case 2:
                factor = MyGameManeger.instance.massBackwardRightFactor;
                break;
            case 3:
                factor = MyGameManeger.instance.massBackwardLeftFactor;
                break;
        }

        factor = CurrentSlider.value;
        MyGameManeger.instance.SettingChanged = true;

        switch(factor){
            case 0:
                MyGameManeger.instance.massRightFactor = factor;
                break;
            case 1:
                MyGameManeger.instance.massLeftFactor = factor;
                break;
            case 2:
                MyGameManeger.instance.massBackwardRightFactor = factor;
                break;
            case 3:
                MyGameManeger.instance.massBackwardLeftFactor = factor;
                break;
        }
    }
}