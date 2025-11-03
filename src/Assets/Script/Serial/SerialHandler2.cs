/*
using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

//Titleシーン用のUI関連機能付きSerialHandler
public class SerialHandler2 : MonoBehaviour
{
    public Text text;//通信状況を伝えるテキスト
    public InputField inputField;//ポート番号入力フィールド
    private bool Connection;
    private bool refresh;
    private bool frameError;

    void Start()
    {
        inputField.text = portName;
        Connection = MyGameManeger.instance.FrameUseable;
    }

    protected override void Update()
    {
        if (isNewMessageReceived_) {
            OnDataReceived(message_);
        }
        isNewMessageReceived_ = false;
        
    }

    protected override void Read()
    {
    }
    
    protected override void Open()
    {

    }
}
*/