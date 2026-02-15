using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO; // ファイル入出力

public class IMGUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // デバッグ用
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 50), "Write CSV"))
        {
            CsvIO csv = new();
            csv.Write(1, 1, "Hello");
            csv.Write(2, 1, "World");
            csv.Flush(Path.Combine(Directory.GetParent(Application.dataPath).FullName, "Test.csv"));
        }
        if (GUI.Button(new Rect(10, 70, 100, 50), "Read CSV"))
        {
            CsvIO csv = new();
            csv.Load(Path.Combine(Directory.GetParent(Application.dataPath).FullName, "Test.csv"));
            print(csv.Read(1, 1));
            print(csv.Read(2, 1));
        }
    }

}
