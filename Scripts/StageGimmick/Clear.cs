using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 現在のクリア状況を判定するScriptableObject
/// </summary>
[CreateAssetMenu(fileName = "New Data", menuName = "ClearData")]
public class Clear : ScriptableObject
{
    public List<StageSaveData> stageDataList = new List<StageSaveData>();
}

/// <summary>
/// 1ステージごとに通常クリアと完璧クリアを調査
/// </summary>
[System.Serializable]
public class StageSaveData
{
    public bool isClear = false;
    public bool isPerfectClear = false;
}
