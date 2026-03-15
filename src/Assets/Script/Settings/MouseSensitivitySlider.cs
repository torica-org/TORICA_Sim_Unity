using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MouseSensitivitySlider : MonoBehaviour
{
    private Slider CurrentSlider;

    // Use this for initialization
    void Start()
    {
        CurrentSlider = GetComponent<Slider>();

        if(GameManager.instance.SettingChanged){
            CurrentSlider.value = GameManager.instance.MouseSensitivity*10f;
        }else{
            GameManager.instance.MouseSensitivity = CurrentSlider.value/10f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Method()
    {
        GameManager.instance.MouseSensitivity = CurrentSlider.value/10f;
        GameManager.instance.SettingChanged = true;
    }
}
