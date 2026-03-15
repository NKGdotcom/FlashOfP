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
    /// ステップに入った時に呼び出す
    /// </summary>
    public virtual void EnterStep()
    {

    }
    /// <summary>
    /// 毎フレームチェック
    /// </summary>
    public virtual void UpdateStep()
    {

    }
    /// <summary>
    /// ステップを抜けるときに呼ぶ
    /// </summary>
    public virtual void ExitStep()
    {

    }
    /// <summary>
    /// リトライの時に呼び出す
    /// </summary>
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
