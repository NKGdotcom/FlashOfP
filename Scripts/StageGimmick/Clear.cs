using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Œ»İ‚ÌƒNƒŠƒAó‹µ‚ğ”»’è
/// </summary>
[CreateAssetMenu(fileName = "New Data", menuName = "ClearData")]
public class Clear : ScriptableObject
{
    public List<StageSaveData> stageDataList = new List<StageSaveData>();
}
[System.Serializable]
public class StageSaveData
{
    public bool isClear = false;
    public bool isPerfectClear = false;
}
