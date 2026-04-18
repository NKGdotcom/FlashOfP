using UnityEngine;
/// <summary>
/// ステージの条件付きクリアを決めるクラス
/// </summary>
public abstract class BasePerfectCondition : MonoBehaviour
{
    /// <summary>
    /// 条件付きクリアを達成しているかどうか
    /// </summary>
    /// <returns></returns>
    public abstract bool IsPerfect();
}
