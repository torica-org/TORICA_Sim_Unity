using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonoBehavior : MonoBehaviour
{
    void OnDestroy()
    {
        // このGameObjectを誰が破棄したかを表示
        Debug.LogError(
            $"[{name}] GameObject destroyed at frame {Time.frameCount}\n" +
            StackTraceUtility.ExtractStackTrace()
        );
    }
    /*
    private ManualXRControl manualXRControl = new(); // VR制御用クラスを保持するフィールド
    private bool isVRInitialized = false; // VRが初期化されたかどうかを示すフラグ

    void Update()
    {
        if (!isVRInitialized)
        {
            isVRInitialized = true;
            StartCoroutine(manualXRControl.StartXRCoroutine());
        }
    }
    void OnDestroy()
    {
        // オブジェクトが破棄される際にVRを停止する.
        if (isVRInitialized)
        {
            isVRInitialized = false;
            manualXRControl.StopXR();
        }
    }

    void OnApplicationQuit()
    {
        // アプリケーションが終了する際にVRを停止する.
        if (isVRInitialized)
        {
            isVRInitialized = false;
            manualXRControl.StopXR();
        }
    }
    */

    /*
    private float a = 1.0f;
    private object b = null;
    private float c = 0.0f;

    void Start()
    {
        Register(ref a);
        InvokeRepeating("Print", 0.0f, 1.0f);
    }

    private void Print()
    {
        Debug.Log("nullCheck: " + (b == null) + "\tb: "+ (float)b);
    }

    void Register(ref float x)
    {
        b = x;
    }

    void Update()
    {
        if (b != null)
        {
            if ((float)b != c)
            {
                Debug.Log("Value Change Detected!!!: " + c + " -> " + b);
                c = (float)b;
            }
        }
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 50), "changeVal"))
        {
            a += 1.0f;
            Debug.Log("a: " + a + "\tb: " + b);
        }
    }
    */
}
/*
public class ChangeValueDetector()
{
    private float preVal = 0.0f;
    private float ref refVal = 0.0f;
    private delegate void refFunc();
    private refFunc privateFunc;

    ChangeValueDetector(ref val, delegate void _Func())
    {
        refVal = val;
        privateFunc = _Func;
    }

    private void DetectProcess()
    {
        if (refVal != preVal)
        {
            privateFunc();
        }
    else

*/