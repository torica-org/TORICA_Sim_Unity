using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RudderErrorController : MonoBehaviour
{
    [SerializeField] private Text RudderErrorText;
    public void PushRudderErrorButton(){
        GameManager.instance.RudderError = !GameManager.instance.RudderError;
        if(GameManager.instance.RudderError){
            RudderErrorText.text = "有効化中";
        }else{
            RudderErrorText.text = "無効化中";
        }
    }

    void Start(){
        if(GameManager.instance.RudderError){
            RudderErrorText.text = "有効化中";
        }else{
            RudderErrorText.text = "無効化中";
        }
    }
}
