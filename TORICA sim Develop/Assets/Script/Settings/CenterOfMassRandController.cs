using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CenterOfMassRandController : MonoBehaviour
{
    [SerializeField] private Text CenterOfMassRandText;
    public void PushCenterOfMassRandButton(){
        MyGameManeger.instance.CenterOfMassRand = !MyGameManeger.instance.CenterOfMassRand;
        if(MyGameManeger.instance.CenterOfMassRand){
            CenterOfMassRandText.text = "有効化中";
        }else{
            CenterOfMassRandText.text = "無効化中";
        }
    }

    void Start(){
        if(MyGameManeger.instance.CenterOfMassRand){
            CenterOfMassRandText.text = "有効化中";
        }else{
            CenterOfMassRandText.text = "無効化中";
        }
    }
}
