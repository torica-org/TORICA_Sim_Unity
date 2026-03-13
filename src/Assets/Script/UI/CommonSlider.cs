using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI用の名前空間を宣言
using TMPro; // TMP用の名前空間を宣言

public class CommonSlider : MonoBehaviour
{
    // 定数
    // private const string LABEL = "Label";
    private const string PLACEHOLDER = "Enter value ...";
    // private const float SLIDER_MAX = 10.0f;
    // private const float SLIDER_MIN = 0.0f;

    private TMP_Text _label;
    private TMP_InputField _inputField;
    private TMP_Text _placeholder;
    private Slider _slider;

    private float _value = 0.0f; // 値を代入するか，コンポーネントを代入

    void Start()
    {
        _label = transform.Find("Label").gameObject.GetComponent<TMP_Text>(); // ラベルのコンポーネントを取得

        _inputField = transform.Find("Value").gameObject.GetComponent<TMP_InputField>(); // InputFieldのコンポーネントを取得
        _placeholder = _inputField.placeholder as TMP_Text; // プレースホルダーのコンポーネントをTMP_Textとして取得
        // _label.text = LABEL; // ラベルのテキスト
        _placeholder.text = PLACEHOLDER; // プレースホルダーのテキスト
        _inputField.text = _value.ToString("0.0"); // 値を代入
        _inputField.onEndEdit.AddListener(OnEndEdit); // 編集完了時のイベントリスナーを設定

        _slider = transform.Find("Slider").gameObject.GetComponent<Slider>(); // スライダーのコンポーネントを取得
        // _slider.maxValue = SLIDER_MAX;
        // _slider.minValue = SLIDER_MIN;
    }

    void Update()
    {
        if (_slider.value != _value)
        {
            _value = _slider.value;
            _inputField.text = _value.ToString("0.0");
        }
    }

    private void OnEndEdit(string input) // イベントリスナーで呼び出される関数
    {
        if (_value != float.Parse(input))
        {
            _value = float.Parse(input);
            _slider.value = _value;
        }
    }
}