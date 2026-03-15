using UnityEngine;

/// <summary>
/// Rigidbodyを用いた動作
/// </summary>
public class PlayerRbMover : MonoBehaviour
{
    private Rigidbody2D playerRb;

    public void SetUp()
    {
        TryGetComponent<Rigidbody2D>(out playerRb);
    }

    /// <summary>
    /// Rbによる移動
    /// </summary>
    /// <param name="_moveDirection"></param>
    public void MovementRb(float _moveDirection)
    {
        playerRb.linearVelocity = new Vector2(_moveDirection, playerRb.linearVelocity.y);
    }
    /// <summary>
    /// Rbによるジャンプ
    /// </summary>
    /// <param name="_jumpPower"></param>
    public void JumpRb(float _jumpPower)
    {
        playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, _jumpPower);
    }
    /// <summary>
    /// 重力を反転
    /// </summary>
    public void ReverseGravity()
    {
        playerRb.gravityScale = -1f;
    }
    /// <summary>
    /// 重力を元に戻す
    /// </summary>
    public void RestoreGravity()
    {
        playerRb.gravityScale = 1f;
    }
    /// <summary>
    /// 爆発
    /// </summary>
    /// <param name="_explosionPower"></param>
    public void ExplosionRb(float _explosionPower)
    {
        playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, _explosionPower);
    }
    /// <summary>
    /// 上に浮かんでいく
    /// </summary>
    /// <param name="_upSpeed"></param>
    public void UpRb(float _upSpeed)
    {
        playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, _upSpeed);
    }
    /// <summary>
    /// 力の加わりを0に
    /// </summary>
    public void ForceZero()
    {
        playerRb.linearVelocity = Vector2.zero;
        playerRb.angularVelocity = 0f;
        transform.rotation = Quaternion.identity;
    }
}
