using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data", menuName = "TutorialData")]
public class TutorialDialogueData : ScriptableObject
{
    public List<TutorialDialogue> DialoguesLists = new List<TutorialDialogue>();
}

[System.Serializable]
public class TutorialDialogue
{
    [TextArea]
    public string TutorialDialogueText;
}
