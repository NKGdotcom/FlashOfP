using UnityEngine;
/// <summary>
/// Stage 4 PerfectCondition
/// </summary>
public class Stage4Condition : BasePerfectCondition
{
    [SerializeField] private ResetStage resetStage;
    private int targetActiveNum = 0;

    public override bool IsPerfect()
    {
        if (resetStage == null)
        {
            Debug.LogWarning("ResetStage がセットされていません！");
            return false;
        }

        return resetStage.GetActiveExplosionCount() <= targetActiveNum;
    }
}
