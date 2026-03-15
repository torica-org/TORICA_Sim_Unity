using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Threading;
using System.Timers;

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
    private static bool isTimerRegistered = false;


    protected UIBase()
    {
        if (!isTimerRegistered)
        {
            isTimerRegistered = true;
            _timer.Elapsed += (s, e) => eventHandler?.Invoke(); // タイマーイベントが発生したときにイベントハンドラーを呼び出す
        }
    }


    public virtual void Dispose() { }


    public static void DisposeAll()
    {
        // UIBaseを継承する全てのインスタンスを破棄するための静的メソッド.
        foreach (var instance in _instances)
        {
            // Debug.Log("Disposed instance: " + instance);
            instance.Dispose();
        }
        _instances.Clear();
    }


    // UIBaseを継承する全てのClassで使用可能なプロパティ.
    public GameObject gameObject { get; set; }
    public RectTransform rectTransform { get; set; }
}
