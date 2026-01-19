using System;
using UnityEngine;

public abstract class StepBase : MonoBehaviour
{
    //ステップ完了時に通知
    public Action OnStepCompleted;

    //参照チェックをする
    public virtual void OnInitialized()
    {

    }

    //ステップが入ったとき
    public virtual void EnterStep(PlayerMoveInput _playerMoveInput)
    {

    }

    //毎フレームチェック
    public abstract void UpdateStep();

    //ステップが終わったときの処理
    public virtual void ExitStep()
    {

    }

    //ステップが完了したと伝える
    protected void Complete()
    {
        OnStepCompleted?.Invoke();
    }
}
