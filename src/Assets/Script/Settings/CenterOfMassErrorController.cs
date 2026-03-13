using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CenterOfMassErrorController : MonoBehaviour
{
    [SerializeField] private Text CenterOfMassErrorText;
    public void PushCenterOfMassErrorButton(){
        GameManager.instance.CenterOfMassError = !GameManager.instance.CenterOfMassError;
        if(GameManager.instance.CenterOfMassError){
            CenterOfMassErrorText.text = "有効化中";
        }else{
            CenterOfMassErrorText.text = "無効化中";
        }
    }

    void Start(){
        if(GameManager.instance.CenterOfMassError){
            CenterOfMassErrorText.text = "有効化中";
        }else{
            CenterOfMassErrorText.text = "無効化中";
        }
    }
}
