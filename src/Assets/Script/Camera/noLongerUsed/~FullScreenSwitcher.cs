using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // 新しいインプットシステム

public class FullScreenSwitcher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.f11Key.wasPressedThisFrame)
        {
            // 現在の状態を反転させる（TrueならFalse、FalseならTrueへ）
            Screen.fullScreen = !Screen.fullScreen;
            Debug.Log("フルスクリーン切り替え: " + Screen.fullScreen);
        }
    }
}
