using System.Collections;
using UnityEngine;

public class ActionStep : StepBase
{
    [SerializeField] private ConditionBase completeCondition;

    //[SerializeField] private float delayTime = 0.2f;
    private bool nextStep = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
            Debug.LogError("äÆóπèåè(Condition)Ç™ê›íËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒÅI");
        }
    }

    public override void EnterStep(PlayerMoveInput _playerMoveInput)
    {
        base.EnterStep(_playerMoveInput);

        StartCoroutine(EnableInputWithDelay(_playerMoveInput));
    }

    private IEnumerator EnableInputWithDelay(PlayerMoveInput input)
    {

        yield return null;

        if (input != null)
        {
            input.IsTutorial = false;
        }
    }

    public override void UpdateStep()
    {
        if (completeCondition != null && completeCondition.CheckCondition())
        {
            if (!nextStep)
            {
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
