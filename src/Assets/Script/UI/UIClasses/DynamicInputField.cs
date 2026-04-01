using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Threading;
using System.Timers;
using UnityEngine.UI;
using TMPro;

// ===== 概要 =========================
// 特定の変数に関するゲッターを渡してインスタンス化する.
// 変数の値が変化したときにゲッターを介してテキストの内容を更新する.

// ===== 使い方 =========================
// ---------------------------------------------------------------------------------------------------------------------------------------------
// DynamicText<float> dynamicText = new(basePanel, "TextDistUpper", () => { return gm.massLeftFactor; }, 50.0f);
// GameObject textObj = dynamicText.gameObject; // プロパティにより取得.
// RectTransform textRect = dynamicText.rectTransform; // プロパティにより取得.
// ---------------------------------------------------------------------------------------------------------------------------------------------

// basePanel: 親のゲームオブジェクト
// "TextDistUpper": 生成するテキストのゲームオブジェクト名
// () => { return gm.massLeftFactor; }: ゲッターの関数（ラムダ式で書くのが一番簡単）.
// 50.0f: フォントサイズ.

// ===== 動的テキストを生成するクラス ===================================
public sealed class DynamicInputField<T> : UIBase
{
    private TMP_InputField _input;
    private TMP_Text _placeholder;
    private Setter<T> _setter;
    private Getter<T> _getter;
    private T _last;

    // ===== コンストラクタ =======================================================================================
    public DynamicInputField(GameObject parent, string objectName, string placeholder,
        Setter<T> setter, Getter<T> getter)
    {
        // ----- セッターとゲッターがnullでないことを確認し，フィールドに保存 ------------------------
        _setter = setter ?? throw new ArgumentNullException(nameof(setter));
        _getter = getter ?? throw new ArgumentNullException(nameof(getter));

        // ----- オブジェクトやコンポーネントを取得 -------------------------------------------------------------------------------
        gameObject = UnityEngine.Object.Instantiate(DefaultInputField, parent.transform, false); // 生成時に直接親キャンバスを指定
        gameObject.name = objectName;
        rectTransform = gameObject.GetComponent<RectTransform>(); // RectTransformを取得
        _input = gameObject.GetComponent<TMP_InputField>(); // TMPコンポーネントを取得.
        _placeholder = _input.placeholder as TMP_Text; // プレースホルダーを取得.
        _placeholder.enableAutoSizing = false;

        // ----- `alignment`を設定 -----------------------------------------
        _placeholder.alignment = TextAlignmentOptions.Left; // 左寄せ.

        // ----- 値の設定 -----------------
        _last = _getter();
        _input.text = Formatter<T>(_last);
        _placeholder.text = placeholder;

        // ----- イベントとインスタンスの登録 ------------------------------------------
        eventHandler += OnTimerEvent; // タイマーイベントが発生したときの処理.
        _instances.Add(this); // インスタンスをリストに追加.
    }

    // ===== 定期的に実行されるイベントで実行される関数 =========================
    private void OnTimerEvent()
    {
        T cur = _getter();
        T input = Converter(_input.text); // `string`を変換して代入.
        // Debug.Log("Current value: " + cur + ", Last value: " + _last);

        if (!Equals(cur)) // 現在の値が最後に記録された値と異なる場合，テキストを更新.
        {
            _last = cur;

            // ----- メインスレッドでUIの操作を実行 -------------------
            _mainThreadContext.Post(_ =>
            {
                _input.text = Formatter<T>(_last); // Unity API を操作
            }, null);
            // -----------------------------------------------------
        }
        else if (!Equals(input))
        {
            _last = input;
            _setter(_last);
        }

        if (gameObject == null)
        {
            Debug.Log("GameObject has been destroyed. Removing event handler.");
            eventHandler -= OnTimerEvent; // オブジェクトが破棄されていた場合イベントハンドラーから削除.
        }
    }

    // ===== `_last`との等価判定用関数 =======================================
    private bool Equals(T other)
    {
        return EqualityComparer<T>.Default.Equals(_last, other);
    }

    // ===== ジェネリック型に変換 ==============================
    private T Converter(string value)
    {
        if (value == "") // 空欄のときはデフォルト値を返す.
        {
            return default(T);
        }
        return (T)Convert.ChangeType(value, typeof(T));
    }

    // ===== 破棄される際の処理 ==============================================
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
}