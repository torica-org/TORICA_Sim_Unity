using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.UI;
/*
public sealed class ValueWatcher<T> : IDisposable
{
    private readonly Func<T> _get;
    private readonly System.Timers.Timer _timer;
    private T _last;

    public event Action<T>? Changed;

    public ValueWatcher(Func<T> getter, double intervalMs = 50)
    {
        _get = getter ?? throw new ArgumentNullException(nameof(getter));
        _last = _get();
        _timer = new System.Timers.Timer(intervalMs) { AutoReset = true, Enabled = true };
        _timer.Elapsed += (s, e) =>
        {
            var cur = _get();
            if (!EqualityComparer<T>.Default.Equals(cur, _last))
            {
                _last = cur;
                Changed?.Invoke(cur);
            }
        };
    }

    public void Dispose() => _timer.Dispose();
}
*/

public sealed class NewSlider : IDisposable
{
    private GameObject _obj;
    private Slider _slider;

    private Action<float> _setter;
    private Func<float> _getter;

    public Action<float> OnValueChange;
    private float _last;

    private readonly System.Timers.Timer _timer;
    private double intervalMs = 100;

    public NewSlider(GameObject parent, string objectName, Action<float> setter, Func<float> getter, float minVal, float maxVal)
    {
        _obj = UIHelper.ProcessSlider(parent, objectName, setter, getter, minVal, maxVal); // スライダーを生成し、生成されたオブジェクトを保持.
        _slider = _obj.GetComponent<Slider>(); // スライダーコンポーネントを取得.

        _getter = getter ?? throw new ArgumentNullException(nameof(getter));
        _last = _getter();

        OnValueChange = _cur => { _slider.value = _cur; };

        _timer = new System.Timers.Timer(intervalMs) { AutoReset = true, Enabled = true }; // `Timer`を初期化し、指定した間隔で自動的にイベントを発生させるように設定.

        _timer.Elapsed += (s, e) => // タイマーイベントが発生したときの処理.
        {
            float cur = _getter();
            if (cur != _last)
            {
                _last = cur;
                OnValueChange?.Invoke(cur);
            }
            else if (_slider.value != _last) // スライダーの値と現在の値が異なる場合、スライダーの値を更新.
            {
                _slider.value = cur;
            }
        };

    }


    public RectTransform GetRectTransform()
    {
        return _obj.GetComponent<RectTransform>();
    }


    public void Dispose()
    {
        _timer.Dispose();
    }
}
