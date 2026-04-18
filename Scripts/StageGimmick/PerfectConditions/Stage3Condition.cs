using UnityEngine;
/// <summary>
/// ステージ3の条件付きクリアの条件
/// </summary>
public class Stage3Condition : BasePerfectCondition
{
    [Header("条件判定用ギミック")]
    [Tooltip("プレイヤーが現在発動しているポップコーンについて")]
    [SerializeField] private PlayerPopcorn playerPopcorn;
    [Tooltip("ポップコーンの発射回数の最大値を決める")]
    [SerializeField] private int targetPopcornNum = 10;

    private void Awake()
    {
        if(playerPopcorn == null) { Debug.LogError("playerPopcornが参照されていません"); return; }
    }

    /// <summary>
    /// ポップコーンの発射回数最大値以内にクリアで来たらクリア
    /// </summary>
    /// <returns></returns>
    public override bool IsPerfect()
    {
        if (playerPopcorn == null)
        {
            Debug.LogWarning("PlayerPopcorn がセットされていません！");
            return false;
        }
        return playerPopcorn.ShotNum  <= targetPopcornNum;
    }
}
