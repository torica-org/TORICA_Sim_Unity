using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorRandController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(MyGameManeger.instance.RudderError){
            if(Random.Range(0.0f,1.0f) < 0.1f){
                Debug.Log("MODE1");
                MyGameManeger.instance.RudderErrorMode = 1;
                MyGameManeger.instance.RudderErrorValue = Random.Range(-1.0f, 1.0f);//ラダー異常時にある値でラダーが固定される。
            }else if(Random.Range(0.0f,1.0f) < 0.5f){
                Debug.Log("MODE2");
                MyGameManeger.instance.RudderErrorMode = 2;
                MyGameManeger.instance.RudderErrorValue = Random.Range(-1.0f, 1.0f);//ラダー異常時にある値によくぶれるようになる
            }else if(Random.Range(0.0f,1.0f) < 0.6f){
                Debug.Log("MODE3");
                MyGameManeger.instance.RudderErrorMode = 3;
                MyGameManeger.instance.RudderErrorValue = RandomUtils.RangeByCentralLimit(-1.0f, 1.0f);//ラダー異常時にある値によくぶれるようになる
            }else{
                MyGameManeger.instance.RudderErrorMode = 0;
            }   
        }
        if(MyGameManeger.instance.CenterOfMassError){
            if(Random.Range(0.0f,1.0f) < 0.5f){
                MyGameManeger.instance.CenterOfMassErrorValue = Random.Range(-0.1f, 0.1f);//重心異常時に足して定常重心がずれる。
            }else{
                MyGameManeger.instance.CenterOfMassErrorValue = 0f;
            }
        }
        if(MyGameManeger.instance.CenterOfMassRand){
            MyGameManeger.instance.CenterOfMassRandValue = RandomUtils.RangeByCentralLimit(0.7f, 1.5f);//重心移動値にかけてゆらぎをもたらす
        }
        else{
            MyGameManeger.instance.CenterOfMassRandValue = 1f;
        }
        if(MyGameManeger.instance.GustRand){
            MyGameManeger.instance.GustRandValue = RandomUtils.RangeByCentralLimit(-1.0f, 1.0f);//風速に足してゆらぎをもたらす
        }
        else{
            MyGameManeger.instance.GustRandValue = 0f;
        }
        if(MyGameManeger.instance.RudderRand){
            MyGameManeger.instance.RudderRandValue = RandomUtils.RangeByCentralLimit(0.7f, 1.5f);//ラダー移動値にかけてゆらぎをもたらす
        }
        else{
            MyGameManeger.instance.RudderRandValue = 1f;
        }
    }
}
