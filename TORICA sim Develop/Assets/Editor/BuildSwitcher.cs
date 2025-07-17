// ファイルパス: Assets/Editor/BuildSwitcher.cs
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;
using System.IO;

public class BuildSwitcher
{
    private const string SteamVRDefine = "USE_STEAMVR";
    private const string SteamVR_FolderPath = "Assets/SteamVR";
    private const string SteamVR_Disabled_FolderPath = "Assets/SteamVR~";

    [MenuItem("Tools/Toggle SteamVR/Enable SteamVR")]
    private static void EnableSteamVR()
    {
        // フォルダを有効化 (SteamVR~ -> SteamVR)
        if (AssetDatabase.IsValidFolder(SteamVR_Disabled_FolderPath))
        {
            AssetDatabase.MoveAsset(SteamVR_Disabled_FolderPath, SteamVR_FolderPath);
            Debug.Log("Enabled SteamVR Folder.");
        }
        
        // スクリプト定義シンボルを有効化
        SetDefine(SteamVRDefine, true);
        
        AssetDatabase.Refresh();
    }

    [MenuItem("Tools/Toggle SteamVR/Disable SteamVR")]
    private static void DisableSteamVR()
    {
        // スクリプト定義シンボルを無効化
        SetDefine(SteamVRDefine, false);
        
        // フォルダを無効化 (SteamVR -> SteamVR~)
        if (AssetDatabase.IsValidFolder(SteamVR_FolderPath))
        {
            AssetDatabase.MoveAsset(SteamVR_FolderPath, SteamVR_Disabled_FolderPath);
            Debug.Log("Disabled SteamVR Folder.");
        }
        
        AssetDatabase.Refresh();
    }
    
    // スクリプト定義シンボルを操作するヘルパーメソッド
    private static void SetDefine(string define, bool enabled)
    {
        BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
        string currentDefines = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);

        if (enabled)
        {
            if (!currentDefines.Contains(define))
            {
                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, currentDefines + ";" + define);
                Debug.Log($"Enabled {define} symbol.");
            }
        }
        else
        {
            if (currentDefines.Contains(define))
            {
                string newDefines = currentDefines.Replace(";" + define, "").Replace(define, "");
                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, newDefines);
                Debug.Log($"Disabled {define} symbol.");
            }
        }
    }
}