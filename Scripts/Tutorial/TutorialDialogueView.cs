using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
/// <summary>
/// チュートリアルの対話をテキストに表示
/// </summary>
public class TutorialDialogueView : MonoBehaviour
{
    [Header("チュートリアル対話UI")]
    [Tooltip("チュートリアルのテキストと背景部分をまとめて親オブジェクト")]
    [SerializeField] private GameObject dialogueUI;
    [Tooltip("チュートリアルの対話を表示させるテキスト")]
    [SerializeField] private TextMeshProUGUI tutorialDialogueText;
    [Tooltip("テキストを表示させた後、次の文字を表示させるのにかかる時間")]
    [SerializeField] private float displaySpeed = 0.05f;

    void Awake()
    {
        if (dialogueUI == null) { Debug.LogWarning("dialogueUIが設定されていません"); return; }
        if (tutorialDialogueText == null) { Debug.LogWarning(" tutorialDialogueTextが設定されていません"); return; }
    }

    /// <summary>
    /// チュートリアルのUIを表示
    /// </summary>
    public void ShowDialogueUI() => dialogueUI.SetActive(true);

    /// <summary>
    /// UIを非表示
    /// </summary>
    public void HideDialogueUI()=> dialogueUI.SetActive(false);

    /// <summary>
    /// 文字を即座に全表示する（スキップ用）
    /// </summary>
    public void ShowFullText(string fullText)
    {
        tutorialDialogueText.text = fullText;
    }

    /// <summary>
    /// 非同期処理でタイピングを表現
    /// 全てタイピングするか、クリックしたら終了
    /// </summary>
    public async UniTask<bool> TypeSentenceAsync(CancellationToken _token, string _empty, string _oneDialogue)
    {
        tutorialDialogueText.text = _empty;
        foreach (var _letter in _oneDialogue)
        {
            //1文字表示したら、ほんの数秒待ち、再び追加
            tutorialDialogueText.text += _letter;
            bool isCancelled = await UniTask.Delay(TimeSpan.FromSeconds(displaySpeed), cancellationToken: _token).SuppressCancellationThrow();

            if (isCancelled)
            {
                return false; 
            }
        }
        return true;
    }
}