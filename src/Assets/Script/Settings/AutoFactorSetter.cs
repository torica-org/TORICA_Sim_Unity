using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoFactorSetter : MonoBehaviour
{
    private float DefaultPitchGravity;
    private AerodynamicCalculator script;
    [SerializeField] private InputField inputField;//ポート番号入力フィールド

    private GameManager gm = GameManager.instance;

    // Start is called before the first frame update
    void Start()
    {
        script = gm.Plane.GetComponent<AerodynamicCalculator>();
    }

    public void OnPush(){
        /*
        if(script.massRightNow != 0){GameManager.instance.massRightFactor = script.massLeftRightS/(script.massRightNow/1000);}
        else{GameManager.instance.massRightFactor = 0;}

        if(script.massLeftNow != 0){GameManager.instance.massLeftFactor = script.massLeftRightS/(script.massLeftNow/1000);}
        else{GameManager.instance.massLeftFactor = 0;}

        if(script.massBackwardRightNow != 0){GameManager.instance.massBackwardRightFactor = script.massBackwardS/(script.massBackwardRightNow/1000);}
        else{GameManager.instance.massBackwardRightFactor = 0;}

        if(script.massBackwardLeftNow != 0){GameManager.instance.massBackwardLeftFactor = script.massBackwardS/(script.massBackwardLeftNow/1000);}
        else{GameManager.instance.massBackwardLeftFactor = 0;}
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

        //float pilotMass;

        if (inputField.text != "")
        {
            gm.pilotMassReal = float.Parse(inputField.text);
            Debug.Log(gm.pilotMassReal);
        }
        else
        {
            gm.pilotMassReal = script.massRightNow + script.massBackwardRightNow;
            if (gm.VRMode)
            {//HMDの質量を加算 -> 減算に修正
                gm.pilotMassReal -= 0.588f;
            }
        }

        // 重心フレーム上での桁中心モーメントについて，（前後センサにかかる荷重によるモーメント）＝（パイロットの体重によるモーメント）とし，その両辺をパイロットの体重で割った式
        script.pilotCenterOfGRaw = (script.massRightNow * AerodynamicCalculator.lengthForward + script.massBackwardRightNow * AerodynamicCalculator.lengthBackward) / gm.pilotMassReal; // 補正前のパイロット重心[m]
        Debug.Log("Raw: " + script.pilotCenterOfGRaw);

        // 機体が定常であるためには，（パイロットの体重によるモーメント）＝（空虚重量〈パイロットなしの機体重量〉によるモーメント）である必要があることを利用
        // シミュレーター上での桁中心モーメントについて，（パイロットの体重によるモーメント）＝（空虚重量〈パイロットなしの機体重量〉によるモーメント）とし，その両辺をパイロットの体重で割った式
        float pilotCenterOfGTheoretical = (-1 * AerodynamicCalculator.aircraftMass * AerodynamicCalculator.aircraftCenterOfMass) / gm.pilotMassReal; // 定常におけるパイロット重心の理論値[m]
        Debug.Log("pilot_th: " + pilotCenterOfGTheoretical);

        // 補正値の算出
        script.pilotCenterOfGOffset = pilotCenterOfGTheoretical - script.pilotCenterOfGRaw; // パイロット重心の補正値
        Debug.Log("offset: " + script.pilotCenterOfGOffset);

        // Debug.Log("pilotMassReal: " + gm.pilotMassReal);

        /*
        if (gm.VRMode)
        {//HMDの質量を加算 -> 減算に修正
            pilotMass = gm.pilotMassReal - 0.588f;
        }
        else
        {
            pilotMass = gm.pilotMassReal;
        }
        */

        //script.massLeftRightS = (pilotMass*AerodynamicCalculator.lengthBackward - AerodynamicCalculator.aircraftMass*AerodynamicCalculator.aircraftCenterOfMass) / (AerodynamicCalculator.lengthForward + AerodynamicCalculator.lengthBackward); // 前部荷重の理論値
        //script.massBackwardS = (pilotMass - script.massLeftRightS); // 後部荷重の理論値
        // Debug.Log("lengthForward + lengthBackward" + (AerodynamicCalculator.lengthForward + AerodynamicCalculator.lengthBackward));
        // Debug.Log("pitchGravityPilotS: " + script.pitchGravityPilotS);

        // Debug.Log("Front: " + script.massLeftRightS + " Rear: " + script.massBackwardS);

        /*
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
        */

    }
}
