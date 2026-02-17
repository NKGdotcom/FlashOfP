using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;
using UnityEngine;

public class ActionStep : StepBase
{
    [SerializeField] private ConditionBase completeCondition;

    //[SerializeField] private float delayTime = 0.2f;
    private bool nextStep = false;

    private void Awake()
    {
        OnInitialized();
    }
    public override void OnInitialized()
    {
        base.OnInitialized();

        if (completeCondition != null)
        {
            completeCondition.OnInitialize();
        }
        else
        {
            Debug.LogError("完了条件(Condition)が設定されていません！");
        }
    }

    public override void EnterStep(PlayerMoveInput _playerMoveInput)
    {
        base.EnterStep(_playerMoveInput);
        nextStep = false;
        EnableInputWithDelayAsync(_playerMoveInput, this.GetCancellationTokenOnDestroy()).Forget();
    }

    private async UniTask EnableInputWithDelayAsync(PlayerMoveInput _input, CancellationToken _token)
    {
        await UniTask.Yield(_token);

        if (_input != null) _input.IsTutorial = false;
    }

    public override void UpdateStep()
    {
        if (completeCondition != null && completeCondition.CheckCondition())
        {
            if (!nextStep)
            {
                Debug.Log("次のステップへ");
                nextStep = true;
                Complete();
            }
        }
    }
    public override void ExitStep()
    {
        base.ExitStep();
    }
}
