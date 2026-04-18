using UnityEngine;
/// <summary>
/// ステップの進行やゲームクリアの条件を定義するインタフェース
/// </summary>
public interface ICondition
{
    /// <summary>
    /// 条件を満たしたか確認
    /// </summary>
    /// <returns></returns>
    bool CheckCondition();
    
    /// <summary>
    /// リトライ時などに、条件の進行状況を初期状態にクリア
    /// </summary>
    void ResetCondition();
}
