using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Linq;
using System.Threading;
using UnityEngine;

/// <summary>
/// ジャンプの挙動
/// </summary>
public class PlayerJump : MonoBehaviour
{
    //---物理移動---
    [SerializeField] private PlayerRbMover playerRbMover;
    [SerializeField] private WordJump[] wordJumps;
    //---ジャンプの挙動---
    private float jumpPower;
    private float jumpInterval;
    private CancellationTokenSource abilityCts;

    private void Awake()
    {
        if(playerRbMover == null) { Debug.LogError("playerRbMoverが参照されていません"); return; }
    }
    public void OnEnable()
    {
        if (abilityCts != null)
        {
            abilityCts.Cancel();
            abilityCts.Dispose();
        }
        abilityCts = new CancellationTokenSource();
        AutoJumpLoopAsync(abilityCts.Token).Forget();
    }
    public void SetParameter(PlayerData _data)
    {
        jumpPower = _data.jumpPower;
        jumpInterval = _data.jumpInterval;
    }
    /// <summary>
    /// IsJumpがtrueになるまでジャンプを待つ
    /// </summary>
    /// <param name="_token"></param>
    /// <returns></returns>
    public async UniTask AutoJumpLoopAsync(CancellationToken _token)
    {
        while (true)
        {
            //---どれかがIsJumpTriggertrueになったら---
            await UniTask.WaitUntil(() => wordJumps.Any(w => w != null && w.IsJumpTrigger), cancellationToken: _token);
            var triggeredWord = wordJumps.FirstOrDefault(w => w != null && w.IsJumpTrigger);

            playerRbMover.JumpRb(jumpPower);

            await UniTask.Delay(TimeSpan.FromSeconds(jumpInterval), cancellationToken: _token);
        }
    }
    private void OnDisable()
    {
        if (abilityCts != null)
        {
            abilityCts.Cancel();
            abilityCts.Dispose();
            abilityCts = null;
        }
    }
}
