using UnityEngine;

[CreateAssetMenu(fileName = "New Data", menuName = "ClearData")]
public class Clear : ScriptableObject
{
    public bool Stage1Clear = false;
    public bool Stage1PerfectClear = false;   
    public bool Stage2Clear = false;
    public bool Stage2PerfectClear = false;
    public bool Stage3Clear = false;
    public bool Stage3PerfectClear = false;
    public bool Stage4Clear = false;
    public bool Stage4PerfectClear = false;
    public bool Stage5Clear = false;
    public bool Stage5PerfectClear = false;
}
