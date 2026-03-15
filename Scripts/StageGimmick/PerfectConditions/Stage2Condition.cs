using UnityEngine;
/// <summary>
/// Stage 2 PerfectCondition
/// </summary>
public class Stage2Condition : BasePerfectCondition
{
    [SerializeField] private WordFlip wordFlip;
    [SerializeField] private WordJump wordJump;
    //---それぞれが完了したかどうかを記録するフラグ---
    private bool isFlipComplete = false;
    private bool isJumpComplete = false;

    private void OnEnable()
    {
        if (wordFlip != null)
        {
            wordFlip.WordComplete += OnFlipComplete;
        }
        if (wordJump != null)
        {
            wordJump.WordComplete += OnJumpComplete;
        }
    }

    private void OnDisable()
    {
        if (wordFlip != null)
        {
            wordFlip.WordComplete -= OnFlipComplete;
        }
        if (wordJump != null)
        {
            wordJump.WordComplete -= OnJumpComplete;
        }
    }

    private void OnFlipComplete()
    {
        isFlipComplete = true;
    }

    private void OnJumpComplete()
    {
        isJumpComplete = true;
    }

    // StageGoalなどから呼ばれる判定メソッド
    public override bool IsPerfect()
    {
        return isFlipComplete && isJumpComplete;
    }
}
