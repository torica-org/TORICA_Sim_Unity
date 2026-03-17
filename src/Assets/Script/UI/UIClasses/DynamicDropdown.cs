using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq; // `SequenceEqual`を使うために必要.
using UnityEngine.Events; // UnityActionのために必要
using UnityEngine.UI;
using TMPro;

// ===== 概要 ======================================
// インスタンス化の際に，オプションのリストの参照を渡してドロップダウンを生成する.
// 参照は内部で維持・監視され，オプションのリストが変更されたときにドロップダウンの内容も自動的に更新される.


// ===== 使い方 ======================================
// ----------------------------------------
// DynamicDropdown dynamicDropdown = new(scrollContent, "DropdownTest", categories, (x) => { Debug.Log("Selected: " + x); });
// RectTransform dpdnRect = dynamicDropdown.rectTransform;
// dpdnRect.anchoredPosition = new Vector2(0, -100);
// dpdnRect.localScale = new Vector3(3, 3, 1); // ドロップダウンのサイズを変更する
// dpdnRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 300); // RectTransformのx軸方向のサイズを変更する
// ----------------------------------------
// basePanel: 親のゲームオブジェクト
// "DropdownTest": 生成するドロップダウンのゲームオブジェクト名
// categories: ドロップダウンのオプションのリスト
// (x) => { Debug.Log("Selected: " + x); }: ドロップダウンの値が変更されたときのコールバック関数（ラムダ式で書くのが一番簡単）


// ===== 動的ドロップダウンを生成するクラス ========================================================
public class DynamicDropdown : UIBase
{
    private List<string> _refOptions; // 参照するリストを保存するフィールド.
    private List<string> _previousOptions; // 前回のオプションを保存するフィールド. これを使ってオプションの変更を検出する.

    private TMP_Dropdown _tmpDpdn;

    public DynamicDropdown(GameObject parent, string objectName, List<string> options, UnityAction<Int32> callback)
    {
        _refOptions = options ?? throw new System.ArgumentNullException(nameof(options)); // 参照するリストがnullでないことを確認し，フィールドに保存.

        // 生成時に直接親キャンバスを指定
        gameObject = UnityEngine.Object.Instantiate(DefaultDropdown, parent.transform, false);

        // その他の設定
        gameObject.name = objectName;

        _tmpDpdn = gameObject.GetComponent<TMP_Dropdown>();
        _tmpDpdn.ClearOptions(); // 既存のオプションをクリア.
        _tmpDpdn.AddOptions(_refOptions);
        _tmpDpdn.onValueChanged.AddListener(callback);

        _previousOptions = new List<string>(_refOptions); // 初期状態のオプションを保存.

        rectTransform = gameObject.GetComponent<RectTransform>(); // RectTransformを取得.

        eventHandler += OnTimerEvent; // タイマーイベントが発生したときの処理.

        _instances.Add(this); // インスタンスをリストに追加.
    }



    private void OnTimerEvent()
    {
        if (!gameObject) return; // ゲームオブジェクトが存在しない場合は処理を中断.

        if (!_refOptions.SequenceEqual(_previousOptions))
        {
            _previousOptions = new List<string>(_refOptions); // オプションが変更されたときに、現在のオプションを保存.
            _tmpDpdn.ClearOptions(); // 既存のオプションをクリア.
            _tmpDpdn.AddOptions(_refOptions);

            // ===== メインスレッドでUIの操作を実行 ====================
            _mainThreadContext.Post(_ =>
            {
                _tmpDpdn.RefreshShownValue(); // 表示を更新.
            }, null);
            // ======================================================
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
}
