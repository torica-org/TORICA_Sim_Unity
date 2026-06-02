using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Text;
using System.IO;
using System;

public class AerodynamicCalculator : SerialReceive
{
    //設計データ書き込み用変数
    //protected string customCsvPath;//ファイルパス

    //protected string fileName = "CustomPlaneData.csv";//ファイル名
    //public static List<List<string>> CsvList = new List<List<string>>();//CSVファイルリスト
    //protected bool CanReadCsv = false;

    // public

    [System.NonSerialized] public float Airspeed = 0.000f; // Airspeed [m/s]
    [System.NonSerialized] public float alpha = 0.000f; // Angle of attack [deg]
    [System.NonSerialized] public float beta = 0.000f; // Side slip angle [deg]
    [System.NonSerialized] public float de = 0.000f; // Elevator angle [deg]
    [System.NonSerialized] public float dr = 0.000f; // Rudder angle [deg]
    [System.NonSerialized] public float dh = 0.000f; // Movement of c.g. [-]
    [System.NonSerialized] public float LocalGustMag = 0.000f; // Magnitude of local gust [m/s]
    [System.NonSerialized] public float LocalGustDirection = 0.000f; // Magnitude of local gust [m/s]
    [System.NonSerialized] public float nz = 0.000f; // Load factor [-]

    [System.NonSerialized] public float Groundspeed = 0.000f; // Groundspeed [m/s]
    [System.NonSerialized] public float ALT = 0.000f;

    //計算で用いるセンサー値

    [System.NonSerialized] public float massLeft;//左ひずみの値[kg]
    [System.NonSerialized] public float massRight;//右ひずみの値[kg]
    [System.NonSerialized] public float massBackwardRight;//後方左ひずみの値[kg]
    [System.NonSerialized] public float massBackwardLeft;//後方右ひずみの値[kg]

    [System.NonSerialized] public float centerOfMass = 0.000f; // 全体重心計算結果[m] pitchGravity
    [System.NonSerialized] public float centerOfMassPilotRaw = 0.2f; // 補正前重心計算結果[m] pitchGravityPilot
    [System.NonSerialized] public float centerOfMassPilot; // 補正済重心計算結果[m] 定常状態(pitchGravity=0)のパイロット重心 pitchGravityPilotS

    // GameManager.csへ移動
    //[System.NonSerialized] public float centerOfMassPilotOffset; // 重心位置のオフセット値[m]

    [System.NonSerialized] public float massLeftRightS;//定常状態の前センサーの値(合計値ではなく一つのセンサーの値)
    [System.NonSerialized] public float massBackwardS;//定常状態の後センサーの値(合計値ではなく一つのセンサーの値)

    // Phisics

    static protected float rho = 1.164f;
    static protected float hE0 = 10.500f; // Altitude at Take-off [m]

    // At Cruise without Ground Effect

    static protected float Airspeed0; // Magnitude of ground speed [m/s]
    static protected float alpha0; // Angle of attack [deg]
    static protected float CDp0; // Parasitic drag [-]
    static protected float Cmw0; // Pitching momentum [-]
    static protected float CLMAX; // Lift Coefficient [-]
    static protected float CL0 = 0.000f; // Lift Coefficient [-]
    static protected float CLw0 = 0.000f; // Lift Coefficient [-]
    static protected float CLt0 = 0.000f; // Tail Coefficient [-]
    static protected float epsilon0 = 0.000f; // Downwash

    // Plane

    static protected bool Downwash; // Conventional Tail: True, T-Tail: False
    static protected float CL = 0.000f; // Lift Coefficient [-]
    static protected float CD = 0.000f; // Drag Coefficient [-]
    static protected float Cx = 0.000f; // X Force Coefficient [-]
    static protected float Cy = 0.000f; // Y Force Coefficient [-]
    static protected float Cz = 0.000f; // Z Force Coefficient [-]
    static protected float Cl = 0.000f; // Rolling momentum [-]
    static protected float Cm = 0.000f; // Pitching momentum [-]
    static protected float Cn = 0.000f; // Yawing momentum [-]
    static protected float dh0 = 0.000f; // Initial Mouse Position

    // Wing

    static protected float Sw; // Wing area of wing [m^2]
    static protected float bw; // Wing span [m]
    static protected float cMAC; // Mean aerodynamic chord [m]
    static public float aw; // Wing Lift Slope [1/deg]

    static protected float ac;
    static protected float cg;

    static protected float hw; // Length between Wing a.c. and c.g. [-] ac-cg

    static protected float hw0;
    static protected float lt0;

    static protected float AR; // Aspect Ratio [-]
    static protected float ew; // Wing efficiency [-]
    static protected float CLw = 0.000f; // Lift Coefficient [-]

    // Tail

    static protected float St; // Wing area of tail [m^2]
    static protected float at; // Tail Lift Slope [1/deg]
    static protected float lt; // Length between Tail a.c. and c.g. [m]
    static protected float VH; // Tail Volume [-]
    static protected float deMAX; // Maximum elevator angle [deg]
    static protected float tau; // Control surface angle of attack effectiveness [-]
    static protected float CLt = 0.000f; // Lift Coefficient [-]

    // Fin

    static protected float drMAX; // Maximum rudder angle

    // Ground Effect

    static protected float CGEMIN; // Minimum Ground Effect Coefficient [-]
    static protected float CGE = 0f; // Ground Effect Coefficient: CDiGE/CDi [-]

    // Stability derivatives

    static protected float Cyb; // [1/deg]
    static protected float Cyp; // [1/rad]
    static protected float Cyr; // [1/rad]
    static protected float Cydr; // [1/deg]
    static protected float Cnb; // [1/deg]
    static protected float Cnp; // [1/rad]
    static protected float Cnr; // [1/rad]
    static protected float Cndr; // [1/deg]
    static protected float Clb; // [1/deg]
    static protected float Clp; // [1/rad]
    static protected float Clr; // [1/rad]
    static protected float Cldr; // [1/deg]

    // Gust

    static protected Vector3 Gust = Vector3.zero; // Gust [m/s]

    // Rotation

    static protected float phi; // ロール[deg]
    static protected float theta;  // ピッチ[deg]
    static protected float psi; // ヨー[deg]

    protected Rigidbody PlaneRigidbody;

    // ----- 設計値（重心センサーのキャリブレーションや慣性モーメントの算出に使用） -----
    // 全備

    static public float massDefault; // 設計上の全重量[kg]
    static public float centerOfMassDefault; // 設計上の全体重心位置[m]
    static public float IyyDefault; // 設計上のピッチ慣性モーメント[kg*m^2]

    // 空虚
    //static public float massAircraft; // 空虚の機体重量[kg] // 既出
    //static public float centerOfMassAircraft; // 空虚の機体重心位置[m] // 既出
    // パイロット
    static public float massPilotDefault; // 設計上のパイロット重量[kg]

    // ----------------------------------------------------------------------------

    //追加機体データ

    // GameManager.csに移動（DontDestroyであってほしい）
    //static public float gm.lengthForward = 0.660f;//フレーム前方(フレーム＋センサー部分)から桁(原点)位置[m]
    //static public float gm.lengthBackward = 0.330f;//フレーム後方(フレームの端)から桁(原点)位置[m]
    static public float centerOfMassAircraft;//機体のみ全重心(パイロットなし,ピッチのみ)[m]

    static public float massAircraft;//機体のみ全重量[kg]

    static public float massPilot;//設計上のパイロット重量[kg]

    //static protected float SensorPositionY = 0.645f;//桁中心から垂直に線を超音波センサーの位置までおろした時の線の長さ[m]
    //static protected float SensorPositionZ = 0.0f;//↑の到達点から超音波センサーまでの長さ[m]
    //static protected float AircraftHight = 0.74f;//プラホからコクピ下部までの長さ[m]

    static protected bool PlusData;//追加機体データが存在するか

    //計算結果データ

    static protected float hw2;//	主翼空力中心と全機重心の距離（cMACで無次元化）（再計算バージョン）

    //翼持ちデータ

    static protected float YMin;//翼持ちの最小荷重(機体のみ重量/2)
    static protected float YrMax;//右翼持ちの許容最大荷重
    static protected float YlMax;//左翼持ちの許容最大荷重
    static protected float YrMoment;//右翼持ち本人がかけるモーメント
    static protected float YlMoment;//左翼持ち本人がかけるモーメント

    static protected float YL;//機体中心から翼持ち棒までの長さ[m]

    public static GameObject Aircraft;
    static protected GameObject SensorPoint;

    protected bool AddTaleForce;

    private GameManager gm = GameManager.instance; // MyGameManagerをgmとして宣言
    private CameraManager cm;

    public void OnEnables()
    {
        if (gm.PlaneName != null)
        {
            if (this.gameObject.name == gm.PlaneName)
            {
                Aircraft = this.gameObject;
            }
        }
        else
        {
            if (this.gameObject.name == gm.DefaultPlane)
            {
                gm.PlaneName = gm.DefaultPlane;
                Aircraft = this.gameObject;
            }
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        cm = GameObject.Find("CameraManager").GetComponent<CameraManager>();

        // Get rigidbody component
        PlaneRigidbody = this.GetComponent<Rigidbody>();
        this.transform.rotation = Quaternion.Euler(0.0f, Config.TakeoffYaw, 0.0f);

        SensorPoint = GameObject.Find("SensorPoint");

        //設計データ読み込み用
        // fileName = gm.PlaneName + ".csv";
        // customCsvPath = gm.PlaneName + ".csv";
        // customCsvPath = Path.Combine(Directory.GetParent(Application.dataPath).FullName, "CustomPlaneData.csv");
        // Debug.Log("File path: " + customCsvPath);
        // ReadFile();

        // Input Specifications
        InputSpecifications();
        // ----- 設計値の保存 ---------------------------------------------------------------
        centerOfMassDefault = PlaneRigidbody.centerOfMass.x; // 設計上の全体重心位置[m]を保存
        // --------------------------------------------------------------------------------

        //pitchGravityPilotS = ((PlaneRigidbody.mass*pitchGravity)-(massAircraft*centerOfMassAircraft))/massPilot;
        //Debug.Log(massAircraft+","+centerOfMassAircraft+","+massPilot+","+gm.lengthForward+","+gm.lengthBackward);
        /*
        if (gm.massPilotReal == 0)
        {//体重入力省略の場合の処理
            gm.massPilotReal = massPilot;
        }
        */
        if (massAircraft != 0 && centerOfMassAircraft != 0 && massPilot != 0 && gm.lengthForward != 0 && gm.lengthBackward != 0)
        {
            PlusData = true;
            centerOfMassPilot = -1 * massAircraft * centerOfMassAircraft / massPilot;

            //今までのやつ
            /*
            massLeftRightS = (massPilot*(pitchGravityPilotS+gm.lengthBackward)/(gm.lengthForward+gm.lengthBackward))/2;
            massBackwardS = (massPilot - massLeftRightS*2)/2;
            */

            // =====AutoFactorSetter.csへ移植=====
            /*
            if(gm.VRMode){//HMDの質量を加算
                float massPilotVR=gm.massPilotReal+0.588f;
                massLeftRightS = (massPilotVR*(pitchGravityPilotS+gm.lengthBackward)/(gm.lengthForward+gm.lengthBackward)); // 前部荷重の理論値
                massBackwardS = (massPilotVR - massLeftRightS); // 後部荷重の理論値
            }
            else{
                float massPilotNonVR=gm.massPilotReal;
                massLeftRightS = (massPilotNonVR*(pitchGravityPilotS+gm.lengthBackward)/(gm.lengthForward+gm.lengthBackward)); // 前部荷重の理論値
                massBackwardS = (massPilotNonVR - massLeftRightS); // 後部荷重の理論値
            }
            */
        }
        else
        {
            PlusData = false;
        }

        YMin = massAircraft / 2;
        YrMax = 80.0f;
        YlMax = 80.0f;

        FlightModelStart();
    }

    private void Update()//フライトモデルに関わらず実行されるINPUT関連の処理
    {
        float pitchGravityBefore = centerOfMass;
        float pitchGravityPilotBefore = centerOfMassPilotRaw;

        if (Config.UseMousePitchControl)
        {//マウスコントロール
            if (PlusData)
            {
                //Debug.Log(PlusData);
                centerOfMassPilot = -massAircraft * centerOfMassAircraft / massPilot;
                centerOfMassPilotRaw = centerOfMassPilot + ((Input.mousePosition.y - dh0) * Config.MouseSensitivity) * 0.0002f;
            }
            //pitchGravity = ((pitchGravityPilot*massPilot)+(centerOfMassAircraft*massAircraft))/PlaneRigidbody.mass;
            centerOfMass = (gm.CenterOfMassErrorValue + ((Input.mousePosition.y - dh0) * Config.MouseSensitivity) * 0.0002f) * gm.CenterOfMassRandValue;
        }

        if (Input.GetAxisRaw("GStick") != 0)
        {//ゲームパッドコントロールのトリガー
            centerOfMassPilot = -massAircraft * centerOfMassAircraft / massPilot;
            centerOfMassPilotRaw = centerOfMassPilot - Input.GetAxisRaw("GStick") * 0.10f;
            centerOfMass = (gm.CenterOfMassErrorValue + ((centerOfMassPilotRaw * massPilot) + (centerOfMassAircraft * massAircraft)) / PlaneRigidbody.mass) * gm.CenterOfMassRandValue;
        }

        if (gm.FrameUseable)//フレームコントロール
        {
            /*
            massLeftNow = 20000f;
            massRightNow = 20000f;
            massBackwardRightNow = 20000f;
            massBackwardLeftNow = 20000f;
            */

            //mass~Now ←センサー生データ
            //mass~Factor ←Rawを調整するための係数
            //mass~ ←NowにFactorの値をかけて計算に使用する値

            // マイコン側でkgに変換する
            massRight = gm.massRightFactor * (massRightNow);
            massBackwardRight = gm.massBackwardRightFactor * (massBackwardRightNow);

            // massLeft = gm.massLeftFactor*(massLeftNow/1000);
            // massBackwardLeft = gm.massBackwardLeftFactor*(massBackwardLeftNow/1000);

            /*
            int safeModeCount = 0;
            int safeModeCount2 = 0;
            if(massRight == 0){
                safeModeCount++;
            }
            else{
                safeModeCount2 = 1;
            }

            if(massLeft == 0){
                safeModeCount++;
            }
            else{
                safeModeCount2 = 2;
            }
            if(massBackwardLeft == 0){
                safeModeCount++;
            }
            else{
                safeModeCount2 = 3;
            }

            if(massBackwardRight == 0){
                safeModeCount++;
            }
            else{
                safeModeCount2 = 4;
            }

            if(safeModeCount == 3){
                Debug.Log("SafeMode");

                if(safeModeCount2 == 1){
                    massBackwardLeft = 53 - massRight;
                }

                if(safeModeCount2 == 2){
                    massBackwardLeft = 53 - massLeft;
                }

                if(safeModeCount2 == 3){
                    massLeft = 53 - massBackwardLeft;
                }

                if(safeModeCount2 == 4){
                    massLeft = 53 - massBackwardRight;
                }
            }
            */

            // float NowMass = massLeft + massRight + massBackwardRight + massBackwardLeft;
            massPilot = massRight + massBackwardRight;

            /*
            // pitchGravity = (gm.CenterOfMassErrorValue + (((gm.lengthForward*massLeft)+(gm.lengthForward*massRight)-(gm.lengthBackward*(massBackwardRight + massBackwardLeft))+(centerOfMassAircraft*massAircraft))/(massLeft+massRight+(massBackwardRight + massBackwardLeft)+massAircraft)))*gm.CenterOfMassRandValue;
            centerOfMass = (gm.CenterOfMassErrorValue + ((gm.lengthForward * massRight) - (gm.lengthBackward * massBackwardRight) + (centerOfMassAircraft * massAircraft)) / (massRight +massBackwardRight+ massAircraft)) * gm.CenterOfMassRandValue;

            if (-0.4f < centerOfMass && centerOfMass < 0.4f){//外れ値除去処理(基本的に重心は±0.4を超えることはない)
                //リジットボディに代入するピッチの値を計算
                //pitchGravity = (GameManager.instance.CenterOfMassErrorValue + (((gm.lengthForward*massLeft)+(gm.lengthForward*massRight)-(gm.lengthBackward*(massBackwardRight + massBackwardLeft))+(centerOfMassAircraft*massAircraft))/(massLeft+massRight+(massBackwardRight + massBackwardLeft)+massAircraft)))*GameManager.instance.CenterOfMassRandValue;
                centerOfMassPilot = ((PlaneRigidbody.mass*centerOfMass)-(massAircraft*centerOfMassAircraft))/massPilot;
                if(NowMass != 0 ){
                    // pitchGravityPilot = (((gm.lengthForward*massLeft)+(gm.lengthForward*massRight)-(gm.lengthBackward*(massBackwardRight + massBackwardLeft)))/(massLeft+massRight+(massBackwardRight + massBackwardLeft)));
                    centerOfMassPilotRaw = (((gm.lengthForward * massRight) - (gm.lengthBackward * massBackwardRight)) / (massRight + massBackwardRight));
                }
                else{
                    centerOfMassPilotRaw = centerOfMassPilot;
                }
            }else{
                Debug.Log("外れ値除去成功！");
                centerOfMass = pitchGravityBefore;
                centerOfMassPilot = pitchGravityPilotBefore;
            }
            */

            // 重心フレーム上での桁中心モーメントについて，（前後センサにかかる荷重によるモーメント）＝（パイロットの体重によるモーメント）とし，その両辺をパイロットの体重で割った式
            centerOfMassPilotRaw = (gm.lengthForward * massRight + gm.lengthBackward * massBackwardRight) / (massRight + massBackwardRight); // 補正前のパイロット重心[m]

            // 補正
            centerOfMassPilot = centerOfMassPilotRaw + gm.centerOfMassPilotOffset; // 補正後のパイロット重心[m]

            // 桁中心モーメントについて，（パイロット体重と空虚重量〈パイロットなしの機体重量〉によるモーメント）＝（全備重量によるモーメント）とし，その両辺を全備重量で割った式
            centerOfMass = (massPilot * centerOfMassPilot + massAircraft * centerOfMassAircraft) / (massPilot + massAircraft);

            if (-0.4f < centerOfMass && centerOfMass < 0.4f)//外れ値除去処理(基本的に重心は±0.4を超えることはない
            { }
            else
            {
                Debug.Log("外れ値除去成功！");
                centerOfMass = pitchGravityBefore;
                centerOfMassPilot = pitchGravityPilotBefore;
            }
        }
        // Get control surface angles
        de = 0.000f;
        dr = 0.000f;

        if (!gm.FrameUseable)
        {
            de = Input.GetAxisRaw("Vertical") * deMAX;
            dr = -Input.GetAxisRaw("Horizontal") * drMAX * gm.RudderRandValue;
        }
        if (Input.GetMouseButton(0)) { dr = drMAX * gm.RudderRandValue; }
        else if (Input.GetMouseButton(1)) { dr = -drMAX * gm.RudderRandValue; }

        if (Input.GetAxisRaw("Trigger") * drMAX != 0)
        {
            dr = -Input.GetAxisRaw("Trigger") * drMAX * gm.RudderRandValue;
        }

        if (gm.FrameUseable)
        {
            //↓必要な処理
            dr = ((JoyStickNow - gm.JoyStick0) / gm.JoyStickFactor) * drMAX * gm.RudderRandValue;
        }

        if (gm.RudderError && gm.RudderErrorMode != 0)
        {
            if (gm.RudderErrorMode == 1)
            {
                dr = gm.RudderErrorValue * drMAX;
            }
            else if (gm.RudderErrorMode == 2)
            {
                if (UnityEngine.Random.Range(0.0f, 1.0f) < 0.5f)
                {
                    dr = gm.RudderErrorValue * drMAX;
                }
            }
            else if (gm.RudderErrorMode == 3)
            {
                dr += gm.RudderErrorValue * drMAX;
            }
        }

        // VR Only Mode (重心センサーを使う場合は使用しない)
        if (gm.VRMode && !gm.FrameUseable)
        {
            massPilot = 68.0f; // [kg]

            centerOfMassPilot = cm.GetZAxisMovement(); // パイロット重心は直接取得できる.

            // 桁中心モーメントについて，（パイロット体重と空虚重量〈パイロットなしの機体重量〉によるモーメント）＝（全備重量によるモーメント）とし，その両辺を全備重量で割った式
            centerOfMass = (massPilot * centerOfMassPilot + massAircraft * centerOfMassAircraft) / (massPilot + massAircraft);
        }
    }

    private void FixedUpdate()
    {
        FlightModelFixedUpdate();
    }

    public void InputSpecifications()
    {
        // 機体の重量と慣性モーメント - 6
        PlaneRigidbody.mass = AircraftData.mass;
        PlaneRigidbody.centerOfMass = AircraftData.centerOfMass;
        PlaneRigidbody.inertiaTensor = AircraftData.inertiaTensor;
        PlaneRigidbody.inertiaTensorRotation = AircraftData.inertiaTensorRotation;
        massAircraft = AircraftData.massAircraft;
        centerOfMassAircraft = AircraftData.centerOfMassAircraft;

        // 巡航時 - 5
        Airspeed0 = AircraftData.Airspeed0;
        alpha0 = AircraftData.alpha0;
        CDp0 = AircraftData.CDp0;
        Cmw0 = AircraftData.Cmw0;
        CLMAX = AircraftData.CLMAX;

        // 主翼 - 7
        Sw = AircraftData.Sw;
        bw = AircraftData.bw;
        cMAC = AircraftData.cMAC;
        aw = AircraftData.aw;
        hw = AircraftData.hw;
        ew = AircraftData.ew;
        AR = AircraftData.AR;

        // 水平尾翼 - 7
        Downwash = AircraftData.Downwash;
        St = AircraftData.St;
        at = AircraftData.at;
        lt = AircraftData.lt;
        deMAX = AircraftData.deMAX;
        tau = AircraftData.tau;
        VH = AircraftData.VH;

        // 垂直尾翼 - 1
        drMAX = AircraftData.drMAX;

        // 地面効果 - 1
        CGEMIN = AircraftData.CGEMIN;

        // 安定微係数 - 12
        Cyb = AircraftData.Cyb;
        Cyp = AircraftData.Cyp;
        Cyr = AircraftData.Cyr;
        Cydr = AircraftData.Cydr;
        Clb = AircraftData.Clb;
        Clp = AircraftData.Clp;
        Clr = AircraftData.Clr;
        Cldr = AircraftData.Cldr;
        Cnb = AircraftData.Cnb;
        Cnp = AircraftData.Cnp;
        Cnr = AircraftData.Cnr;
        Cndr = AircraftData.Cndr;

        // 離陸 - 1
        YL =  AircraftData.YL;
    }
    public virtual void FlightModelStart()
    {
    }

    public virtual void FlightModelFixedUpdate()
    {
    }
}
