using UnityEngine;
/// <summary>
/// プレイヤーの重力が逆になる
/// </summary>
public class PlayerFlip : MonoBehaviour
{
    //---物理移動---
    [SerializeField] private PlayerRbMover playerRbMover;
    [SerializeField] private WordFlip[] wordFlips;
    private void Awake()
    {
        if(playerRbMover == null) { Debug.LogError("playerRbMoverが参照されていません"); return; }
        if(wordFlips == null) { Debug.LogError("wordFlipsが参照されていません"); return; }

        foreach(var _word in wordFlips)
        {
            _word.WordComplete += ReverseGravity;
        }
    }
    /// <summary>
    /// 重力を逆に
    /// </summary>
    public void ReverseGravity()
    {
        playerRbMover.ReverseGravity();
    }
    /// <summary>
    /// 重力を元に戻す
    /// </summary>
    public void RestoreGravity()
    {
        playerRbMover.RestoreGravity();
    }
    public void OnDestroy()
    {
        foreach (var word in wordFlips)
        {
            word.WordComplete -= ReverseGravity;
        }
    }
}
