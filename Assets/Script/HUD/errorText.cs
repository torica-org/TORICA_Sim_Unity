using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class errorText : MonoBehaviour
{
    public Text text;
    // Update is called once per frame
    void Update()
    {
        if(MyGameManeger.instance.error){
            text.text = MyGameManeger.instance.errorText;
            MyGameManeger.instance.error = false;
        }
    }
}
