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
        var obj = Instantiate(PlaneObj, new Vector3(PlaneObj.transform.position.x,PlaneObj.transform.position.y,PlaneObj.transform.position.z), Quaternion.identity);
        obj.name = PlaneObj.name;
        
        MyGameManeger.instance.Plane = GameObject.Find(MyGameManeger.instance.PlaneName);
    }
}
