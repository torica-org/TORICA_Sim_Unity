using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyGameManeger : MonoBehaviour
{
    public static MyGameManeger instance = null;
    
    [System.NonSerialized] public bool Landing = false;//着水しているか否か
    [System.NonSerialized] public bool HUDActive = true;//HUDを有効にしているか否か
    [System.NonSerialized] public bool HorizontalLineActive = false;//水平赤線を有効にしているか否か
    [System.NonSerialized] public bool SettingActive = false;//ゲーム設定の表示
    [System.NonSerialized] public bool FlightSettingActive = false;//フライト設定の表示
    [System.NonSerialized] public bool CameraSwitch = true; // true:FPS false:TPS
    [System.NonSerialized] public bool SettingChanged = false;//設定変更
    [System.NonSerialized] public bool MousePitchControl = false;//マウス操作の可否
    [System.NonSerialized] public bool RandomWind = false;//ランダム風の可否
    [System.NonSerialized] public bool SaveCsv = false;//CSVファイルへの保存の可否
    [System.NonSerialized] public bool EnterFlight = false;//フライト開始
    [System.NonSerialized] public float MouseSensitivity = 1.000f; // Magnitude of Gust [m/s]
    [System.NonSerialized] public float GustMag = 0.000f; // Magnitude of Gust [m/s]
    [System.NonSerialized] public float GustDirection = 0.000f; // Direction of Gust [deg]: -180~180
    [System.NonSerialized] public float Airspeed_TO = 5.000f; // Airspeed at take-off [m/s]
    [System.NonSerialized] public float alpha_TO = 0.000f; // Angle of attack at take-off [deg]
    [System.NonSerialized] public string PlaneName;
    [System.NonSerialized] public string FlightMode = "BirdmanRally";
    [System.NonSerialized] public GameObject Plane = null;
    [System.NonSerialized] public Vector3 PlatformPosition = new Vector3(0f,10.5f,0f);
    [System.NonSerialized] public float StartRotation=0.0f;
    [System.NonSerialized] public float TailRotation=0.0f;
    [System.NonSerialized] public float FieldOfView=90;
    //ロードセルのオフセット値
    [System.NonSerialized] public float massLeft0=0;
    [System.NonSerialized] public float massRight0=0;
    [System.NonSerialized] public float massBackwardRight0=0;
    [System.NonSerialized] public float massBackwardLeft0=0;
    [System.NonSerialized] public float JoyStick0=0;
    //ロードセルの調整用係数(この係数をロードセルの値に掛ける)
    [System.NonSerialized] public float massLeftFactor=1;
    [System.NonSerialized] public float massRightFactor=1;
    [System.NonSerialized] public float massBackwardRightFactor=1;
    [System.NonSerialized] public float massBackwardLeftFactor=1;
    //ジョイスティックの調整用係数(この係数をジョイスティックの値に割る)
    [System.NonSerialized] public float JoyStickFactor = 500;
    //エアデータ保存リスト
    [System.NonSerialized] public List<float> AirSpeedList = new List<float>();
    [System.NonSerialized] public List<float> AltList = new List<float>();
    [System.NonSerialized] public List<float> AlphaList = new List<float>();
    [System.NonSerialized] public List<float> BetaList = new List<float>();
    [System.NonSerialized] public List<float> ThetaList = new List<float>();
    [System.NonSerialized] public List<float> PhiList = new List<float>();
    //エラー関係
    [System.NonSerialized] public string errorText;
    [System.NonSerialized] public bool error = false;
    

    [System.NonSerialized] public bool FrameUseable = false;

    [System.NonSerialized] public bool FirstLoad;

    [System.NonSerialized] public int SettingMode = 0;

    [System.NonSerialized] public bool TakeOff = false;

    [System.NonSerialized] public float SoundBolume = 50;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            FirstLoad = true;
            DontDestroyOnLoad(this.gameObject);
            //SceneManager.LoadScene("FlightScene");
        }
        else
        {
            MyGameManeger.instance.FirstLoad = false;
            Destroy(this.gameObject);
        }
        MyGameManeger.instance.EnterFlight = false;
    }
}
