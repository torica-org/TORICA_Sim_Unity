using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelInstantiater : MonoBehaviour
{
    public void OnEnables()
    {
        if(GameManager.instance.PlaneName == null){
            GameManager.instance.PlaneName = GameManager.instance.DefaultPlane;
        }

        GameObject PlaneObj = (GameObject)Resources.Load(GameManager.instance.PlaneName);
        var obj = Instantiate(PlaneObj, new Vector3(PlaneObj.transform.position.x,PlaneObj.transform.position.y,PlaneObj.transform.position.z), Quaternion.identity);
        obj.name = PlaneObj.name;
        
        GameManager.instance.Plane = GameObject.Find(GameManager.instance.PlaneName);
    }
}
