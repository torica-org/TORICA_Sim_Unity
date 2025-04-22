using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelInstantiater : MonoBehaviour
{
    public void OnEnables()
    {
        if(MyGameManeger.instance.PlaneName == null){
            MyGameManeger.instance.PlaneName = MyGameManeger.instance.DefaultPlane;
        }

        GameObject PlaneObj = (GameObject)Resources.Load(MyGameManeger.instance.PlaneName);
        Instantiate(PlaneObj, new Vector3( -1.0f, 0.0f, 0.0f), Quaternion.identity);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
