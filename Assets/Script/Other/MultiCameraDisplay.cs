using UnityEngine;
using UnityEngine.XR;

public class MultiCameraDisplay : MonoBehaviour
{
    Camera _thirdViewCamera;
    
    void Start()
    {
        //カメラコンポーネントを取得
        _thirdViewCamera = GetComponent<Camera>();
        //PCディスプレイ表示を三人称視点カメラに切り替え
        OnThirdView();
    }
    
    //PCディスプレイにプレイヤー目線を表示
    void OnPlayerView()
    {
        _thirdViewCamera.enabled = false;
        XRSettings.gameViewRenderMode = GameViewRenderMode.LeftEye;
    }
    
    //PCディスプレイにThirdViewCamera映像を表示
    void OnThirdView()
    {
        _thirdViewCamera.enabled = true;
        XRSettings.gameViewRenderMode = GameViewRenderMode.None;
    }
}
