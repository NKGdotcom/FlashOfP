using UnityEngine;

/// <summary>
/// ゲーム本編のクリア条件となる部分の追加
/// </summary>
public class BaseCondition : MonoBehaviour, ICondition
{
    //条件を満たしたかどうか
    protected bool isFinish;
    
    /// <summary>
    /// 条件を満たしたかどうか判定
    /// </summary>
    /// <returns></returns>
    public virtual bool CheckCondition()
    {
        return isFinish;
    }
    
    /// <summary>
    /// リトライ時などに、条件の進行状態を初期状態にリセットする
    /// </summary>
    public virtual void ResetCondition()
    {
        isFinish = false;
    }
}
