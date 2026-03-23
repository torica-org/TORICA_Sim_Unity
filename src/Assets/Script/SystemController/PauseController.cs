using UnityEngine;
using UnityEngine.InputSystem; // 新しいInput System用

public class PauseController : MonoBehaviour
{
    [Header("ポーズ画面のパネル")]
    [SerializeField] private GameObject pausePanel;

    // ポーズ中かどうかのフラグ
    private bool isPaused = false;

    void Start()
    {
        // 開始時は必ずポーズ解除状態にする
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
        ResumeGame();
    }

    void Update()
    {
        // ESCキーが押されたら切り替え
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
            {
                ResumeGame(); // 再開
            }
            else
            {
                PauseGame(); // 一時停止
            }
        }
    }

    // ゲームを一時停止する処理
    public void PauseGame()
    {
        isPaused = true;

        // 1. 時間を止める
        Time.timeScale = 0f;

        // 2. パネルを表示する
        if (pausePanel != null) pausePanel.SetActive(true);

        // 3. マウスカーソルを表示してロックを解除する（フライトシミュでは重要！）
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // 音を止める場合（必要なら）
        // AudioListener.pause = true;
    }

    // ゲームを再開する処理
    public void ResumeGame()
    {
        isPaused = false;

        // 1. 時間を動かす（1が通常速度）
        Time.timeScale = 1f;

        // 2. パネルを隠す
        if (pausePanel != null) pausePanel.SetActive(false);

        // 3. マウスカーソルを消してロックする（必要なら）
        // ※ゲームの仕様に合わせて調整してください
        // Cursor.visible = false;
        // Cursor.lockState = CursorLockMode.Locked;

        // AudioListener.pause = false;
    }

    // （おまけ）ボタンから呼び出す用の「ゲーム終了」関数
    public void QuitGame()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}