using UnityEngine;

public class TutorialDialogueStep : StepBase
{
    [SerializeField] private TutorialDialogueData tutorialDialogueData; //チュートリアルで一度に話す量
    private int currentLine;

    private void Awake()
    {
        OnInitialized();
    }

    public override void OnInitialized()
    {
        NullCheck();
    }

    private void NullCheck()
    {
        if (tutorialDialogueData == null) { Debug.LogWarning("tutorialDialogueDataが設定していません"); return; }
    }

    public override void EnterStep(PlayerMoveInput _playerMoveInput)
    {
        base.EnterStep(_playerMoveInput);

        _playerMoveInput.IsTutorial = true;
        TutorialDialogueDisplay.Instance.ShowUI();
        currentLine = 0;
        UpdateView();
    }

    public override void UpdateStep()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentLine++;
            if(currentLine < tutorialDialogueData.DialoguesLists.Count)
            {
                UpdateView();
            }
            else
            {
                ExitStep();
            }
        }
    }

    //チュートリアルのデータを渡す処理
    private void UpdateView()
    {
        string _dialogueData = tutorialDialogueData.DialoguesLists[currentLine].TutorialDialogueText;

        TutorialDialogueDisplay.Instance.SetDialogueString(_dialogueData);
    }

    public override void ExitStep()
    {
        base.ExitStep();

        TutorialDialogueDisplay.Instance.HiddenUI();

        Complete();
    }
}
