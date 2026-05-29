using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Config
{
    private static bool SetProperty<T>(ref T backingField, T value)
    {
        // 値が同じなら false を返して終了
        if (EqualityComparer<T>.Default.Equals(backingField, value))
            return false;

        // 値を更新
        backingField = value;

        // クラス内の共通処理メソッドを呼ぶ
        Flush();

        // 値が更新されたので true を返す
        return true;
    }

    // 設定値の内部変数とアクセサ.
    private static bool _HUDActive = true;
    public static bool HUDActive
    {
        get => _HUDActive;
        set => SetProperty(ref _HUDActive, value);
    }

    private static bool _HorizontalLineActive = false;
    public static bool HorizontalLineActive
    {
        get => _HorizontalLineActive;
        set => SetProperty(ref _HorizontalLineActive, value);
    }

    private static bool _IsMainDisplayTPS = true;
    public static bool IsMainDisplayTPS
    {
        get => _IsMainDisplayTPS;
        set => SetProperty(ref _IsMainDisplayTPS, value);
    }

    private static bool _MousePitchControl = false;
    public static bool MousePitchControl
    {
        get => _MousePitchControl;
        set => SetProperty(ref _MousePitchControl, value);
    }

    private static float _MouseSensitivity = 1.0f;
    public static float MouseSensitivity
    {
        get => _MouseSensitivity;
        set => SetProperty(ref _MouseSensitivity, value);
    }

    private static bool _ExportLog = false;
    public static bool ExportLog
    {
        get => _ExportLog;
        set => SetProperty(ref _ExportLog, value);
    }

    private static bool _GustRandom = false;
    public static bool GustRandom
    {
        get => _GustRandom;
        set => SetProperty(ref _GustRandom, value);
    }

    private static float _GustMagnitude = 0.0f;
    public static float GustMagnitude
    {
        get => _GustMagnitude;
        set => SetProperty(ref _GustMagnitude, value);
    }

    private static float _GustDirection = 0.0f;
    public static float GustDirection
    {
        get => _GustDirection;
        set => SetProperty(ref _GustDirection, value);
    }

    private static float _TakeoffSpeed = 5.8f;
    public static float TakeoffSpeed
    {
        get => _TakeoffSpeed;
        set => SetProperty(ref _TakeoffSpeed, value);
    }

    private static float _TakeoffRoll = 0.0f;
    public static float TakeoffRoll
    {
        get => _TakeoffRoll;
        set => SetProperty(ref _TakeoffRoll, value);
    }

    private static float _TakeoffPitch = 0.0f;
    public static float TakeoffPitch
    {
        get => _TakeoffPitch;
        set => SetProperty(ref _TakeoffPitch, value);
    }

    private static float _TakeoffYaw = 0.0f;
    public static float TakeoffYaw
    {
        get => _TakeoffYaw;
        set => SetProperty(ref _TakeoffYaw, value);
    }

    private static void SetDefault()
    {
        _HUDActive = true;
        _HorizontalLineActive = false;
        _IsMainDisplayTPS = true;
        _MousePitchControl = false;
        _MouseSensitivity = 1.0f;
        _ExportLog = false;
        _GustRandom = false;
        _GustMagnitude = 0.0f;
        _GustDirection = 0.0f;
        _TakeoffSpeed = 5.8f;
        _TakeoffRoll = 0.0f;
        _TakeoffPitch = 0.0f;
        _TakeoffYaw = 0.0f;
    }

    // `Config.txt`を保存する`Application.dataPath`へのフルパス.
    private static string _sourceDirectory = Directory.GetParent(Application.dataPath).FullName;

    // `Config.txt`の名前.
    private static string _fileName = "Config.txt";

    // `Config.txt`のフルパスを構築.
    private static string _configFilePath = Path.Combine(_sourceDirectory, _fileName);

    // ファイルシステムの変更を監視する`FileSystemWatcher`.
    private static FileSystemWatcher watcher;

    // 最後に同期した時間を記録する変数（クラスのメンバとして定義）
    private static DateTime _lastSyncTime = DateTime.MinValue;

    // `ConfigIO`クラスのインスタンス.
    private static readonly int recordNum = 40; // インスタンスで扱える行数を指定.
    private static readonly ConfigIO config = new(recordNum);

    private static SynchronizationContext context = SynchronizationContext.Current;

    // ===== ゲームのシーンがロードされる前に一度だけ呼び出される初期化メソッド. =========================
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] // ゲームのシーンがロードされる前にこのメソッドを呼び出すための属性.
    private static void Initialize()
    {
        Sync();

        // `FileSystemWatcher`を初期化して、`Config.txt`の変更を監視.
        watcher = new FileSystemWatcher(_sourceDirectory, _fileName);
        // ファイルが変更されたときのイベントハンドラーを登録.
        watcher.Changed += (sender, e) =>
        {
            // 前回の同期から 1000ミリ秒（1秒）以内の連続呼び出しは無視する
            if ((DateTime.Now - _lastSyncTime).TotalMilliseconds < 1000)
                return;
            _lastSyncTime = DateTime.Now; // 最後に検知した時間を「今」に更新
            Sync(); // 同期を実行
        };
        watcher.Created += (sender, e) => Sync();
        watcher.Deleted += (sender, e) => Sync();
        watcher.Renamed += (sender, e) => Sync();
        watcher.Error += (sender, e) =>
            Debug.LogError($"FileSystemWatcher error: {e.GetException()}");
        // 監視を開始.
        watcher.EnableRaisingEvents = true;
        Debug.Log("ConfigManager initialized and watching for changes in Config.txt.");
    }

    // ===== 設定を`Config.txt`と同期する ===================================================
    private static void Sync()
    {
        Load();
        Flush();

        // メインスレッドの文脈に処理を非同期的に戻す.
        context.Post(
            (_) =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            },
            null
        );
    }

    // ===== 設定を`Config.txt`から読み込む ================================================
    private static void Load()
    {
        if (File.Exists(_configFilePath)) // `Config.txt`が存在する場合.
        {
            try
            {
                config.Load(_configFilePath); // 変更されたファイルの内容を再読み込み.
                Debug.Log("Config file reloaded. Check start.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to reload config file: {ex.Message}");
            }

            CheckContent("HUDActive", ref _HUDActive);
            CheckContent("HorizontalLineActive", ref _HorizontalLineActive);
            CheckContent("IsMainDisplayTPS", ref _IsMainDisplayTPS);
            CheckContent("MousePitchControl", ref _MousePitchControl);
            CheckContent("MouseSensitivity", ref _MouseSensitivity);
            CheckContent("ExportLog", ref _ExportLog);
            CheckContent("GustRandom", ref _GustRandom);
            CheckContent("GustMagnitude", ref _GustMagnitude);
            CheckContent("GustDirection", ref _GustDirection);
            CheckContent("TakeoffSpeed", ref _TakeoffSpeed);
            CheckContent("TakeoffRoll", ref _TakeoffRoll);
            CheckContent("TakeoffPitch", ref _TakeoffPitch);
            CheckContent("TakeoffYaw", ref _TakeoffYaw);
        }
        else
        {
            SetDefault();
        }
    }

    // ===== 内容を確認し，存在すれば変数を更新 =====
    private static bool CheckContent<T>(string key, ref T value)
    {
        for (int i = 1; i <= recordNum; i++)
        {
            if (string.Compare(config.Read(i, 1), key, ignoreCase: true) == 0)
            {
                // Debug.Log("Content exsists: " + key);
                string valueString = config.Read(i, 2);

                if (typeof(T) == typeof(Enum))
                {
                    value = (T)Enum.Parse(typeof(T), valueString, true);
                }
                else
                {
                    value = (T)Convert.ChangeType(valueString, typeof(T));
                }
                return true;
            }
        }
        return false;
    }

    // ===== `Config.txt`の内容を生成して書き込む ====================================
    private static bool Flush()
    {
        config.Clear();
        config.Write(1, 1, "----- Configurations -----");
        int linenumber = 3; // 2行目は空行，3行目から書き込み

        Action newLine = () => linenumber++; // Configの行を勧めるデリゲート

        Action<string, string> addConfig = (key, value) => // Configに設定を追加するデリゲート
        {
            config.Write(linenumber, 1, key);
            config.Write(linenumber, 2, value);
            newLine();
        };

        Action<string> addString = (str) => // Configに文字列を追加するデリゲート
        {
            config.Write(linenumber, 1, "// " + str);
            newLine();
        };

        addString("飛行中の画面の周囲に情報を表示する(True/False)");
        addConfig("HUDActive", HUDActive.ToString());
        newLine();

        addString("水平線を表示する(True/False)");
        addConfig("HorizontalLineActive", HorizontalLineActive.ToString());
        newLine();

        addString("メインディスプレイを三人称視点にする(True/False)");
        addConfig("IsMainDisplayTPS", IsMainDisplayTPS.ToString());
        newLine();

        addString("マウスによる重心移動を有効化する(True/False)");
        addConfig("MousePitchControl", MousePitchControl.ToString());
        newLine();

        addString("マウス感度を設定する(初期値: 1.0)");
        addConfig("MouseSensitivity", MouseSensitivity.ToString("0.0"));
        newLine();

        addString("フライトログの出力を有効化する(True/False)");
        addConfig("ExportLog", ExportLog.ToString());
        newLine();

        addString("ランダム風モードを有効化する(True/False)");
        addConfig("GustRandom", GustRandom.ToString());
        newLine();

        addString("風速[m/s](初期値: 0.0)");
        addConfig("GustMagnitude", GustMagnitude.ToString("0.0"));
        newLine();

        addString("風上[deg](初期値: 0.0 / 範囲: [L]-180.0 ↔ [R]180.0)");
        addConfig("GustDirection", GustDirection.ToString("0.0"));
        newLine();

        addString("離陸時のスピード[m/s](初期値: 5.8)");
        addConfig("TakeoffSpeed", TakeoffSpeed.ToString("0.0"));
        newLine();

        addString("離陸時のロール[deg](初期値: 0.0)");
        addConfig("TakeoffRoll", TakeoffRoll.ToString("0.0"));
        newLine();

        addString("離陸時のピッチ[deg](初期値: 0.0)");
        addConfig("TakeoffPitch", TakeoffPitch.ToString("0.0"));
        newLine();

        addString("離陸時のヨー[deg](初期値: 0.0)");
        addConfig("TakeoffYaw", TakeoffYaw.ToString("0.0"));

        try
        {
            config.Flush(_configFilePath); // 変更されたファイルの内容を再度書き込み.
            Debug.Log("Config file synced.");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to sync config file: {ex.Message}");
            return false;
        }

        return true;
    }
}
