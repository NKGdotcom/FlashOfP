using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 1チュートリアルで話す会話内容を管理するデータ
/// </summary>
[CreateAssetMenu(fileName = "New Data", menuName = "TutorialData")]
public class TutorialDialogueData : ScriptableObject
{
    [Header("チュートリアル会話データ")]
    [Tooltip("チュートリアルで表示する会話を1文（1ページ）ずつ上から順番に設定してください。")]
    public List<TutorialDialogue> DialoguesLists = new List<TutorialDialogue>();
}

/// <summary>
/// チュートリアルで話す内容
/// </summary>
[System.Serializable]
public class TutorialDialogue
{
    [TextArea(3,5)]
    public string TutorialDialogueText;
}
