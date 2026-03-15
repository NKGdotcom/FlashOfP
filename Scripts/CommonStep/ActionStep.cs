using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;
using UnityEngine;
/// <summary>
/// 何か条件が終わるまで(ゲーム本編)
/// </summary>
public class ActionStep : StepBase
{
    //---次に進む条件となるもの---
    [SerializeField] private BaseCondition condition;
    private bool nextStep = false;

    private void Awake()
    {
        if (condition == null) { Debug.LogError("conditionが参照されていません");  return; }
    }
    public override void EnterStep()
    {
        GameStart();
    }
    /// <summary>
    /// ゲームスタート
    /// </summary>
    private void GameStart()
    {
        nextStep = false;
        GameState.Instance.SetState(State.GAME_ACT);
    }
    public override void UpdateStep()
    {
        if (condition.CheckCondition())
        {
            if (!nextStep)
            {
                nextStep = true;
                ExitStep();
            }
        }
    }
    public override UniTask RetryStep(CancellationToken token)
    {
        condition.ResetCondition();
        nextStep = false;

        return UniTask.CompletedTask;
    }
    public override void ExitStep()
    {
        Complete();
    }
}
