using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Config
{
    // 設定値の内部変数とアクセサ.
    private static bool _HUDActive = true;
    public static bool HUDActive
    {
      get
      {
        return _HUDActive;
      }
      set
      {
        _HUDActive = value;
        WriteConfig();
      }
    }
    
    private static bool _HorizontalLineActive = false;
    public static bool HorizontalLineActive
    {
      get
      {
        return _HorizontalLineActive;
      }
      set
      {
        _HorizontalLineActive = value;
        WriteConfig();
      }
    }

    private static bool _IsMainDisplayTPS = true;
    public static bool IsMainDisplayTPS
    {
      get
      {
        return _IsMainDisplayTPS;
      }
      set
      {
        _IsMainDisplayTPS = value;
        WriteConfig();
      }
    }

    private static bool _MousePitchControl = false;
    public static bool MousePitchControl
    {
      get
      {
        return _MousePitchControl;
      }
      set
      {
        _MousePitchControl = value;
        WriteConfig();
      }
    }


    private static bool _ExportLog = false;
    public static bool ExportLog
    {
      get
      {
        return _ExportLog;
      }
      set
      {
        _ExportLog = value;
        WriteConfig();
      }
    }

    private static float _MouseSensitivity = 1.0f;
    public static float MouseSensitivity
    {
      get
      {
        return _MouseSensitivity;
      }
      set
      {
        _MouseSensitivity = value;
        WriteConfig();
      }
    }

    private static bool _RandomGust = false;
    public static bool RandomGust
    {
      get
      {
        return _RandomGust;
      }
      set
      {
        _RandomGust = value;
        WriteConfig();
      }
    }

    private static float _MagnitudeOfGust = 0.0f;
    public static float MagnitudeOfGust
    {
      get
      {
        return _MagnitudeOfGust;
      }
      set
      {
        _MagnitudeOfGust = value;
        WriteConfig();
      }
    }

    private static float _DirectionOfGust = 0.0f;
    public static float DirectionOfGust
    {
      get
      {
        return _DirectionOfGust;
      }
      set
      {
        _DirectionOfGust = value;
        WriteConfig();
      }
    }

    private static float _TakeoffSpeed = 5.8f;
    public static float TakeoffSpeed
    {
      get
      {
        return _TakeoffSpeed;
      }
      set
      {
        _TakeoffSpeed = value;
        WriteConfig();
      }
    }

    private static float _TakeoffPitch = 0.0f;
    public static float TakeoffPitch
    {
      get
      {
        return _TakeoffPitch;
      }
      set
      {
        _TakeoffPitch = value;
        WriteConfig();
      }
    }

    private static float _TakeoffYaw = 0.0f;
    public static float TakeoffYaw
    {
      get
      {
        return _TakeoffYaw;
      }
      set
      {
        _TakeoffYaw = value;
        WriteConfig();
      }
    }

    // `config.txt`を保存する`Application.dataPath`へのフルパス.
    private static string _sourceDirectory = Directory.GetParent(Application.dataPath).FullName;

    // `config.txt`のフルパスを構築.
    private static string _configFilePath = Path.Combine(_sourceDirectory, "config.txt");

    // ファイルシステムの変更を監視する`FileSystemWatcher`.
    private static FileSystemWatcher watcher;

    // `CsvIO`クラスのインスタンス. 30行2列のファイルを"="区切りで扱うように初期化.
    private static readonly CsvIO csv = new(30, 2, "=");

    // ===== ゲームのシーンがロードされる前に一度だけ呼び出される初期化メソッド. =========================
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] // ゲームのシーンがロードされる前にこのメソッドを呼び出すための属性.
    private static void Initialize()
    {
        // `config.txt`が存在しない場合は、デフォルトの内容で新しいファイルを作成.
        if (!File.Exists(_configFilePath))
        {
            File.WriteAllText(_configFilePath, "key=value");
            Debug.Log("config.txt created with default content.");
        }
        csv.Load(_configFilePath); // `config.txt`の内容を`CsvIO`クラスにロード.
        ReadConfig();

        // `FileSystemWatcher`を初期化して、`config.txt`の変更を監視.
        watcher = new FileSystemWatcher(_sourceDirectory, "config.txt");
        // ファイルが変更されたときのイベントハンドラーを登録.
        watcher.Changed += (sender, e) => ReadConfig();
        watcher.Created += (sender, e) => ReadConfig();
        watcher.Deleted += (sender, e) => ReadConfig();
        watcher.Renamed += (sender, e) => ReadConfig();
        watcher.Error += (sender, e) => Debug.LogError($"FileSystemWatcher error: {e.GetException()}");
        // 監視を開始.
        watcher.EnableRaisingEvents = true;
        Debug.Log("ConfigManager initialized and watching for changes in config.txt.");
    }

    // ===== 設定を`config.txt`から読み出す ===================================================
    private static void ReadConfig()
    {
        try
        {
            csv.Load(_configFilePath); // 変更されたファイルの内容を再読み込み.
            Debug.Log("Config file reloaded successfully.");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to reload config file: {ex.Message}");
        }
        for (int i = 1; i <= 30; i++)
        {
            Debug.Log($"{csv.Read(i, 1)} = {csv.Read(i, 2)}"); // `CsvIO`クラスの内容をログに出力. 
        }
    }

    // ===== 設定を`config.txt`に書き込む ===================================================
    private static void WriteConfig()
    {
        csv.Flush(_configFilePath);
    }
}
