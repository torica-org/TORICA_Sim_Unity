using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundBolumeController : MonoBehaviour
{
    [SerializeField]private Text scoreText;
    private Slider CurrentSlider;
    // Start is called before the first frame update
    void Start()
    {
        CurrentSlider = GetComponent<Slider>();

        CurrentSlider.value = MyGameManeger.instance.SoundBolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Method()
    {
        scoreText.text = CurrentSlider.value.ToString();
        MyGameManeger.instance.SettingChanged = true;
        MyGameManeger.instance.SoundBolume = CurrentSlider.value;
    }
}
