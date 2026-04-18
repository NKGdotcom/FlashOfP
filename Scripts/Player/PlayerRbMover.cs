using UnityEngine;

/// <summary>
/// PlayerのRigidbodyを用いた動作を集めた処理
/// </summary>
public class PlayerRbMover : MonoBehaviour
{
    //コンポーネント参照
    private Rigidbody2D playerRb;

    /// <summary>
    /// Rigidbodyを取得
    /// </summary>
    public void SetUp()
    {
        TryGetComponent<Rigidbody2D>(out playerRb);
    }

    /// <summary>
    /// Rigidbodyを用いた移動処理
    /// </summary>
    /// <param name="_moveDirection"></param>
    public void MovementRb(float _moveDirection)
    {
        playerRb.linearVelocity = new Vector2(_moveDirection, playerRb.linearVelocity.y);
    }

    /// <summary>
    /// Rigidbodyを用いたジャンプ処理
    /// </summary>
    /// <param name="_jumpPower"></param>
    public void JumpRb(float _jumpPower)
    {
        playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, _jumpPower);
    }

    /// <summary>
    /// 重力を逆転させる
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
    /// Rigidbodyを用いた爆発で吹き飛ぶ処理
    /// </summary>
    /// <param name="_explosionPower"></param>
    public void ExplosionRb(float _explosionPower)
    {
        playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, _explosionPower);
    }

    /// <summary>
    /// Rigidbodyを用いた浮遊処理
    /// </summary>
    /// <param name="_upSpeed"></param>
    public void UpRb(float _upSpeed)
    {
        playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, _upSpeed);
    }

    /// <summary>
    /// 力を0にして、ピタッと止める
    /// </summary>
    public void ForceZero()
    {
        //速度、加速度を0にして、回転も元に戻す
        playerRb.linearVelocity = Vector2.zero;
        playerRb.angularVelocity = 0f;
        transform.rotation = Quaternion.identity;
    }
}
