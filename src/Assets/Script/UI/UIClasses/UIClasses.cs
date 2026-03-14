/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Threading;
using System.Timers;
using UnityEngine.UI;
using TMPro;


// ===== 動的テキストを生成するクラス ===================================
public sealed class NewDynamicFloatText : UIBase
{
    private TextMeshProUGUI _text;
    private Getter _getter;
    private float _last;

    public NewDynamicFloatText(GameObject parent, string objectName, Getter getter, float fontSize) : base() // `base()`を呼び出して`stdUI`のコンストラクタも実行.
    {
        gameObject = UIHelper.NewDynamicFloatTextObj(parent, objectName, getter(), fontSize); // 動的テキストを生成し，生成されたオブジェクトを保持.
        _text = gameObject.GetComponent<TextMeshProUGUI>(); // TMPコンポーネントを取得.
        _getter = getter ?? throw new ArgumentNullException(nameof(getter)); // ゲッターがnullでないことを確認し，フィールドに保存.
        _last = _getter();

        eventHandler += () => // タイマーイベントが発生したときの処理.
        {
            float cur = _getter();
            if (cur != _last) // 現在の値が最後に記録された値と異なる場合，テキストを更新.
            {
                _last = cur;
                // ===== メインスレッドでUIの操作を実行 ====================
                _mainThreadContext.Post(_ =>
                {
                    _text.text = cur.ToString("0.000"); // Unity API を操作
                }, null);
                // ======================================================
            }
        }; // eventHanderのラムダ式.

    } // NewDynamicFloatText()
}
*/
