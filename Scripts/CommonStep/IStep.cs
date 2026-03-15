using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

/// <summary>
/// リセットの初期処理
/// </summary>
public interface IStep
{
    void EnterStep(); //ステップに入ったとき

    void UpdateStep(); //ステップの間

    void ExitStep(); //ステップが終わったとき

    UniTask RetryStep(CancellationToken _token); //リトライ時の初期化

    event Action OnFinishStep; //ステップが終わったことを知らせる
}
