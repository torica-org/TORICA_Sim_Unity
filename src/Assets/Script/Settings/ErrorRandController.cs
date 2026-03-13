using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorRandController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.instance.RudderError){
            if(Random.Range(0.0f,1.0f) < 0.1f){
                Debug.Log("MODE1");
                GameManager.instance.RudderErrorMode = 1;
                GameManager.instance.RudderErrorValue = Random.Range(-1.0f, 1.0f);//ラダー異常時にある値でラダーが固定される。
            }else if(Random.Range(0.0f,1.0f) < 0.5f){
                Debug.Log("MODE2");
                GameManager.instance.RudderErrorMode = 2;
                GameManager.instance.RudderErrorValue = Random.Range(-1.0f, 1.0f);//ラダー異常時にある値によくぶれるようになる
            }else if(Random.Range(0.0f,1.0f) < 0.6f){
                Debug.Log("MODE3");
                GameManager.instance.RudderErrorMode = 3;
                GameManager.instance.RudderErrorValue = RandomUtils.RangeByCentralLimit(-1.0f, 1.0f);//ラダー異常時にある値によくぶれるようになる
            }else{
                GameManager.instance.RudderErrorMode = 0;
            }   
        }
        if(GameManager.instance.CenterOfMassError){
            if(Random.Range(0.0f,1.0f) < 0.5f){
                GameManager.instance.CenterOfMassErrorValue = Random.Range(-0.1f, 0.1f);//重心異常時に足して定常重心がずれる。
            }else{
                GameManager.instance.CenterOfMassErrorValue = 0f;
            }
        }
        if(GameManager.instance.CenterOfMassRand){
            GameManager.instance.CenterOfMassRandValue = RandomUtils.RangeByCentralLimit(0.7f, 1.5f);//重心移動値にかけてゆらぎをもたらす
        }
        else{
            GameManager.instance.CenterOfMassRandValue = 1f;
        }
        if(GameManager.instance.GustRand){
            GameManager.instance.GustRandValue = RandomUtils.RangeByCentralLimit(-1.0f, 1.0f);//風速に足してゆらぎをもたらす
        }
        else{
            GameManager.instance.GustRandValue = 0f;
        }
        if(GameManager.instance.RudderRand){
            GameManager.instance.RudderRandValue = RandomUtils.RangeByCentralLimit(0.7f, 1.5f);//ラダー移動値にかけてゆらぎをもたらす
        }
        else{
            GameManager.instance.RudderRandValue = 1f;
        }
        if(GameManager.instance.CgeRand){
            GameManager.instance.CgeRandValue = RandomUtils.RangeByCentralLimit(0.7f, 1.5f);//地面効果係数にかけてゆらぎをもたらす
        }
        else{
            GameManager.instance.CgeRandValue = 1f;
        }

    }
}
