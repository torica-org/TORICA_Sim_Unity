using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GustRandController : MonoBehaviour
{
    [SerializeField] private Text GustRandText;
    public void PushGustRandButton(){
        MyGameManeger.instance.GustRand = !MyGameManeger.instance.GustRand;
        if(MyGameManeger.instance.GustRand){
            GustRandText.text = "有効化中";
        }else{
            GustRandText.text = "無効化中";
        }
    }

    void Start(){
        if(MyGameManeger.instance.GustRand){
            GustRandText.text = "有効化中";
        }else{
            GustRandText.text = "無効化中";
        }
    }
}
