using UnityEngine;
using UnityEngine.XR;

public class MultiCameraDisplay : MonoBehaviour
{
    Camera _thirdViewCamera;

    void Start()
    {

        Debug.Log("displays connected: " + Display.displays.Length);
        // Display.displays[0] は主要なデフォルトのディスプレイで、常にオンです。ですから、インデックス 1 から始まります。
        // その他のディスプレイが使用可能かを確認し、それぞれをアクティブにします。

        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }

        //カメラコンポーネントを取得
        _thirdViewCamera = GetComponent<Camera>();
        //PCディスプレイ表示を三人称視点カメラに切り替え
        OnThirdView();
    }

    //PCディスプレイにプレイヤー目線を表示
    void OnPlayerView()
    {
        _thirdViewCamera.enabled = false;

        if (GameManager.instance.VRMode)
        {
            XRSettings.gameViewRenderMode = GameViewRenderMode.LeftEye;
        }
    }

    //PCディスプレイにThirdViewCamera映像を表示
    void OnThirdView()
    {
        _thirdViewCamera.enabled = true;

        if (GameManager.instance.VRMode)
        {
            XRSettings.gameViewRenderMode = GameViewRenderMode.None;
        }
    }
}
