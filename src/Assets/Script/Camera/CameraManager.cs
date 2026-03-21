using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [TextArea(5, 15)]
    [Tooltip("備忘録や仕様のメモなどを自由に書き込めます")]
    public string note = "複数ディスプレイにも対応した画面切り替えとVRの制御を行っています．" + 
        "XR Origin (XR Rig)とXR Interaction Managerはとりあえず格納しているだけです．";

    private GameManager gm = GameManager.instance;

    private GameObject FPSObj; // 一人称視点Cameraのオブジェクト
    private Camera FPSCamera; // 一人称視点Camera

    private GameObject TPSObj; // 三人称視点Cameraのオブジェクト
    private Camera TPSCamera; // 三人称視点Camera

    private GameObject XROrigin; // VR用のXR Origin (XR Rig)
    private static Camera XRCamera = null; // XR OriginのCamera

    private ManualXRControl manualXRControl = new(); // VR制御用クラスを保持するフィールド

    private AudioListener audioListener; // AudioListenerコンポーネントを保持するフィールド

    private bool isVRInitialized = false; // VRが初期化されたかどうかを示すフラグ

    private static Vector3 caribrationOffset = Vector3.zero; // キャリブレーションのオフセットを保持するフィールド.


    // ===== オブジェクトが生成された際に実行されるメソッド =====================================
    void Start()
    {
        // Cameraのオブジェクトとコンポーネントを取得.
        FPSObj = GameManager.instance.Plane.transform.Find("FPSCamera").gameObject;
        FPSCamera = FPSObj.GetComponent<Camera>();
        TPSObj = GameManager.instance.Plane.transform.Find("TPSCamera").gameObject;
        TPSCamera = TPSObj.GetComponent<Camera>();

        // XR Origin (XR Rig) オブジェクトとCameraコンポーネントを取得
        //XROrigin = this.gameObject.transform.Find("XR Origin (XR Rig)").gameObject;
        XROrigin = GameObject.Find("XR Origin (XR Rig)");
        XRCamera = XROrigin.transform.Find("Camera Offset/Main Camera").GetComponent<Camera>();
        //XROrigin.SetActive(false);

        // AudioListenerコンポーネントをSystemControllerから取得して保持する.
        audioListener = GameObject.Find("SystemController").GetComponent<AudioListener>();

        //gm.VRMode = true;

    }

    // ===== 毎フレーム実行されるメソッド =================================================
    void Update()
    {
        // "v"キーが押されたらカメラを切り替える.
        if (Input.GetKeyDown("v"))
        {
            GameManager.instance.isMainDisplayTPS = !GameManager.instance.isMainDisplayTPS;
        }

        // VRモードの切り替えを検知して、必要に応じてVR制御を開始する.
        if (gm.VRMode && !isVRInitialized)
        {
            isVRInitialized = true; // VR初期化済のフラグを立てる.

            TPSCamera.targetDisplay = 7; // 退避.
            FPSCamera.targetDisplay = 8; // 退避.

            //XROrigin.SetActive(true); // XR Originオブジェクトをアクティブにする.
            //FPSObj.SetActive(false); // FPSカメラオブジェクトを非アクティブにする.
            audioListener.enabled = false; // AudioListenerを無効にする（VRモードではXR OriginのAudioListenerが使用されるため）.

            try
            {
                StartCoroutine(manualXRControl.StartXRCoroutine());
            }
            catch (System.Exception e)
            {
                Debug.LogError("VR init error: " + e.Message);

                gm.VRMode = false;
                isVRInitialized = false; // 初期化に失敗した場合はフラグを下ろす.

                return;
            }

            //gm.isMainDisplayTPS = false; // VRヘッドセットがメインディスプレイになる模様.

            // 初回のみ
            Quaternion FPSRotation = FPSObj.transform.rotation; // FPSカメラの回転を保存.
            this.gameObject.transform.rotation = FPSRotation; // CameraManagerの回転をFPSカメラの回転に合わせる.
        }
        else if (!gm.VRMode && isVRInitialized)
        {
            isVRInitialized = false; // VR初期化済のフラグを下ろす.

            manualXRControl.StopXR(); // VRを停止する.

            //XROrigin.SetActive(false); // XR Originオブジェクトを非アクティブにする.
            //FPSObj.SetActive(true); // FPSカメラオブジェクトをアクティブにする.
            audioListener.enabled = true; // AudioListenerを有効にする.

            //gm.isMainDisplayTPS = true; // PCモニターがメインディスプレイに戻る.
        }

        SyncCameraFlag();

        Vector3 FPSPosition = FPSObj.transform.position; // FPSカメラの位置を保存.
        this.gameObject.transform.position = FPSPosition - caribrationOffset; // CameraManagerの位置をFPSカメラの位置に合わせる（キャリブレーションオフセットを考慮）.
    }


    // ===== カメラの状態をフラグと同期するメソッド =================================================
    private void SyncCameraFlag()
    {
        int displayNum = -1;
        if (isVRInitialized)
        {
            displayNum = XRCamera.targetDisplay; // これはおそらく0になるはず.
        }
        
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
    }


    // ===== キャリブレーションを行うメソッド（staticなのでインスタンス化無しで呼べる） ============
    public static void Caribrate()
    {
        if (XRCamera == null)
        {
            Debug.LogWarning("Caribration failed: XRCamera is not initialized.");
            return;
        }

        // 進行方向（x軸）を前後とする.
        Vector3 vrCameraLocalOffset = XRCamera.transform.localPosition; // XRカメラのローカル位置を取得.
        // XRCameraのローカル位置はグローバル座標と同じように，前後: z軸，上下: y軸，左右: x軸で保持されている.

        // 現在のXRカメラのローカル位置をキャリブレーションオフセットとして保存.
        caribrationOffset.x = vrCameraLocalOffset.z; // 前後方向のオフセット.
        caribrationOffset.y = vrCameraLocalOffset.y; // 上下方向のオフセット.
        caribrationOffset.z = vrCameraLocalOffset.x; // 左右方向のオフセット.
        // CameraManagerの向きはFPSカメラと同様にy軸について90deg回転しているため，前後: x軸，上下: y軸，左右: z軸である.
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


    // ===== IMGUIが使用できるメソッド ===================================================
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 70, 100, 50), "Caribrate"))
        {
            Caribrate();
        }
    }

}
