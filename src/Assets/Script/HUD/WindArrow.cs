using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        Transform myTransform = this.transform;

        // ローカル座標を基準に、回転を取得
        Vector3 localAngle = myTransform.localEulerAngles;
        localAngle.x = 0; // ローカル座標を基準に、x軸を軸にした回転を10度に変更
        localAngle.y = 0; // ローカル座標を基準に、y軸を軸にした回転を10度に変更
        localAngle.z = 180-MyGameManeger.instance.GustDirection; // ローカル座標を基準に、z軸を軸にした回転を10度に変更
        myTransform.localEulerAngles = localAngle; // 回転角度を設定
    }
}
