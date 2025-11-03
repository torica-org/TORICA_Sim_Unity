using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Header : MonoBehaviour
{
    [SerializeField] private GameObject SettingMode0;
    [SerializeField] private GameObject SettingMode1;
    [SerializeField] private GameObject SettingMode2;
    [SerializeField] private GameObject SettingMode3;

    [SerializeField] private Button SettingMode0Button;
    [SerializeField] private Button SettingMode1Button;
    [SerializeField] private Button SettingMode2Button;
    [SerializeField] private Button SettingMode3Button;
    
    ColorBlock cb;
    
    void Start(){
        switch(MyGameManeger.instance.SettingMode){
            case 0:
                cb = SettingMode0Button.colors;
                cb.normalColor = new Color(0.7843137f,0.7843137f,0.7843137f,1f);
                SettingMode0Button.colors = cb;
                break;
            case 1:
                cb = SettingMode1Button.colors;
                cb.normalColor = new Color(0.7843137f,0.7843137f,0.7843137f,1f);
                SettingMode1Button.colors = cb;
                break;
            case 2:
                cb = SettingMode2Button.colors;
                cb.normalColor = new Color(0.7843137f,0.7843137f,0.7843137f,1f);
                SettingMode2Button.colors = cb;
                break;
            case 3:
                cb = SettingMode3Button.colors;
                cb.normalColor = new Color(0.7843137f,0.7843137f,0.7843137f,1f);
                SettingMode3Button.colors = cb;
                break;
        }
        ChangeSettingMode(MyGameManeger.instance.SettingMode);
    }

    public void ChangeSettingMode(int ChangeMode){

        switch(ChangeMode){
            case 0:
                SettingMode0.SetActive(true);
                SettingMode1.SetActive(false);
                SettingMode2.SetActive(false);
                SettingMode3.SetActive(false);
                MyGameManeger.instance.SettingMode = 0;

                cb = SettingMode1Button.colors;
                cb.normalColor = new Color(1f,1f,1f,1f);
                SettingMode1Button.colors = cb;
                
                cb = SettingMode1Button.colors;
                cb.normalColor = new Color(1f,1f,1f,1f);
                SettingMode2Button.colors = cb;

                cb = SettingMode1Button.colors;
                cb.normalColor = new Color(1f,1f,1f,1f);
                SettingMode3Button.colors = cb;
                break;
            case 1:
                SettingMode1.SetActive(true);
                SettingMode0.SetActive(false);
                SettingMode2.SetActive(false);
                SettingMode3.SetActive(false);
                MyGameManeger.instance.SettingMode = 1;

                cb = SettingMode1Button.colors;
                cb.normalColor = new Color(1f,1f,1f,1f);
                SettingMode0Button.colors = cb;
                
                cb = SettingMode1Button.colors;
                cb.normalColor = new Color(1f,1f,1f,1f);
                SettingMode2Button.colors = cb;

                cb = SettingMode1Button.colors;
                cb.normalColor = new Color(1f,1f,1f,1f);
                SettingMode3Button.colors = cb;
                break;
            case 2:
                SettingMode2.SetActive(true);
                SettingMode1.SetActive(false);
                SettingMode0.SetActive(false);
                SettingMode3.SetActive(false);
                MyGameManeger.instance.SettingMode = 2;

                cb = SettingMode1Button.colors;
                cb.normalColor = new Color(1f,1f,1f,1f);
                SettingMode1Button.colors = cb;
                
                cb = SettingMode1Button.colors;
                cb.normalColor = new Color(1f,1f,1f,1f);
                SettingMode0Button.colors = cb;

                cb = SettingMode1Button.colors;
                cb.normalColor = new Color(1f,1f,1f,1f);
                SettingMode3Button.colors = cb;
                break;
            case 3:
                SettingMode3.SetActive(true);
                SettingMode1.SetActive(false);
                SettingMode2.SetActive(false);
                SettingMode0.SetActive(false);
                MyGameManeger.instance.SettingMode = 3;

                cb = SettingMode1Button.colors;
                cb.normalColor = new Color(1f,1f,1f,1f);
                SettingMode1Button.colors = cb;
                
                cb = SettingMode1Button.colors;
                cb.normalColor = new Color(1f,1f,1f,1f);
                SettingMode2Button.colors = cb;

                cb = SettingMode1Button.colors;
                cb.normalColor = new Color(1f,1f,1f,1f);
                SettingMode0Button.colors = cb;
                break;
        }
    }
}
