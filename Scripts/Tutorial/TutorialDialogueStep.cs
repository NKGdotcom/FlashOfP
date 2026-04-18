using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
/// <summary>
/// チュートリアルの会話を振興するステップ
/// 文字送り表示、クリックによるスキップ、次のセリフへの遷移を管理
/// </summary>
public class TutorialDialogueStep : StepBase
{
    [Header("チュートリアルデータ")]
    [Tooltip("このステップで表示する会話データのリスト")]
    [SerializeField] private TutorialDialogueData tutorialDialogueData;

    [Header("UI参照")]
    [Tooltip("会話を表示するコンポーネント")]
    [SerializeField] private TutorialDialogueView tutorialDialogueView;

    [Header("演出設定")]
    [Tooltip("最後の会話が終わってから、次のステップへ進むまでの待機時間")]
    [SerializeField] private float waitDelayNextStep = 0.2f;
    
    //状態
    private int currentLine = 0;
    private string currentDialogue;
    private bool isTyping = false;
    private CancellationTokenSource typingCts;

    private void Awake()
    {
        if (tutorialDialogueData == null) { Debug.LogWarning("tutorialDialogueDataが設定していません"); return; }
        if (tutorialDialogueView == null) { Debug.LogWarning("tutorialDialogueViewが設定していません"); return; }
    }

    /// <summary>
    /// ステップ開始した際の初期化
    /// </summary>
    public override void EnterStep()
    {
        currentLine = 0;
        tutorialDialogueView.ShowDialogueUI();

        GameState.Instance.SetState(State.EXPLAIN);

        PlayNextDialogue();
    }

    public override void UpdateStep()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                //タイピング中なら全文字を即時表示
                CancelTyping();
                tutorialDialogueView.ShowFullText(currentDialogue);
                isTyping = false;
            }
            else
            {
                //タイピングが完了していれば次の行へ進む
                currentLine++;

                if (HasNextDialogue()) { PlayNextDialogue(); }
                else { DelayNextStep().Forget(); }
            }
        }
    }

    /// <summary>
    /// このステップが終わる際に呼び出す
    /// </summary>
    public override void ExitStep() => Complete();

    /// <summary>
    /// リトライで呼び出す際に、再びUIを非表示にしリロード
    /// </summary>
    /// <param name="_token"></param>
    /// <returns></returns>
    public override UniTask RetryStep(CancellationToken _token)
    {
        tutorialDialogueView.HideDialogueUI();
        return UniTask.CompletedTask;
    }

    private void PlayNextDialogue()
    {
        //現在の行のテキストを取得
        currentDialogue = tutorialDialogueData.DialoguesLists[currentLine].TutorialDialogueText;

        //前回のタイピング処理が残っていたらキャンセル
        CancelTyping();

        typingCts = CancellationTokenSource.CreateLinkedTokenSource(this.GetCancellationTokenOnDestroy());

        isTyping = true;
        RunTypingAsync().Forget();
    }

    /// <summary>
    /// ビュー側に文字送りを依頼し、終わるまで待機する非同期処理
    /// </summary>
    private async UniTaskVoid RunTypingAsync()
    {
        //文字送りが完了したかを受け取る
        bool _isCompleted = await tutorialDialogueView.TypeSentenceAsync(typingCts.Token, string.Empty,currentDialogue);

        //最後まで表示されたらタイピング状態を削除
        if (_isCompleted)
        {
            isTyping = false;
        }
    }
    /// <summary>
    /// 実行中のタイピング処理をキャンセルし、破棄
    /// </summary>
    private void CancelTyping()
    {
        //タイピング中なら、文字送りをキャンセルし、全文字を即時表示(スキップ)
        if (typingCts != null)
        {
            typingCts.Cancel();
            typingCts.Dispose();
            typingCts = null;
        }
    }

    /// <summary>
    /// まだ表示していない会話が残っているか
    /// </summary>
    /// <returns></returns>
    private bool HasNextDialogue() => currentLine < tutorialDialogueData.DialoguesLists.Count;

    /// <summary>
    /// 次のステップに進む前に少し待ってからステップを終了する
    /// </summary>
    private async UniTaskVoid DelayNextStep()
    {
        tutorialDialogueView.HideDialogueUI();
        await UniTask.Delay(TimeSpan.FromSeconds(waitDelayNextStep),
            cancellationToken: this.GetCancellationTokenOnDestroy());
        ExitStep();
    }
}