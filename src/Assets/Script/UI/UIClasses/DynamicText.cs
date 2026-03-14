using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Threading;
using System.Timers;
using UnityEngine.UI;
using TMPro;


// ===== 動的テキストを生成するクラス ===================================
public sealed class DynamicText<T> : UIBase
{
    private TextMeshProUGUI _tmp;
    private Getter<T> _getter;
    private T _last;


    public DynamicText(GameObject parent, string objectName, Getter<T> getter, float fontSize) : base() // `base()`を呼び出して`stdUI`のコンストラクタも実行.
    {
        gameObject = UIHelper.NewTextObj(parent, objectName, fontSize); // 動的テキストを生成し，生成されたオブジェクトを保持.
        rectTransform = gameObject.GetComponent<RectTransform>(); // RectTransformを取得.
        _tmp = gameObject.GetComponent<TextMeshProUGUI>(); // TMPコンポーネントを取得.
        _getter = getter ?? throw new ArgumentNullException(nameof(getter)); // ゲッターがnullでないことを確認し，フィールドに保存.
        _last = _getter();

        _tmp.text = _getter().ToString();

        eventHandler += () => // タイマーイベントが発生したときの処理.
        {
            T cur = _getter();
            // Debug.Log("Current value: " + cur + ", Last value: " + _last);
            if (!Equals(cur)) // 現在の値が最後に記録された値と異なる場合，テキストを更新.
            {
                _last = cur;
                // ===== メインスレッドでUIの操作を実行 ====================
                _mainThreadContext.Post(_ =>
                {
                    _tmp.text = Formatter(cur); // Unity API を操作
                }, null);
                // ======================================================
            }

        }; // eventHanderのラムダ式.

    } // DynamicText<T>()

    private bool Equals(T other)
    {
        return EqualityComparer<T>.Default.Equals(_last, other);
    }


    private string Formatter(T value)
    {
        if (typeof(T) == typeof(float))
            return ((float)(object)value).ToString("0.000", System.Globalization.CultureInfo.InvariantCulture);
        return value?.ToString() ?? string.Empty;
    }


}
