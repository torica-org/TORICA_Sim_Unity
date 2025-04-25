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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPush(){
        
    }
}
