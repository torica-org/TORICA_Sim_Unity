using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RudderErrorController : MonoBehaviour
{
    [SerializeField] private Text RudderErrorText;
    public void PushRudderErrorButton(){
        MyGameManeger.instance.RudderError = !MyGameManeger.instance.RudderError;
        if(MyGameManeger.instance.RudderError){
            RudderErrorText.text = "有効化中";
        }else{
            RudderErrorText.text = "無効化中";
        }
    }

    void Start(){
        if(MyGameManeger.instance.RudderError){
            RudderErrorText.text = "有効化中";
        }else{
            RudderErrorText.text = "無効化中";
        }
    }
}
