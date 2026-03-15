using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
/// <summary>
/// チュートリアルの対話を表示
/// </summary>
public class TutorialDialogueView : MonoBehaviour
{
    //---チュートリアルの文字説明---
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private TextMeshProUGUI tutorialDialogueText;
    [SerializeField] private float displaySpeed = 0.05f;

    void Awake()
    {
        if (dialogueUI == null) { Debug.LogWarning("dialogueUIが設定されていません"); return; }
        if (tutorialDialogueText == null) { Debug.LogWarning(" tutorialDialogueTextが設定されていません"); return; }
    }

    /// <summary>
    /// チュートリアルのUIを表示
    /// </summary>
    public void ShowDialogueUI()
    {
        dialogueUI.SetActive(true);
    }
    /// <summary>
    /// UIを非表示
    /// </summary>
    public void HideDialogueUI()
    {
        dialogueUI.SetActive(false);
    }

    public async UniTaskVoid TypeSentenceAsync(CancellationToken _token, string _empty, string _oneDialogue)
    {
        tutorialDialogueText.text = _empty;
        foreach (var _letter in _oneDialogue)
        {
            tutorialDialogueText.text += _letter;
            //---文字を表示するスピード---
            await UniTask.Delay(TimeSpan.FromSeconds(displaySpeed), cancellationToken: _token);
        }
    }
}
