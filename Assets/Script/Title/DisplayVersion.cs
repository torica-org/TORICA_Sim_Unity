using UnityEngine;
using TMPro; // TextMeshProを使用する場合

public class DisplayVersion : MonoBehaviour
{
    public TMP_Text versionText; // TextMeshProのテキストコンポーネント

    void Start()
    {
        // アプリケーションのバージョン番号を取得してテキストに設定
        versionText.text = "v" + Application.version;
    }
}