using UnityEngine;
using System.IO;

public class DataToGraph : MonoBehaviour
{
    // テスト用
    void Start()
    {
        // 0〜100のランダムなデータ100個
        float[] sampleData = new float[100];
        for (int i = 0; i < 100; i++) sampleData[i] = Random.Range(0f, 100f);

        CreateAndSaveGraph(sampleData, 1600, 1200, "MyGraph.png");
    }

    public void CreateAndSaveGraph(float[] dataPoints, int width, int height, string fileName)
    {
        // 1. テクスチャ作成
        Texture2D texture = new Texture2D(width, height);

        // 背景を白で塗りつぶす
        Color[] resetColors = new Color[width * height];
        for (int i = 0; i < resetColors.Length; i++) resetColors[i] = Color.white;
        texture.SetPixels(resetColors);

        // 枠線描写
        DrawLine(texture, 100, 100, 100, height-30, Color.gray); // 縦軸
        DrawLine(texture, 100, 100, width-30, 100, Color.gray); // 横軸
        // グラフの描写範囲はx:100~1170，y:100~1570

        // 2. グラフを描画
        Color lineColor = Color.blue;
        float maxVal = 100f; // データの最大値（適宜調整）
        float xStep = (float)width / (dataPoints.Length - 1);

        for (int i = 0; i < dataPoints.Length - 1; i++)
        {
            // 座標計算
            int x0 = (int)(i * xStep);
            int y0 = (int)((dataPoints[i] / maxVal) * height);
            int x1 = (int)((i + 1) * xStep);
            int y1 = (int)((dataPoints[i + 1] / maxVal) * height);

            // 線を引く（簡易的な線分描画）
            DrawLine(texture, x0, y0, x1, y1, lineColor);
        }

        texture.Apply();

        // 3. 保存
        byte[] bytes = texture.EncodeToPNG();
        // 1. ベースとなるパス（exeのある場所、またはプロジェクトルート）を取得
        // Application.dataPath は ".../Assets" や ".../Game_Data" を返すので、その親を取得する
        string exeDirectory = Directory.GetParent(Application.dataPath).FullName;

        // 2. サブフォルダ名を結合（例: "SavedGraphs"）
        string saveDirectory = Path.Combine(exeDirectory, "SavedGraphs");

        // 3. フォルダが存在しなければ作成
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }
        string path = Path.Combine(saveDirectory, fileName);
        File.WriteAllBytes(path, bytes);
        Debug.Log("グラフ生成完了: " + path);

        Destroy(texture);
    }

    // ブレゼンハムのアルゴリズム的な線分描画関数
    private void DrawLine(Texture2D tex, int x0, int y0, int x1, int y1, Color col)
    {
        int dx = Mathf.Abs(x1 - x0);
        int dy = Mathf.Abs(y1 - y0);
        int sx = x0 < x1 ? 1 : -1;
        int sy = y0 < y1 ? 1 : -1;
        int err = dx - dy;

        while (true)
        {
            // 範囲内のみ描画
            if (x0 >= 0 && x0 < tex.width && y0 >= 0 && y0 < tex.height)
                tex.SetPixel(x0, y0, col);

            if (x0 == x1 && y0 == y1) break;
            int e2 = 2 * err;
            if (e2 > -dy) { err -= dy; x0 += sx; }
            if (e2 < dx) { err += dx; y0 += sy; }
        }
    }
}