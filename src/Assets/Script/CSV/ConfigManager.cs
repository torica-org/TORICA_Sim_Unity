using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ConfigManager
{
    // `config.txt`を保存する`Application.dataPath`へのフルパス.
    private static string _sourceDirectory = Directory.GetParent(Application.dataPath).FullName;

    // ファイルシステムの変更を監視する`FileSystemWatcher`.
    private static FileSystemWatcher watcher;

    // `CsvIO`クラスのインスタンス. 30行2列のファイルを"="区切りで扱うように初期化.
    private static CsvIO csv = new(30, 2, "=");

    // ===== ゲームのシーンがロードされる前に一度だけ呼び出される初期化メソッド. =========================
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] // ゲームのシーンがロードされる前にこのメソッドを呼び出すための属性.
    private static void Initialize()
    {
        // `config.txt`のフルパスを構築.
        string configFilePath = Path.Combine(_sourceDirectory, "config.txt");
        // `config.txt`が存在しない場合は、デフォルトの内容で新しいファイルを作成.
        if (!File.Exists(configFilePath))
        {
            File.WriteAllText(configFilePath, "key=value");
            Debug.Log("config.txt created with default content.");
        }
        // `FileSystemWatcher`を初期化して、`config.txt`の変更を監視.
        watcher = new FileSystemWatcher(_sourceDirectory, "config.txt");
        // ファイルが変更されたときのイベントハンドラーを登録.
        watcher.Changed += OnConfigFileChanged;
        watcher.Created += OnConfigFileChanged;
        watcher.Deleted += OnConfigFileChanged;
        watcher.Renamed += OnConfigFileChanged;
        watcher.Error += (sender, e) => Debug.LogError($"FileSystemWatcher error: {e.GetException()}"); // エラーが発生したときのイベントハンドラーを登録.
        // 監視を開始.
        watcher.EnableRaisingEvents = true;
        Debug.Log("ConfigManager initialized and watching for changes in config.txt.");

        csv.Load(configFilePath); // `config.txt`の内容を`CsvIO`クラスにロード. ここでは、ファイルの内容を読み込むためのメソッドを呼び出しています.
    }

    private static void OnConfigFileChanged(object sender, FileSystemEventArgs e)
    {
        // `config.txt`が変更されたときの処理. ここでは、変更されたファイルのパスをログに出力.
        Debug.Log($"Config file changed: {e.FullPath}");
        try
        {
            csv.Load(e.FullPath); // 変更されたファイルの内容を再読み込み.
            Debug.Log("Config file reloaded successfully.");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to reload config file: {ex.Message}");
        }
        for (int i = 1; i <= 30; i++)
        {
            Debug.Log($"{csv.Read(i, 1)} = {csv.Read(i, 2)}"); // `CsvIO`クラスの内容をログに出力. ここでは、ファイルの内容を確認するための処理を行っています.
        }
    }
}
