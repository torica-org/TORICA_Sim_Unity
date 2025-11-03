using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CenterOfMassErrorController : MonoBehaviour
{
    [SerializeField] private Text CenterOfMassErrorText;
    public void PushCenterOfMassErrorButton(){
        MyGameManeger.instance.CenterOfMassError = !MyGameManeger.instance.CenterOfMassError;
        if(MyGameManeger.instance.CenterOfMassError){
            CenterOfMassErrorText.text = "有効化中";
        }else{
            CenterOfMassErrorText.text = "無効化中";
        }
    }

    void Start(){
        if(MyGameManeger.instance.CenterOfMassError){
            CenterOfMassErrorText.text = "有効化中";
        }else{
            CenterOfMassErrorText.text = "無効化中";
        }
    }
}
