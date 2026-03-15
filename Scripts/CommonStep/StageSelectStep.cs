using UnityEngine;

/// <summary>
/// ステージセレクトステップ
/// </summary>
public class StageSelectStep : StepBase
{
    [SerializeField] private BaseStageSelect[] stageSelectButtonLists;
    [SerializeField] private SceneChangeStep nextSceneStep;

    private void Awake()
    {
        if(stageSelectButtonLists == null) { Debug.LogError("stageSelectButtonListsが参照されていません"); return;}
        if(nextSceneStep == null) { Debug.LogError("nextSceneStepが参照されていません"); return; }

        foreach(var stageSelect in stageSelectButtonLists)
        {
            stageSelect.OnClick += OnStageSelected;
        }
    }
    public override void EnterStep()
    {
        GameState.Instance.SetState(State.STAGE_SELECT);
    }
    public override void UpdateStep()
    {

    }
    //---ボタンからステージが送られてくる---
    private void OnStageSelected(GameObject _selectedStage)
    {
        if(_selectedStage != null)
        {
            nextSceneStep.SetNextStage(_selectedStage);
        }
        ExitStep();
    }
    public override void ExitStep()
    {
        Complete();
    }

    private void OnDestroy()
    {
        foreach (var stageSelect in stageSelectButtonLists)
        {
            stageSelect.OnClick -= OnStageSelected;
        }
    }
}
