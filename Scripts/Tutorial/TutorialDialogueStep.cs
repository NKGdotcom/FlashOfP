using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
/// <summary>
/// チュートリアルステップ
/// </summary>
public class TutorialDialogueStep : StepBase
{
    //---チュートリアルで一度に話す量をセット---
    [SerializeField] private TutorialDialogueData tutorialDialogueData;
    private string onceDialogue;
    private CancellationTokenSource typingCts;
    //---チュートリアルの表示について---
    [SerializeField] private TutorialDialogueView tutorialDialogueView;
    private int currentLine = 0;
    private const string EMPTY_STRING = "";
    private float waitDelayNextStep = 0.2f;

    private void Awake()
    {
        if (tutorialDialogueData == null) { Debug.LogWarning("tutorialDialogueDataが設定していません"); return; }
        if (tutorialDialogueView == null) { Debug.LogWarning("tutorialDialogueViewが設定していません"); return; }
    }
    public override void EnterStep()
    {
        StartTutorial();
    }
    /// <summary>
    /// チュートリアルに入る
    /// </summary>
    private void StartTutorial()
    {
        Debug.Log("チュートリアル");
        InitialTutorial();
    }
    /// <summary>
    /// チュートリアルの初期化
    /// </summary>
    private void InitialTutorial()
    {
        GameState.Instance.SetState(State.EXPLAIN);
        currentLine = 0;
        tutorialDialogueView.ShowDialogueUI();
        UpdateView();
    }
    /// <summary>
    /// チュートリアルのデータを渡し、流す
    /// </summary>
    private void UpdateView()
    {
        onceDialogue = EMPTY_STRING;
        onceDialogue = TutorialOneDialogue();
        if (typingCts != null)
        {
            typingCts.Cancel();
            typingCts.Dispose();
        }
        typingCts = CancellationTokenSource.CreateLinkedTokenSource(this.GetCancellationTokenOnDestroy());
        tutorialDialogueView.TypeSentenceAsync(typingCts.Token, EMPTY_STRING, onceDialogue).Forget();
    }
    /// <summary>
    /// チュートリアルに流す会話を取得
    /// </summary>
    /// <returns></returns>
    private string TutorialOneDialogue()
    {
        return tutorialDialogueData.DialoguesLists[currentLine].TutorialDialogueText;
    }
    public override void UpdateStep()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentLine++;
            //---チュートリアルがまだ残っているか---
            if(IsLeftTutorialDialogue()) { UpdateView(); }
            else { DelayNextStep().Forget(); }
        }
    }
    /// <summary>
    /// チュートリアルがまだ残っている場合
    /// </summary>
    /// <returns></returns>
    private bool IsLeftTutorialDialogue()
    {
        return currentLine < tutorialDialogueData.DialoguesLists.Count;
    }
    /// <summary>
    /// 次のステップに進む前に少し待つ
    /// </summary>
    /// <returns></returns>
    private async UniTaskVoid DelayNextStep()
    {
        tutorialDialogueView.HideDialogueUI();
        //---次のステップに進む前に少し待つ---
        await UniTask.Delay(TimeSpan.FromSeconds(waitDelayNextStep),
            cancellationToken: this.GetCancellationTokenOnDestroy());
        ExitStep();
    }
    public override UniTask RetryStep(CancellationToken _token)
    {
        tutorialDialogueView.HideDialogueUI();
        return UniTask.CompletedTask;
    }
    public override void ExitStep()
    {
        Complete();
    }
}
