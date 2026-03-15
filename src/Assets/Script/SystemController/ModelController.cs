/*現在は使用されていません
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    private GameObject PlaneParent;
    
    // Start is called before the first frame update
    public void OnEnables()
    {
        PlaneParent = GameObject.Find("Plane");
        foreach(Transform item in PlaneParent.transform){
            if(item.gameObject.name != GameManager.instance.PlaneName){
                item.gameObject.SetActive(false);
            }
        }
        GameManager.instance.Plane = GameObject.Find(GameManager.instance.PlaneName);
    }
}
*/
