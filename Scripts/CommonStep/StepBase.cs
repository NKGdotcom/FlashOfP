using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
/// <summary>
/// ステップの元となるクラス
/// </summary>
public abstract class StepBase : MonoBehaviour, IStep
{
    //ステップ完了時に通知
    public event Action OnFinishStep;

    /// <summary>
    /// このステップに入った瞬間に呼ばれる処理(初期化)
    /// </summary>
    public virtual void EnterStep() { }

    /// <summary>
    /// このステップにいる間マイフレーム呼ばれる更新処理
    /// </summary>
    public virtual void UpdateStep() { }

    /// <summary>
    /// このステップを終了して次へ進むときの処理
    /// </summary>
    public virtual void ExitStep() { }

    /// <summary>
    /// ゲームをリトライした際に、このステップの状態を初期化
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public virtual UniTask RetryStep(CancellationToken _token)
    {
       return UniTask.CompletedTask;
    }

    /// <summary>
    /// 終了したときに呼び出す
    /// </summary>
    protected void Complete()
    {
        OnFinishStep?.Invoke();
    }
}
