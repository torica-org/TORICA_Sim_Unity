using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Threading;
using System.Timers;

// ===== 概要 =========================
// DynamicTextやDynamicSliderなどのUI要素の基底クラス.

public abstract class UIBase : IDisposable // 抽象クラス. 直接インスタンス化できない. DynamicTextやDynamicSliderなどのUI要素の基底クラスとして機能する.
{
    protected static List<UIBase> _instances = new List<UIBase>(); // UIBaseを継承するクラスのインスタンスを管理するためのリスト.

    // メインスレッドのコンテキスト（文脈）を保存するためのフィールド. これにより，UIの更新がメインスレッドで行われることを保証できる.
    protected static SynchronizationContext _mainThreadContext = SynchronizationContext.Current; // 厳密には，初回ロード時のスレッドの文脈を保存.

    // デリゲートの定義.
    public delegate void Setter<T>(T value); // ジェネリックな`Setter`型の定義.
    public delegate T Getter<T>(); // ジェネリックな`Getter`型の定義.
    public delegate void EventHandler(); // イベント発生時の処理を溜め込むデリゲートの定義.

    protected static EventHandler eventHandler; // イベントハンドラーのフィールド.

    // `Timer`を初期化し，指定した間隔で自動的にイベントを発生させるように設定.
    private static double intervalMs = 50;
    private static System.Timers.Timer _timer = new System.Timers.Timer(intervalMs) { AutoReset = true, Enabled = true };
    private static bool isInitialized = false;

    // `Instantiate`するためのプレハブをロードするためのフィールド. これらは`UIBase`のコンストラクタで一度だけロードされる.
    protected static GameObject DefaultText;
    protected static GameObject DefaultSlider;
    protected static GameObject DefaultButton;
    protected static GameObject DefaultDropdown;
    protected static GameObject DefaultInputField;
    protected static GameObject AirdataChart;

    // ===== ゲームのシーンがロードされる前に一度だけ呼び出される初期化メソッド. =========================
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] // ゲームのシーンがロードされる前にこのメソッドを呼び出すための属性.
    private static void Initializer()
    {
        if (!isInitialized)
        {
            isInitialized = true;
            _timer.Elapsed += (s, e) => eventHandler?.Invoke(); // タイマーイベントが発生したときにイベントハンドラーを呼び出す.

            DefaultText = (GameObject)Resources.Load("UIParts/DefaultText");
            DefaultSlider = (GameObject)Resources.Load("UIParts/DefaultSlider");
            DefaultButton = (GameObject)Resources.Load("UIParts/DefaultButton");
            DefaultDropdown = (GameObject)Resources.Load("UIParts/DefaultDropdown");
            DefaultInputField = (GameObject)Resources.Load("UIParts/DefaultInputField");
            AirdataChart = (GameObject)Resources.Load("UIParts/AirdataChart");

            /*
            Debug.Log("Initialized UIBase. Loaded prefabs: " +
                "\nDefaultButton: " + (DefaultButton != null) +
                "\nDefaultDropdown: " + (DefaultDropdown != null) +
                "\nDefaultSlider: " + (DefaultSlider != null) +
                "\nDefaultText: " + (DefaultText != null) +
                "\nAirdataChart: " + (AirdataChart != null));
            */
        }
    }

    // ===== 継承先のクラスでオーバーライドするための仮想関数. ==========================
    public virtual void Dispose() { }

    // ===== UIBaseを継承する全てのインスタンスを破棄するための静的メソッド. =========================
    public static void DisposeAll()
    {
        foreach (var instance in _instances)
        {
            // Debug.Log("Disposed instance: " + instance);
            instance.Dispose();
        }
        _instances.Clear();
    }

    // ===== ジェネリックなフォーマッタ. 数値型の場合は小数点以下3桁まで表示し，それ以外の型の場合は通常の`ToString()`を使用. =====
    protected static string Formatter<T>(T value)
    {
        // Debug.Log("typeof(T): " + typeof(T));
        if (typeof(T) == typeof(float) || typeof(T) == typeof(int))
            return ((float)(object)value).ToString("0.000", System.Globalization.CultureInfo.InvariantCulture);
        return value?.ToString() ?? string.Empty;
    }

    // UIBaseを継承する全てのClassで使用可能なプロパティ.
    public GameObject gameObject { get; set; }
    public RectTransform rectTransform { get; set; }
}