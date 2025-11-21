using UnityEngine;
using TMPro; // TextMeshProを使用

public class DisplayVersion : MonoBehaviour
{
    public TMP_Text versionText; // TextMeshProのオブジェクト

    void Start()
    {
        versionText.text = "TORICA Sim  v" + Application.version;
    }
}