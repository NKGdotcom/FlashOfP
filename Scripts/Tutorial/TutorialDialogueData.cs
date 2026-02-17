using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data", menuName = "TutorialData")]
public class TutorialDialogueData : ScriptableObject
{
    public List<TutorialDialogue> DialoguesLists = new List<TutorialDialogue>();
}

//チュートリアルで話す内容を決める
[System.Serializable]
public class TutorialDialogue
{
    [TextArea]
    public string TutorialDialogueText;
}
