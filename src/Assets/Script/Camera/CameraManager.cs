using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class CameraManager : MonoBehaviour
{
    [TextArea(5, 15)]
    [Tooltip("備忘録や仕様のメモなどを自由に書き込めます")]
    public string note = "複数ディスプレイにも対応した画面切り替えとVRの制御を行っています．" +
        "XR Origin (XR Rig)とXR Interaction Managerを格納し，VRの原点を決めています．";

    private GameManager gm;
    private UIManager ui;

    private GameObject FPSObj; // 一人称視点Cameraのオブジェクト
    private Camera FPSCamera; // 一人称視点Camera

    private GameObject TPSObj; // 三人称視点Cameraのオブジェクト
    private Camera TPSCamera; // 三人称視点Camera

    private GameObject XROrigin; // VR用のXR Origin (XR Rig)
    private static Camera XRCamera = null; // XR OriginのCamera

    private ManualXRControl manualXRControl = new(); // VR制御用クラスを保持するフィールド.
    private MultiCameraDisplay multiCameraDisplay; // カメラ制御用クラスを保持するフィールド.

    private AudioListener audioListener; // AudioListenerコンポーネントを保持するフィールド

    private static bool isVRInitialized = false; // VRが初期化されたかどうかを示すフラグ

    private static Vector3 caribrationOffset = Vector3.zero; // キャリブレーションのオフセットを保持するフィールド.

    // ===== オブジェクトが生成された際に実行されるメソッド =====================================
    void Start()
    {
        gm = GameManager.instance; // `GameManager`のインスタンスを取得して代入.
        ui = UIManager.instance; // `UIManager`のインスタンスを取得して代入.

        // Cameraのオブジェクトとコンポーネントを取得.
        FPSObj = GameManager.instance.Plane.transform.Find("FPSCamera").gameObject;
        FPSCamera = FPSObj.GetComponent<Camera>();
        TPSObj = GameManager.instance.Plane.transform.Find("TPSCamera").gameObject;
        TPSCamera = TPSObj.GetComponent<Camera>();

        //ui.SetRenderCamera(TPSCamera); // UIManagerにFPSCameraを渡す.

        // XR Origin (XR Rig) オブジェクトとCameraコンポーネントを取得
        XROrigin = GameObject.Find("XR Origin (XR Rig)");
        XRCamera = XROrigin.transform.Find("Camera Offset/Main Camera").GetComponent<Camera>();
        multiCameraDisplay = new();
        //multiCameraDisplay.ConfigureEventSystem(); // EventSystemの設定を行う.

        XRSettings.gameViewRenderMode = GameViewRenderMode.None;

        // AudioListenerコンポーネントをSystemControllerから取得して保持する.
        audioListener = GameObject.Find("SystemController").GetComponent<AudioListener>();
    }

    // ===== 毎フレーム実行されるメソッド =================================================
    void Update()
    {
        // "f5"キーが押されたらカメラを切り替える.
        if (Input.GetKeyDown("f5") && !gm.VRMode)
        {
            gm.isMainDisplayTPS = !gm.isMainDisplayTPS;
        }
        // "v"キーが押されたらVRモードを切り替える.
        if (Input.GetKeyDown("v"))
        {
            gm.VRMode = !gm.VRMode;
        }
        // "c"キーが押されたらVRのキャリブレーションを行う.
        if (Input.GetKeyDown("c"))
        {
            CaribrateVR();
        }
        //
        if (Input.GetKeyDown("f6"))
        {
            multiCameraDisplay.ToggleSubDisplay();
        }

        // VRモードの切り替えを検知して、必要に応じてVR制御を開始する.
        if (gm.VRMode && !isVRInitialized)
        {
            isVRInitialized = true; // VR初期化済のフラグを立てる.

            // VRのカメラとtargetDisplayが被るとエラーとなるが，`enabled = false`なら大丈夫.
            FPSCamera.targetDisplay = 0; // 固定.
            TPSCamera.targetDisplay = 1; // 一時的に固定.
            XRCamera.targetDisplay = 0; // インスペクターでも`Display 1`に設定済みだが念の為.

            FPSCamera.enabled = false; // FPSカメラを無効にする（VRモードではXR OriginのCameraが使用されるため）.
            XRCamera.enabled = true; // XRカメラを有効にする.

            // 他のカメラのTargetEyeをNoneにする（念の為上書き）.
            //FPSCamera.stereoTargetEye = StereoTargetEyeMask.None;
            //TPSCamera.stereoTargetEye = StereoTargetEyeMask.None;

            audioListener.enabled = false; // AudioListenerを無効にする（VRモードではXR OriginのAudioListenerが使用されるため）.

            try
            {
                StartCoroutine(manualXRControl.StartXRCoroutine());
                multiCameraDisplay.SetGameViewRenderMode(false); // ゲームビューにVR映像を出力しない.
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("VR init error: " + e.Message);

                manualXRControl.StopXR();
                gm.VRMode = false;
                isVRInitialized = false; // 初期化に失敗した場合はフラグを下ろす.

                return;
            }

            TPSCamera.targetDisplay = 0; // 固定.

            Vector3 zAxisRotation = new(0f, 90f, 0f);
            this.gameObject.transform.eulerAngles = zAxisRotation; // CameraManagerの向きをFPSカメラと同様にy軸について90deg回転させる.
        }
        else if (!gm.VRMode && isVRInitialized)
        {
            isVRInitialized = false; // VR初期化済のフラグを下ろす.

            manualXRControl.StopXR(); // VRを停止する.

            XRCamera.enabled = false;
            FPSCamera.enabled = true;

            audioListener.enabled = true; // AudioListenerを有効にする.
        }

        SyncCameraFlag();

        // `XR Origin`は`GameManager.instance.Plane`の子にせず，スクリプトで追従させる.
        // `XR Origin`の親であるCameraManager(=this.gameObject)をFPSカメラの位置と合わせる.
        Vector3 FPSPosition = FPSObj.transform.position; // FPSカメラの位置を保存.
        this.gameObject.transform.position = FPSPosition - caribrationOffset; // CameraManagerの位置をFPSカメラの位置に合わせる（キャリブレーションオフセットを考慮）.

        //Debug.Log("HMD Z Axis Movement: " + GetZAxisMovement());
        //Debug.Log("displays connected: " + Display.displays.Length);
    }

    // ===== カメラの状態をフラグと同期するメソッド =================================================
    private void SyncCameraFlag()
    {
        //int displayNum = -1;

        if (isVRInitialized)
        {
            //displayNum = XRCamera.targetDisplay; // これはおそらく0になるはず.
            return;
        }

        /*
        if (gm.isMainDisplayTPS)
        {
            TPSCamera.targetDisplay = displayNum + 1; // VR OFF -> 0, VR ON -> 1
            FPSCamera.targetDisplay = displayNum + 2; // VR OFF -> 1, VR ON -> 2
        }
        else
        {
            TPSCamera.targetDisplay = displayNum + 2; // VR OFF -> 1, VR ON -> 2
            FPSCamera.targetDisplay = displayNum + 1; // VR OFF -> 0, VR ON -> 1
        }
        */
        if (gm.isMainDisplayTPS)
        {
            TPSCamera.targetDisplay = 0; // VR OFF -> 0
            FPSCamera.targetDisplay = 1; // VR OFF -> 1
        }
        else
        {
            TPSCamera.targetDisplay = 1; // VR OFF -> 1
            FPSCamera.targetDisplay = 0; // VR OFF -> 0
        }
    }

    // ===== キャリブレーションを行うメソッド（staticなのでインスタンス化無しで呼べる） ============
    public static void CaribrateVR()
    {
        if (!XRCamera || !isVRInitialized)
        {
            Debug.LogWarning("Caribration failed: XRCamera is not initialized.");
            return;
        }

        // カメラにとっては，前後: z軸，左右: x軸である．

        Vector3 vrCameraLocalOffset = XRCamera.transform.localPosition; // XRカメラのローカル位置を取得.
        // XRCameraのローカル位置はグローバル座標と同じように，前後: z軸，上下: y軸，左右: x軸で保持されている.

        // 現在のXRカメラのローカル位置をキャリブレーションオフセットとして保存.
        caribrationOffset.x = vrCameraLocalOffset.z; // 前後方向のオフセット.
        caribrationOffset.y = vrCameraLocalOffset.y; // 上下方向のオフセット.
        caribrationOffset.z = vrCameraLocalOffset.x; // 左右方向のオフセット.
        // CameraManagerの向きはFPSカメラと同様にy軸について90deg回転しているため，前後: x軸，上下: y軸，左右: z軸である.
    }

    // ===== VRゴーグルの前後移動量を返すメソッド ============================
    public static float GetZAxisMovement()
    {
        if (!XRCamera || !isVRInitialized)
        {
            Debug.LogWarning("Getting movement failed: XRCamera is not initialized.");
            return 0f;
        }
        return XRCamera.transform.localPosition.z; // 前後移動量を返す.
    }

    // ===== オブジェクトが破棄された際に実行されるメソッド ======================================
    void OnDestroy()
    {
        // オブジェクトが破棄される際にVRを停止する.
        if (isVRInitialized)
        {
            isVRInitialized = false;
            manualXRControl.StopXR();
        }
    }

    // ===== アプリケーションが終了する際に実行されるメソッド ===================================
    void OnApplicationQuit()
    {
        // アプリケーションが終了する際にVRを停止する.
        if (isVRInitialized)
        {
            isVRInitialized = false;
            manualXRControl.StopXR();
        }
    }
}