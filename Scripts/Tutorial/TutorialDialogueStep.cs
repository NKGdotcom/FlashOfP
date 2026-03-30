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

    //---現在文字を表示中かどうかのフラグ---
    private bool isTyping = false;

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
        currentLine = 0;
        tutorialDialogueView.ShowDialogueUI();
        UpdateView();
        GameState.Instance.SetState(State.EXPLAIN);
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

        isTyping = true;
        RunTypingAsync().Forget();
    }

    /// <summary>
    /// 文字送りの非同期処理を待機し、状態を管理する
    /// </summary>
    private async UniTaskVoid RunTypingAsync()
    {
        bool isCompleted = await tutorialDialogueView.TypeSentenceAsync(typingCts.Token, EMPTY_STRING, onceDialogue);

        if (isCompleted)
        {
            isTyping = false;
        }
    }

    /// <summary>
    /// チュートリアルに流す会話を取得
    /// </summary>
    private string TutorialOneDialogue()
    {
        return tutorialDialogueData.DialoguesLists[currentLine].TutorialDialogueText;
    }

    public override void UpdateStep()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                if (typingCts != null)
                {
                    typingCts.Cancel();
                    typingCts.Dispose();
                    typingCts = null;
                }
                tutorialDialogueView.ShowFullText(onceDialogue);
                isTyping = false;
            }
            else
            {
                currentLine++;

                if (IsLeftTutorialDialogue()) { UpdateView(); }
                else { DelayNextStep().Forget(); }
            }
        }
    }

    /// <summary>
    /// チュートリアルがまだ残っている場合
    /// </summary>
    private bool IsLeftTutorialDialogue()
    {
        return currentLine < tutorialDialogueData.DialoguesLists.Count;
    }

    /// <summary>
    /// 次のステップに進む前に少し待つ
    /// </summary>
    private async UniTaskVoid DelayNextStep()
    {
        tutorialDialogueView.HideDialogueUI();
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
        Complete(); // 元のコードのまま
    }
}