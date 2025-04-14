using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

//Titleシーン用のUI関連機能付きSerialHandler
public class SerialHandler2 : SerialHandler
{
    public Text text;//通信状況を伝えるテキスト
    public InputField inputField;

    void Start()
    {
        Debug.Log("AE");
        inputField.text = portName;
    }

    public void SetPort()
    {
        Close();
        portName=inputField.text;
        text.text = "再設定中";
        Open();

        
        //SceneManager.LoadScene("FlightScene");
    }
    
    protected override void Open()
    {
        try{
            serialPort_ = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
            serialPort_.DtrEnable= true;

            serialPort_.ReadTimeout = 1000;
            //serialPort_.WriteTimeout = 100;
            //または
            //serialPort_ = new SerialPort(portName, baudRate);

            serialPort_.Open();

            isRunning_ = true;

            thread_ = new Thread(Read);
            thread_.Start();
        }
        catch(System.Exception e)
        {
            Debug.LogWarning(e.Message);
            Debug.LogWarning("フレーム使用不可能");
            text.text = "再設定中";
        }
    }
}
