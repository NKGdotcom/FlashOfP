using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;
using UnityEngine;

/// <summary>
/// ゲーム本編(アクションパート)の進行を管理するステップ
/// 指定されたクリア条件(condition)を満たすまでプレイヤーの操作を許可し、満たしたら次のステップへ移動
/// </summary>
public class ActionStep : StepBase
{
    [Header("進行条件")]
    [Tooltip("このステップをクリアするための条件を判定するクラス")]
    [SerializeField] private BaseCondition condition;
    //重複してクリア処理(ExitStep)が呼ばれるのを防ぐためのフラグ
    private bool isCleared = false;

    private void Awake()
    {
        if (condition == null) { Debug.LogError("conditionが参照されていません");  return; }
    }

    /// <summary>
    /// このステップに入った瞬間に呼ばれる処理(初期化)
    /// </summary>
    public override void EnterStep()
    {
        isCleared = false;
        //ゲームのプレイ中に状態を設定
        GameState.Instance.SetState(State.GAME_ACT);
    }

    /// <summary>
    /// このステップにいる間マイフレーム呼ばれる更新処理
    /// </summary>
    public override void UpdateStep()
    {
        //すでに条件を満たしたら、以降の判定は行わない
        if (isCleared) return;

        //マイフレーム条件を監視し、満たした瞬間に終了処理へ移行する
        if (condition.CheckCondition())
        {
            isCleared = true;
            ExitStep();
        }
    }

    /// <summary>
    /// このステップを終了して次へ進むときの処理
    /// </summary>
    public override void ExitStep() => Complete();

    /// <summary>
    /// ゲームをリトライした際に、このステップの状態を初期化
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public override UniTask RetryStep(CancellationToken token)
    {
        //判定条件の進行度をリセット
        condition.ResetCondition();
        isCleared = false;

        //非同期処理で待つ必要がないため、即座に完了したタスクを返す
        return UniTask.CompletedTask;
    }
}
