using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

/// <summary>
/// ステップの処理の一通りの流れ
/// </summary>
public interface IStep
{
    /// <summary>
    /// ステップに入った瞬間に呼び出す(初期化)
    /// </summary>
    void EnterStep(); 

    /// <summary>
    /// ステップの間マイフレーム呼び出す
    /// </summary>
    void UpdateStep();

    /// <summary>
    /// ステップが終わった時に呼び出す
    /// </summary>
    void ExitStep();

    /// <summary>
    /// リトライ時に呼び出す
    /// </summary>
    /// <param name="_token"></param>
    /// <returns></returns>
    UniTask RetryStep(CancellationToken _token);

    //ステップが終わったことを知らせる
    event Action OnFinishStep; 
}
