using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Threading;
using System.Timers;
using UnityEngine.UI;
using TMPro;

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
public sealed class DynamicText<T> : UIBase
{
    private TextMeshProUGUI _tmp;
    private Getter<T> _getter;
    private T _last;


    public DynamicText(GameObject parent, string objectName, Getter<T> getter, float fontSize) : base() // `base()`を呼び出して`UIBase`のコンストラクタも実行.
    {
        gameObject = UIHelper.NewTextObj(parent, objectName, fontSize); // 動的テキストを生成し，生成されたオブジェクトを保持.
        rectTransform = gameObject.GetComponent<RectTransform>(); // RectTransformを取得.
        _tmp = gameObject.GetComponent<TextMeshProUGUI>(); // TMPコンポーネントを取得.
        _getter = getter ?? throw new ArgumentNullException(nameof(getter)); // ゲッターがnullでないことを確認し，フィールドに保存.
        _last = _getter();

        _tmp.text = UIHelper.Formatter<T>(_last);

        eventHandler += OnTimerEvent; // タイマーイベントが発生したときの処理.

        _instances.Add(this); // インスタンスをリストに追加.
    } // DynamicText<T>()


    private void OnTimerEvent()
    {
        T cur = _getter();
        // Debug.Log("Current value: " + cur + ", Last value: " + _last);

        if (!Equals(cur)) // 現在の値が最後に記録された値と異なる場合，テキストを更新.
        {
            _last = cur;

            // ===== メインスレッドでUIの操作を実行 ====================
            _mainThreadContext.Post(_ =>
            {
                _tmp.text = UIHelper.Formatter<T>(cur); // Unity API を操作
            }, null);
            // ======================================================
        }

        if (gameObject == null)
        {
            Debug.Log("GameObject has been destroyed. Removing event handler.");
            eventHandler -= OnTimerEvent; // オブジェクトが破棄されていた場合イベントハンドラーから削除.
        }
    }


    private bool Equals(T other)
    {
        return EqualityComparer<T>.Default.Equals(_last, other);
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
}
