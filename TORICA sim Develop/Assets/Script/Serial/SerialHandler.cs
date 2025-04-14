using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;
using UnityEngine.UI;

public class SerialHandler : MonoBehaviour
{
    public delegate void SerialDataReceivedEventHandler(string message);
    public event SerialDataReceivedEventHandler OnDataReceived;

    //ポート名
    //例
    //Linuxでは/dev/ttyUSB0
    //windowsではCOM1
    //Macでは/dev/tty.usbmodem1421など
    public static string portName = "COM3";
    public int baudRate    = 115200;

    protected SerialPort serialPort_;
    protected Thread thread_;
    protected bool isRunning_ = false;

    protected string message_;
    protected bool isNewMessageReceived_ = false;

    void Awake()
    {
        Open();
    }

    void Update()
    {
        if (isNewMessageReceived_) {
            OnDataReceived(message_);
        }
        isNewMessageReceived_ = false;
    }

    void OnDestroy()
    {
        Close();
    }

    protected virtual void Open()
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
            //text.text = "フレームと接続できませんでした。フレームを使用しない操作に切り替えます。";
        }
    }

    protected void Close()
    {
        isNewMessageReceived_ = false;
        isRunning_ = false;

        if (thread_ != null && thread_.IsAlive) {
            thread_.Join();
        }

        if (serialPort_ != null && serialPort_.IsOpen) {
            serialPort_.Close();
            serialPort_.Dispose();
        }
    }

    protected void Read()
    {
        while (isRunning_ && serialPort_ != null && serialPort_.IsOpen) {
            try {
                //message_ = serialPort_.ReadExisting();
                message_ = serialPort_.ReadLine();
                isNewMessageReceived_ = true;
                //Debug.Log(message_);
            } catch (System.Exception e) {
                Debug.LogWarning(e.Message);
            }
        }
    }

    public void Write(string message)
    {
        try {
            serialPort_.Write(message);
        } catch (System.Exception e) {
            Debug.LogWarning(e.Message);
        }
    }
}
