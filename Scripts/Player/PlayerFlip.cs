using UnityEngine;
/// <summary>
/// プレイヤーの重力を反転させるクラス
/// </summary>
public class PlayerFlip : MonoBehaviour
{
    [Header("コンポーネント参照")]
    [Tooltip("物理移動を管理するコンポーネント")]
    [SerializeField] private PlayerRbMover playerRbMover;
    [Tooltip("重力反転のトリガーとなる単語（WordFlip）の配列")]
    [SerializeField] private WordFlip[] wordFlips;

    private void Awake()
    {
        if(playerRbMover == null) { Debug.LogError("playerRbMoverが参照されていません"); return; }
        if(wordFlips == null) { Debug.LogError("wordFlipsが参照されていません"); return; }

        foreach(var _word in wordFlips)
        {
            if (_word != null)
            {
                _word.WordComplete += ReverseGravity;
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var _word in wordFlips)
        {
            if (_word != null)
            {
                _word.WordComplete -= ReverseGravity;
            }
        }
    }

    /// <summary>
    /// プレイヤーの重力を逆に
    /// </summary>
    public void ReverseGravity()
    {
        playerRbMover.ReverseGravity();
    }

    /// <summary>
    /// プレイヤーの重力を通常状態に戻す
    /// </summary>
    public void RestoreGravity()
    {
        playerRbMover.RestoreGravity();
    }
}
