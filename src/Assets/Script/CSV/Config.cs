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
    private static readonly string defaultAircraftName = "ARG-2";
    private static string aircraftName = defaultAircraftName;
    public static string AircraftName
    {
        get => aircraftName;
        
    }

    private static readonly bool defaultShowHUD = true;
    private static bool showHUD = defaultShowHUD;
    public static bool ShowHUD
    {
        get => showHUD;
        set => SetProperty(ref showHUD, value);
    }

    private static readonly bool defaultHorizontalLineActive = false;
    private static bool _HorizontalLineActive = defaultHorizontalLineActive;
    public static bool HorizontalLineActive
    {
        get => _HorizontalLineActive;
        set => SetProperty(ref _HorizontalLineActive, value);
    }

    private static readonly bool defaultIsMainDisplayTPS = true;
    private static bool _IsMainDisplayTPS = defaultIsMainDisplayTPS;
    public static bool IsMainDisplayTPS
    {
        get => _IsMainDisplayTPS;
        set => SetProperty(ref _IsMainDisplayTPS, value);
    }

    private static readonly bool defaultMousePitchControl = false;
    private static bool _MousePitchControl = defaultMousePitchControl;
    public static bool MousePitchControl
    {
        get => _MousePitchControl;
        set => SetProperty(ref _MousePitchControl, value);
    }

    private static readonly float defaultMouseSensitivity = 1.0f;
    private static float _MouseSensitivity = defaultMouseSensitivity;
    public static float MouseSensitivity
    {
        get => _MouseSensitivity;
        set => SetProperty(ref _MouseSensitivity, value);
    }

    private static readonly bool defaultExportLog = false;
    private static bool _ExportLog = defaultExportLog;
    public static bool ExportLog
    {
        get => _ExportLog;
        set => SetProperty(ref _ExportLog, value);
    }

    private static readonly bool defaultGustRandom = false;
    private static bool _GustRandom = defaultGustRandom;
    public static bool GustRandom
    {
        get => _GustRandom;
        set => SetProperty(ref _GustRandom, value);
    }

    private static readonly float defaultGustMagnitude = 0.0f;
    private static float _GustMagnitude = defaultGustMagnitude;
    public static float GustMagnitude
    {
        get => _GustMagnitude;
        set => SetProperty(ref _GustMagnitude, value);
    }

    private static readonly float defaultGustDirection = 0.0f;
    private static float _GustDirection = defaultGustDirection;
    public static float GustDirection
    {
        get => _GustDirection;
        set => SetProperty(ref _GustDirection, value);
    }

    private static readonly float defaultTakeoffSpeed = 6.5f;
    private static float _TakeoffSpeed = defaultTakeoffSpeed;
    public static float TakeoffSpeed
    {
        get => _TakeoffSpeed;
        set => SetProperty(ref _TakeoffSpeed, value);
    }

    private static readonly float defaultTakeoffRoll = 0.0f;
    private static float _TakeoffRoll = defaultTakeoffRoll;
    public static float TakeoffRoll
    {
        get => _TakeoffRoll;
        set => SetProperty(ref _TakeoffRoll, value);
    }

    private static readonly float defaultTakeoffPitch = 0.0f;
    private static float _TakeoffPitch = defaultTakeoffPitch;
    public static float TakeoffPitch
    {
        get => _TakeoffPitch;
        set => SetProperty(ref _TakeoffPitch, value);
    }

    private static readonly float defaultTakeoffYaw = 0.0f;
    private static float _TakeoffYaw = defaultTakeoffYaw;
    public static float TakeoffYaw
    {
        get => _TakeoffYaw;
        set => SetProperty(ref _TakeoffYaw, value);
    }

    private static void SetDefault()
    {
        aircraftName = defaultAircraftName;
        showHUD = defaultShowHUD;
        _HorizontalLineActive = defaultHorizontalLineActive;
        _IsMainDisplayTPS = defaultIsMainDisplayTPS;
        _MousePitchControl = defaultMousePitchControl;
        _MouseSensitivity = defaultMouseSensitivity;
        _ExportLog = defaultExportLog;
        _GustRandom = defaultGustRandom;
        _GustMagnitude = defaultGustMagnitude;
        _GustDirection = defaultGustDirection;
        _TakeoffSpeed = defaultTakeoffSpeed;
        _TakeoffRoll = defaultTakeoffRoll;
        _TakeoffPitch = defaultTakeoffPitch;
        _TakeoffYaw = defaultTakeoffYaw;
    }

    private static void Validate()
    {
        // ----- AircraftName -----------------------------------------------------------------------------
        int indexOfAircraft = AircraftData.Names.FindIndex(name => string.Compare(name, aircraftName, ignoreCase: true) == 0);
        if (indexOfAircraft == -1)
        {
            aircraftName = defaultAircraftName;
        }
        aircraftName = AircraftData.Names[indexOfAircraft]; // 大文字小文字を無視してCSVファイルの機体名と一致するものを探し、見つかったものに置き換える.
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
    private static readonly int recordNum = 50; // インスタンスで扱える行数を指定.
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
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); // シーンを再ロード.
            },
            null
        );
    }

    // ===== 設定を`Config.txt`から読み込む ================================================
    private static void Load()
    {
        SetDefault(); // デフォルト値にリセット.

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

            aircraftName = CheckContent<string>("AircraftName");
            showHUD = CheckContent<bool>("ShowHUD");
            _HorizontalLineActive = CheckContent<bool>("HorizontalLineActive");
            _IsMainDisplayTPS = CheckContent<bool>("IsMainDisplayTPS");
            _MousePitchControl = CheckContent<bool>("MousePitchControl");
            _MouseSensitivity = CheckContent<float>("MouseSensitivity");
            _ExportLog = CheckContent<bool>("ExportLog");
            _GustRandom = CheckContent<bool>("GustRandom");
            _GustMagnitude = CheckContent<float>("GustMagnitude");
            _GustDirection = CheckContent<float>("GustDirection");
            _TakeoffSpeed = CheckContent<float>("TakeoffSpeed");
            _TakeoffRoll = CheckContent<float>("TakeoffRoll");
            _TakeoffPitch = CheckContent<float>("TakeoffPitch");
            _TakeoffYaw = CheckContent<float>("TakeoffYaw");

            Validate(); // 読み込んだ値の妥当性を検査して、必要に応じて修正.
        }
    }

    // ===== 内容を確認し，存在すれば値を返す =====
    private static T CheckContent<T>(string key) // 全ての型に対応するジェネリック型
    {
        for (int i = 1; i <= recordNum; i++) // 全ての行で繰り返し.
        {
            if (string.Compare(config.Read(i, 1), key, ignoreCase: true) == 0) // 大文字小文字を無視して該当行の文字列とkeyを比較 -> 等しいなら.
            {
                // Debug.Log("Content exsists: " + key);
                string valueString = config.Read(i, 2); // 該当行の`=`右側にある値を読み込む.

                try
                {
                    if (typeof(T) == typeof(Enum)) // valueの型がEnumなら
                    {
                        return (T)Enum.Parse(typeof(T), valueString, true);
                    }
                    else // valueの型がEnum以外なら
                    {
                        return (T)Convert.ChangeType(valueString, typeof(T));
                    }
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"Failed convert config string: {e.Message}");
                }
            }
        }
        return default; // 見つからなかった場合は型のデフォルト値を返す.
    }

    // ===== `Config.txt`の内容を生成して書き込む ====================================
    private static bool Flush()
    {
        config.Clear();
        config.Write(1, 1, "----- Configurations -----");
        int linenumber = 3; // 2行目は空行，3行目から書き込み

        Action newLine = () => linenumber++; // Configの行を1つ進めるデリゲート

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

        addString("使用する機体諸元（以下に利用可能な機体が列挙されます）");
        addString("--------------------------------------------------------");
        string availableAircrafts = "";
        for (int i = 0; i < AircraftData.Names.Count; i++)
        {
            availableAircrafts += AircraftData.Names[i] + ", ";
            if (i % 5 == 0 && i != 0) // 5機体ごとに改行を入れる.
            {
                availableAircrafts += Environment.NewLine;
                addString(availableAircrafts);
                availableAircrafts = "";
            }
        }
        addString("--------------------------------------------------------");
        addConfig("AircraftName", AircraftName);
        newLine();

        addString("飛行中の画面の周囲に情報を表示する(True/False)");
        addConfig("ShowHUD", ShowHUD.ToString());
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

        addString($"マウス感度を設定する(初期値: {defaultMouseSensitivity:0.0})");
        addConfig("MouseSensitivity", MouseSensitivity.ToString("0.0"));
        newLine();

        addString("フライトログの出力を有効化する(True/False)");
        addConfig("ExportLog", ExportLog.ToString());
        newLine();

        addString("ランダム風モードを有効化する(True/False)");
        addConfig("GustRandom", GustRandom.ToString());
        newLine();

        addString($"風速[m/s](初期値: {defaultGustMagnitude:0.0})");
        addConfig("GustMagnitude", GustMagnitude.ToString("0.0"));
        newLine();

        addString($"風上[deg](初期値: {defaultGustDirection:0.0} / 範囲: [L]-180.0 ↔ [R]180.0)");
        addConfig("GustDirection", GustDirection.ToString("0.0"));
        newLine();

        addString($"離陸時のスピード[m/s](初期値: {defaultTakeoffSpeed:0.0})");
        addConfig("TakeoffSpeed", TakeoffSpeed.ToString("0.0"));
        newLine();

        addString($"離陸時のロール[deg](初期値: {defaultTakeoffRoll:0.0})");
        addConfig("TakeoffRoll", TakeoffRoll.ToString("0.0"));
        newLine();

        addString($"離陸時のピッチ[deg](初期値: {defaultTakeoffPitch:0.0})");
        addConfig("TakeoffPitch", TakeoffPitch.ToString("0.0"));
        newLine();

        addString($"離陸時のヨー[deg](初期値: {defaultTakeoffYaw:0.0})");
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
