using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpInterval = 1.0f;
    [SerializeField] private float jumpPower = 8;
    
    //IsJumpがtrueになるまでジャンプを待つ
    public async UniTask AutoJumpLoopAsync(PlayerP _player, CancellationToken _token)
    {
        while (true)
        {
            await UniTask.WaitUntil(() => _player.IsJump, cancellationToken: _token);

            Jump(_player);

            await UniTask.Delay(TimeSpan.FromSeconds(jumpInterval), cancellationToken: _token);
        }
    }
    //ジャンプする
    private void Jump(PlayerP _player)
    {
        SoundManager.Instance.PlaySE(SESource.jump);

        if (_player.PlayerRb != null)
        {
            if (_player.IsFlip)
            {
                _player.PlayerRb.linearVelocity = new Vector2(_player.PlayerRb.linearVelocity.x, -jumpPower);
            }
            else //重力逆の効果があるかどうか
            {
                _player.PlayerRb.linearVelocity = new Vector2(_player.PlayerRb.linearVelocity.x, jumpPower);
            }
        }
    }
}
