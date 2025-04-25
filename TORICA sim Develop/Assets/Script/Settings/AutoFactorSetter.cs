using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFactorSetter : MonoBehaviour
{
    private float DefaultPitchGravity;
    private AerodynamicCalculator script;
    // Start is called before the first frame update
    void Start()
    {
        script = MyGameManeger.instance.Plane.GetComponent<AerodynamicCalculator>();
    }

    public void OnPush(){
        MyGameManeger.instance.massRightFactor = script.massLeftRightS/(script.massRightNow/1000);
        MyGameManeger.instance.massLeftFactor = script.massLeftRightS/(script.massLeftNow/1000);
        MyGameManeger.instance.massBackwardRightFactor = script.massBackwardS/(script.massBackwardRightNow/1000);
        MyGameManeger.instance.massBackwardLeftFactor = script.massBackwardS/(script.massBackwardLeftNow/1000);
    }
}
