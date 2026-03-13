using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CgeRandCountroller : MonoBehaviour
{
    [SerializeField] private Text CgeRandText;
    public void PushCgeRandButton(){
        GameManager.instance.CgeRand = !GameManager.instance.CgeRand;
        if(GameManager.instance.CgeRand){
            CgeRandText.text = "有効化中";
        }else{
            CgeRandText.text = "無効化中";
        }
    }

    void Start(){
        if(GameManager.instance.CgeRand){
            CgeRandText.text = "有効化中";
        }else{
            CgeRandText.text = "無効化中";
        }
    }
}
