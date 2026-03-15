using UnityEngine;
/// <summary>
/// Stage 5 PerfectCondition
/// </summary>
public class Stage5Condition : BasePerfectCondition
{
    [SerializeField] private WordUp wordUp;

    public override bool IsPerfect()
    {
        if (wordUp == null)
        {
            Debug.LogWarning("wordUpがセットされていません！");
            return false;
        }

        return !wordUp.IsUp;
    }
}
