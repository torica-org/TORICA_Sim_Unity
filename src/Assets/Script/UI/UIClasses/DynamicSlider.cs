using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Threading;
using System.Timers;
using UnityEngine.UI;


// ===== スライダー（変数監視機能付き）を生成するクラス ========================================================
public sealed class DynamicSlider : UIBase
{
    private Slider _slider;

    private Setter<float> _setter;
    private Getter<float> _getter;

    private float _last;


    public DynamicSlider(GameObject parent, string objectName, Setter<float> setter, Getter<float> getter, float minVal, float maxVal) : base() // `base()`を呼び出して`stdUI`のコンストラクタも実行.
    {
        gameObject = UIHelper.NewSliderObj(parent, objectName, minVal, maxVal, getter()); // スライダーを生成し，生成されたオブジェクトを保持.
        rectTransform = gameObject.GetComponent<RectTransform>(); // RectTransformを取得.
        _slider = gameObject.GetComponent<Slider>(); // スライダーコンポーネントを取得.

        _setter = setter ?? throw new ArgumentNullException(nameof(setter)); // セッターとゲッターがnullでないことを確認し，フィールドに保存.
        _getter = getter ?? throw new ArgumentNullException(nameof(getter));
        _last = _getter();


        eventHandler += () => // タイマーイベントが発生したときの処理.
        {
            float cur = _getter();
            // Debug.Log($"Current value: {cur}, Last value: {_last}, Slider value: {_slider.value}");
            if (cur != _last) // 現在の値が最後に記録された値と異なる場合，スライダーの値を更新.
            {
                // Debug.Log($"Value changed externally: {cur}");
                _last = cur;

                // ===== メインスレッドでUIの操作を実行 ====================
                _mainThreadContext.Post(_ =>
                {
                    // Debug.Log("Updating slider value on main thread.");
                    _slider.value = cur; // Unity API を操作
                }, null);
                // ======================================================
            }
            else if (_slider.value != _last) // スライダーの値と現在の値が異なる場合，セッターを介して変数を更新.
            {
                // Debug.Log($"Value changed via slider: {_slider.value}");
                _last = _slider.value;
                _setter(_slider.value);
            }

        }; // eventHanderのラムダ式.

    } // DynamicSlider()

} // class DynamicSlider
