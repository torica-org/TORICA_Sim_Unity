using UnityEngine;
using TMPro; // TextMeshProを使用

public class DisplayVersion : MonoBehaviour
{
    public TMP_Text versionText; // TextMeshProのオブジェクト

    void Start()
    {
        versionText = GameObject.Find("DisplayVersion").GetComponent<TMP_Text>();
        versionText.text = "TORICA Simulator " + Application.version;
    }
}