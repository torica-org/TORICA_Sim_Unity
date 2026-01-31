using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoFactorSetter : MonoBehaviour
{
    private float DefaultPitchGravity;
    private AerodynamicCalculator script;
    [SerializeField] private InputField inputField;//ポート番号入力フィールド

    private MyGameManeger gm = MyGameManeger.instance;

    // Start is called before the first frame update
    void Start()
    {
        script = gm.Plane.GetComponent<AerodynamicCalculator>();
    }

    public void OnPush(){
        /*
        if(script.massRightNow != 0){MyGameManeger.instance.massRightFactor = script.massLeftRightS/(script.massRightNow/1000);}
        else{MyGameManeger.instance.massRightFactor = 0;}

        if(script.massLeftNow != 0){MyGameManeger.instance.massLeftFactor = script.massLeftRightS/(script.massLeftNow/1000);}
        else{MyGameManeger.instance.massLeftFactor = 0;}

        if(script.massBackwardRightNow != 0){MyGameManeger.instance.massBackwardRightFactor = script.massBackwardS/(script.massBackwardRightNow/1000);}
        else{MyGameManeger.instance.massBackwardRightFactor = 0;}

        if(script.massBackwardLeftNow != 0){MyGameManeger.instance.massBackwardLeftFactor = script.massBackwardS/(script.massBackwardLeftNow/1000);}
        else{MyGameManeger.instance.massBackwardLeftFactor = 0;}
        */


        /*
        if(script.massRightNow != 0){gm.massRightFactor = script.massLeftRightS/((script.massRightNow+script.massLeftNow)/1000);}
        else{gm.massRightFactor = 0;}

        if(script.massLeftNow != 0){gm.massLeftFactor = script.massLeftRightS/((script.massRightNow+script.massLeftNow)/1000);}
        else{gm.massLeftFactor = 0;}

        if(script.massBackwardRightNow != 0){gm.massBackwardRightFactor = script.massBackwardS/((script.massBackwardRightNow+script.massBackwardLeftNow)/1000);}
        else{gm.massBackwardRightFactor = 0;}

        if(script.massBackwardLeftNow != 0){gm.massBackwardLeftFactor = script.massBackwardS/((script.massBackwardRightNow+script.massBackwardLeftNow)/1000);}
        else{gm.massBackwardLeftFactor = 0;}
        */
        if (inputField.text != "")
        {
            gm.pilotMassReal = float.Parse(inputField.text);
            Debug.Log(gm.pilotMassReal);
        }
        else
        {
            gm.pilotMassReal = (script.massRightNow + script.massBackwardRightNow)/1000;
        }

        // Debug.Log("pilotMassReal: " + gm.pilotMassReal);

        float pilotMass;

        if (gm.VRMode)
        {//HMDの質量を加算
            pilotMass = gm.pilotMassReal + 0.588f;
        }
        else
        {
            pilotMass = gm.pilotMassReal;
        }

        script.massLeftRightS = (pilotMass*AerodynamicCalculator.lengthBackward - AerodynamicCalculator.aircraftMass*AerodynamicCalculator.aircraftCenterOfMass) / (AerodynamicCalculator.lengthForward + AerodynamicCalculator.lengthBackward); // 前部荷重の理論値
        script.massBackwardS = (pilotMass - script.massLeftRightS); // 後部荷重の理論値
        // Debug.Log("lengthForward + lengthBackward" + (AerodynamicCalculator.lengthForward + AerodynamicCalculator.lengthBackward));
        // Debug.Log("pitchGravityPilotS: " + script.pitchGravityPilotS);

        // Debug.Log("Front: " + script.massLeftRightS + " Rear: " + script.massBackwardS);

        if (script.massRightNow != 0) 
        { 
            gm.massRightFactor = script.massLeftRightS / (script.massRightNow / 1000);
            // Debug.Log(script.massLeftRightS + "(Front) / " + script.massRightNow + "(Rear) / 1000 = " + gm.massRightFactor); 
        }
        else
        { 
            gm.massRightFactor = 0; 
        }

        if (script.massBackwardRightNow != 0) 
        { 
            gm.massBackwardRightFactor = script.massBackwardS / (script.massBackwardRightNow / 1000);
            // Debug.Log(script.massBackwardS + "(Front) / " + script.massBackwardRightNow + "(Rear) / 1000 = " + gm.massBackwardRightFactor);
        }
        else
        { 
            gm.massBackwardRightFactor = 0; 
        }

    }
}
