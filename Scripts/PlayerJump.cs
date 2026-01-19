using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpInterval = 1.0f;
    [SerializeField] private float jumpPower = 8;
    
    //コルーチンで開始
    public IEnumerator AutoJumpLoop(PlayerP _player)
    {
        while (true)
        {
            yield return new WaitUntil(() => _player.IsJump);

            Jump(_player);

            yield return new WaitForSeconds(jumpInterval);
        }
    }

    //ジャンプ
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
