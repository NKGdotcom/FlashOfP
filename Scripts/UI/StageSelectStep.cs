using UnityEngine;

public class StageSelectStep : StepBase
{
    [SerializeField] private BaseStageSelect[] stageSelectButtonLists;

    [SerializeField] private SceneChangeStep nextSceneStep;

    private void Awake()
    {
        foreach(var stageSelect in stageSelectButtonLists)
        {
            stageSelect.OnClick += OnStageSelected;
        }
    }

    public override void UpdateStep()
    {

    }

    //ƒ{ƒ^ƒ“‚©‚çGameObject‚ª‘—‚ç‚ê‚Ä‚­‚é
    private void OnStageSelected(GameObject selectedStage)
    {

        if(selectedStage != null)
        {
            nextSceneStep.SetNextStage(selectedStage);
        }

        Complete();
    }
}
