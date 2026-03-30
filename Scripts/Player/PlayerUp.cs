using UnityEngine;
using UnityEngine.Rendering;
using System.Linq;
/// <summary>
/// 徐々に浮かぶ処理
/// </summary>
public class PlayerUp : MonoBehaviour
{
    //---物理移動---
    [SerializeField] private PlayerRbMover playerRbMover;
    [SerializeField] private WordUp[] wordUps;
    //---浮遊パラメータ---
    private float upSpeed;

    public void SetUp(PlayerData _data)
    {
        upSpeed = _data.upSpeed;
    }
    //上に浮かぶ
    public void Floating()
    {
        bool _canUp = wordUps.Any(w => w != null && w.IsUp);

        if (!_canUp)
        {
            return;
        }

        playerRbMover.UpRb(upSpeed);
    }

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
