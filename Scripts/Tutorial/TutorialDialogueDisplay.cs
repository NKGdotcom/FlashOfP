using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;


public class TutorialDialogueDisplay : MonoBehaviour
{
    public static TutorialDialogueDisplay Instance {  get; private set; }

    [SerializeField] private TextMeshProUGUI tutorialDialogueText;
    [SerializeField] private GameObject dialogueUI; //テキストと黒い背景が含まれる
    [SerializeField] private float displaySpeed = 0.05f;
    private string onceDialogue;
    private const string EMPTY_STRING = "";

    private CancellationTokenSource typingCts;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        OnInitialized();

        HiddenUI();
    }
 
    //初期化処理
    private void OnInitialized()
    {
        if(tutorialDialogueText == null)
        {
            Debug.LogWarning(" tutorialDialogueTextがnullです");
        }
        if(dialogueUI == null)
        {
            Debug.LogWarning("dialogueUIがnullです");
        }
    }

    //UIを非表示
    public void HiddenUI()
    {
        dialogueUI.SetActive(false);
    }

    //UIの表示
    public void ShowUI()
    {
        dialogueUI.SetActive(true);
    }

    //チュートリアルのダイアログを出す準備
    public void SetDialogueString(string _oneDialogue)
    {
        onceDialogue = EMPTY_STRING;
        onceDialogue = _oneDialogue;
        PrintDialogue();
    }

    //コルーチンを準備
    private void PrintDialogue()
    {
        if (typingCts != null)
        {
            typingCts.Cancel();
            typingCts.Dispose();
        }

        typingCts = CancellationTokenSource.CreateLinkedTokenSource(this.GetCancellationTokenOnDestroy());

        TypeSentenceAsync(typingCts.Token).Forget();
    }

    private async UniTaskVoid TypeSentenceAsync(CancellationToken _token)
    {
        tutorialDialogueText.text = EMPTY_STRING;

        foreach (var _letter in onceDialogue)
        {
            tutorialDialogueText.text += _letter;

            await UniTask.Delay(TimeSpan.FromSeconds(displaySpeed), cancellationToken: _token);
        }
    }
}
