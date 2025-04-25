using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactorChange2 : MonoBehaviour
{

    private Slider CurrentSlider;
    private float factor;
    private int LoadSell = 2;//0:Right 1:Left 2:BackwardRight 3:BackwardLeft

    // Start is called before the first frame update
    void Start()
    {
        CurrentSlider = GetComponent<Slider>();
        switch(LoadSell){
            case 0:
                CurrentSlider.value = MyGameManeger.instance.massRightFactor;
                break;
            case 1:
                CurrentSlider.value = MyGameManeger.instance.massLeftFactor;
                break;
            case 2:
                CurrentSlider.value = MyGameManeger.instance.massBackwardRightFactor;
                break;
            case 3:
                CurrentSlider.value = MyGameManeger.instance.massBackwardLeftFactor;
                break;
        }
    }

    public void Method()
    {
        MyGameManeger.instance.SettingChanged = true;

        switch(LoadSell){
            case 0:
                MyGameManeger.instance.massRightFactor = CurrentSlider.value;
                break;
            case 1:
                MyGameManeger.instance.massLeftFactor = CurrentSlider.value;
                break;
            case 2:
                MyGameManeger.instance.massBackwardRightFactor = CurrentSlider.value;
                break;
            case 3:
                MyGameManeger.instance.massBackwardLeftFactor = CurrentSlider.value;
                break;
        }
    }
}