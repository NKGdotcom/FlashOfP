using UnityEngine;
using UnityEngine.Rendering;
using System.Linq;
/// <summary>
/// 徐々に浮かぶ処理
/// </summary>
public class PlayerUp : MonoBehaviour
{
    [Header("コンポーネント参照")]
    [Tooltip("物理移動を管理するコンポーネント")]
    [SerializeField] private PlayerRbMover playerRbMover;
    [Tooltip("浮力のトリガーとなる単語（WordUps）の配列")]
    [SerializeField] private WordUp[] wordUps;

    //浮遊パラメータ
    private float upSpeed;

    private void Awake()
    {
        if (playerRbMover == null) { Debug.LogError("playerRbMoverが参照されていません"); return; }
        if (wordUps == null) { Debug.LogError("wordUpsが参照されていません"); return; }
    }

    /// <summary>
    /// PlayerDataからパラメータをセット
    /// </summary>
    /// <param name="_data"></param>
    public void SetUp(PlayerData _data)
    {
        upSpeed = _data.UpSpeed;
    }

    /// <summary>
    /// 上に浮遊させる
    /// </summary>
    public void Floating()
    {
        bool _canUp = wordUps.Any(w => w != null && w.IsUp);
        if(! _canUp) { return; }

        playerRbMover.UpRb(upSpeed);
    }

    /// <summary>
    /// 浮遊の効果をリセット
    /// </summary>
    public void ResetUp()
    {
        foreach(var wordUp in wordUps)
        {
            if (wordUp != null)
            {
                wordUp.ResetWord();
            }
        }
    }
}
