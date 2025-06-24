using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorRandController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(MyGameManeger.instance.RudderError){
            if(Random.Range(0.0f,1.0f) < 0.5f){
                Debug.Log("AAA");
                MyGameManeger.instance.RudderErrorValue = Random.Range(-1.0f, 1.0f);//ラダー異常時にある値でラダーが固定される。
            }else{
                MyGameManeger.instance.RudderErrorValue = 999f;
            }   
        }
        if(MyGameManeger.instance.CenterOfMassError){
            if(Random.Range(0.0f,1.0f) < 0.5f){
                MyGameManeger.instance.CenterOfMassErrorValue = RandomUtils.RangeByCentralLimit(-0.1f, 0.1f);//重心異常時に足して定常重心がずれる。
            }else{
                MyGameManeger.instance.CenterOfMassErrorValue = 999f;
            }
        }
        if(MyGameManeger.instance.CenterOfMassRand){
            MyGameManeger.instance.CenterOfMassRandValue = RandomUtils.RangeByCentralLimit(0.7f, 1.5f);//重心移動値にかけてゆらぎをもたらす
        }
        if(MyGameManeger.instance.GustRand){
            MyGameManeger.instance.GustRandValue = RandomUtils.RangeByCentralLimit(-1.0f, 1.0f);//風速に足してゆらぎをもたらす
        }
        if(MyGameManeger.instance.RudderRand){
            MyGameManeger.instance.RudderRandValue = RandomUtils.RangeByCentralLimit(0.7f, 1.5f);//ラダー移動値にかけてゆらぎをもたらす
        }
    }
}
