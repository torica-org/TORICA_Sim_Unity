using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // シーン管理のために必要
using TMPro; // TextMeshProを扱うために必要

public class DetectKeyInput : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "FlightScene"; // 切り替え先のシーン名をInspectorで設定可能にする
    [SerializeField] public TextMeshProUGUI statusText; // 型を TMP に変更

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Debug.Log("A key was pressed this frame.");

            if (statusText != null)
            {
                statusText.text = "Loading...";
            }

            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        // 1. 指定されたシーンが存在するか確認
        if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogError("切り替え先のシーン名が設定されていません。Inspectorを確認してください。");
            return;
        }

        // 2. シーンをロード
        try
        {
            SceneManager.LoadScene(nextSceneName);
        }
        catch (System.Exception e)
        {
            Debug.LogError("シーンのロードに失敗しました。シーン名が正しいか、Build Settingsに追加されているか確認してください。エラー: " + e.Message);
        }

    }
}