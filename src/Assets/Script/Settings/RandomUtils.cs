using UnityEngine;

/// <summary>
/// 乱数生成に関するユーティリティクラス
/// </summary>
public static class RandomUtils
{
    /// <summary>
    /// 中心極限定理を利用して、指定範囲内で中央値に寄った乱数を生成します。
    /// </summary>
    /// <remarks>
    /// 複数回生成した一様乱数の平均値を取ることで、正規分布に似た釣鐘型の分布を擬似的に作り出します。
    /// iterationsの値を大きくするほど、分布はより強く中央に集中します。
    /// </remarks>
    /// <param name="min">乱数の最小値</param>
    /// <param name="max">乱数の最大値</param>
    /// <param name="iterations">乱数を生成し平均を取る回数。3〜5程度の値が一般的です。</param>
    /// <returns>指定範囲内で、中央値付近に分布する乱数</returns>
    public static float RangeByCentralLimit(float min, float max, int iterations = 3)
    {
        // iterationsが1未満にならないように保証する
        if (iterations < 1)
        {
            iterations = 1;
        }

        float sum = 0f;
        // 指定された回数だけ、範囲内の一様乱数を生成して合計する
        for (int i = 0; i < iterations; i++)
        {
            sum += Random.Range(min, max);
        }

        // 合計を回数で割って平均値を返す
        return sum / iterations;
    }

    // （もし前回の回答の関数も利用する場合は、ここにRangeGaussianメソッドなどを記述します）
}