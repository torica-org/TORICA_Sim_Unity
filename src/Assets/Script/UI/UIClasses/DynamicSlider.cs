using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Threading;
using System.Timers;
using UnityEngine.UI;

// ===== 概要 =========================
// 特定の変数に関するセッターとゲッターを渡してインスタンス化する.
// 変数の値が変化したときにゲッターを介してスライダーの値を更新し，スライダーの値が変化したときにセッターを介して変数を更新する.

// ===== 使い方 =========================
// ---------------------------------------------------------------------------------------------------------------------------------------------
// DynamicSlider slider = new(basePanel, "SliderTest", (x) => { gm.massRightFactor = x; }, () => { return gm.massRightFactor; }, 0.0f, 1.0f, 0.1f);
// GameObject sliderobj = slider.gameObject; // プロパティにより取得.
// RectTransform sliderRect = slider.rectTransform; // プロパティにより取得.
// ---------------------------------------------------------------------------------------------------------------------------------------------

// basePanel: 親のゲームオブジェクト
// "SliderTest": 生成するスライダーのゲームオブジェクト名
// (x) => { gm.massRightFactor = x; }: セッターの関数（ラムダ式で書くのが一番簡単）
// () => { return gm.massRightFactor; }: ゲッターの関数（ラムダ式で書くのが一番簡単）
// 0.0f: 最小値
// 1.0f: 最大値
// 0.1f: スライダーのステップ（省略可能）

// ===== スライダー（変数監視機能付き）を生成するクラス ========================================================
public sealed class DynamicSlider : UIBase
{
    private Slider _slider;

    private Setter<float> _setter;
    private Getter<float> _getter;

    private float _last;
    private float _step;

    public DynamicSlider(GameObject parent, string objectName, Setter<float> setter, Getter<float> getter,
        float minVal, float maxVal, float step = 0.0f)
    {
        _setter = setter ?? throw new ArgumentNullException(nameof(setter)); // セッターとゲッターがnullでないことを確認し，フィールドに保存.
        _getter = getter ?? throw new ArgumentNullException(nameof(getter));

        _step = step; // ステップを保存.

        _last = _getter(); // 初期値を取得して保存.

        // 生成時に直接親キャンバスを指定
        gameObject = UnityEngine.Object.Instantiate(DefaultSlider, parent.transform, false);

        // その他の設定
        gameObject.name = objectName;

        _slider = gameObject.GetComponent<Slider>(); // スライダーコンポーネントを取得.
        _slider.minValue = minVal;
        _slider.maxValue = maxVal;
        _slider.value = _last;

        rectTransform = gameObject.GetComponent<RectTransform>(); // RectTransformを取得.

        eventHandler += OnTimerEvent; // タイマーイベントが発生したときの処理.

        _instances.Add(this); // インスタンスをリストに追加.
    } // DynamicSlider()

    private void OnTimerEvent()
    {
        float curVar = _getter();
        float curSlider = GetNearestValue(_slider.value);

        if (curVar != _last) // 現在の値が最後に記録された値と異なる場合，スライダーの値を更新.
        {
            _last = curVar;
            curSlider = GetNearestValue(curVar);
        }
        else if (curSlider != _last) // スライダーの値と現在の値が異なる場合，セッターを介して変数を更新.
        {
            _last = curSlider;
            _setter(curSlider);
        }

        // ----- メインスレッドでUIの操作を実行 ------------------
        _mainThreadContext.Post(_ =>
        {
            _slider.value = curSlider; // Unity API を操作
        }, null);
        // ----------------------------------------------------

        if (gameObject == null)
        {
            Debug.Log("GameObject has been destroyed. Removing event handler.");
            eventHandler -= OnTimerEvent; // オブジェクトが破棄されていた場合イベントハンドラーから削除.
        }
    }

    private float GetNearestValue(float rawVal)
    {
        if (_step == 0.0f)
        {
            return rawVal;
        }
        else
        {
            int steps = (int)Mathf.Round(rawVal / _step);
            return _step * steps;
        }
    }

    public override void Dispose()
    {
        eventHandler -= OnTimerEvent; // イベントハンドラーから削除.

        _mainThreadContext.Post(_ =>
        {
            UnityEngine.Object.Destroy(gameObject);
            gameObject = null;
            rectTransform = null;
        }, null);
    }
} // class DynamicSlider