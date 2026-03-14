using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Threading;
using System.Timers;

public class UIBase : IDisposable
{
    // メインスレッドのコンテキストを保存するためのフィールド. これにより，UIの更新がメインスレッドで行われることを保証できる.
    protected static SynchronizationContext _mainThreadContext = SynchronizationContext.Current;

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

    public void Dispose()
    {
        eventHandler = null;
    }

    // UIBaseを継承する全てのClassで使用可能なプロパティ.
    public GameObject gameObject { get; set; }
    public RectTransform rectTransform { get; set; }
}
