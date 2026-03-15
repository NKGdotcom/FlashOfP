using UnityEngine;
/// <summary>
/// Stage 3 PerfectCondition
/// </summary>
public class Stage3Condition : BasePerfectCondition
{
    [SerializeField] private PlayerPopcorn playerPopcorn;
    [SerializeField] private int targetPopcornNum = 10;

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
