using UnityEngine;
/// <summary>
/// 私は条件ですインタフェース
/// </summary>
public interface ICondition
{
    //条件を満たしたか確認
    bool CheckCondition();
    //条件をリセット
    void ResetCondition();
}
